using System;
using UnityEngine;
using Zenject;

public class Character : MonoBehaviour, IEnemyTarget
{
    public event Action<int, int> OnHealthChanged;
    public event Action OnDied;
    public event Action OnDamaged;

    [SerializeField] private ExperienceCollector _collector;

    private int _maxHealth;
    private int _health;

    private float _lastDamageTime;
    private float _damageCooldown;

    private float _currentPickupRadius;
    private float _radiusUpgradeStep;

    private IWeapon _currentWeapon;

    private IMouseInput _mouseInput;

    private ILevelable _levelSystem;

    public Vector2 MoveDirection { get; private set; }
    public Vector2 AimDirection { get; private set; }

    public float Speed { get; private set; }

    public Vector3 Position => transform.position;

    public bool IsDie => _health <= 0;

    [Inject]
    private void Construct(CharacterStatsConfig characterStatsConfig, IMouseInput mouseInput, CameraService cameraService,
        ILevelable levelSystem)
    {
        _health = _maxHealth = characterStatsConfig.MaxHealth;
        Speed = characterStatsConfig.Speed;
        _damageCooldown = characterStatsConfig.DamageCooldown;
        _mouseInput = mouseInput;
        cameraService.SetTarget(transform);
        _levelSystem = levelSystem;

        _currentPickupRadius = characterStatsConfig.BasePickupRadius;
        _radiusUpgradeStep = characterStatsConfig.RadiusUpgradeStep;

        _collector.UpdateRadius(_currentPickupRadius);
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
        if (IsDie || damage <= 0 || Time.time < _lastDamageTime + _damageCooldown)
            return;

        _lastDamageTime = Time.time;

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

    public void AddMaxHealth(int amount)
    {
        if (amount <= 0)
            return;

        _maxHealth += amount;
        OnHealthChanged?.Invoke(_health, _maxHealth);
    }

    public void AddHealth(int amount)
    {
        if (amount <= 0 || IsDie)
            return;

        _health = Mathf.Min(_health + amount, _maxHealth);
        OnHealthChanged?.Invoke(_health, _maxHealth);
    }
    public void IncreaseMaxHealth(int amount)
    {
        AddMaxHealth(amount);
        AddHealth(amount);
    }

    public void IncreasePickupRadius()
    {
        _currentPickupRadius += _radiusUpgradeStep;
        _collector.UpdateRadius(_currentPickupRadius);
    }

    private void UpdateAimDirection()
    {
        Vector2 dir = _mouseInput.MouseWorldPosition - (Vector2)transform.position;
        AimDirection = dir.normalized;
    }

    public void AddExperience(int amount) => _levelSystem.AddExperience(amount);

    public ILevelable LevelProgress => _levelSystem;

    private void Die() => OnDied?.Invoke();
}
