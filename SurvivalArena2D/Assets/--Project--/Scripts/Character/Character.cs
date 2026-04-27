using System;
using UnityEngine;
using Zenject;

public class Character : MonoBehaviour, IEnemyTarget
{
    public event Action<int, int> OnHealthChanged;
    public event Action OnDied;
    public event Action OnDamaged;

    private int _maxHealth;
    private int _health;

    private IWeapon _currentWeapon;

    private IMouseInput _mouseInput;

    public Vector2 MoveDirection { get; private set; }
    public Vector2 AimDirection { get; private set; }

    public float Speed { get; private set; }

    public Vector3 Position => transform.position;

    public bool IsDie => _health <= 0;

    [Inject]
    private void Construct(CharacterStatsConfig characterStatsConfig, IMouseInput mouseInput, CameraService cameraService)
    {
        _health = _maxHealth = characterStatsConfig.MaxHealth;
        Speed = characterStatsConfig.Speed;
        _mouseInput = mouseInput;
        cameraService.SetTarget(transform);

    }

    private void Update()
    {
        if (IsDie)
            return;
        UpdateAimDirection();
    }

    public void SetWeapon(IWeapon weapon)
    {
        if (IsDie)
            return;
        _currentWeapon = weapon;
    }

    public void Attack()
    {
        if (IsDie)
            return;
        _currentWeapon?.Attack(transform.position, _mouseInput);
    }
    public void TakeDamage(int damage)
    {
        if (IsDie || damage <= 0)
            return;
        _health -= damage;
        _health = Mathf.Clamp(_health, 0, _maxHealth);

        OnDamaged?.Invoke();
        OnHealthChanged?.Invoke(_health, _maxHealth);
        Debug.Log($"Character taked {damage} damage");
        if (_health <= 0)
        {
            Die();
        }
    }
    public void SetMoveDirection(Vector2 direction)
    {
        MoveDirection = direction;
    }
    private void UpdateAimDirection()
    {
        Vector2 dir = _mouseInput.MouseWorldPosition - (Vector2)transform.position;
        AimDirection = dir.normalized;
    }
    private void Die()
    {
        OnDied?.Invoke();
    }
}
