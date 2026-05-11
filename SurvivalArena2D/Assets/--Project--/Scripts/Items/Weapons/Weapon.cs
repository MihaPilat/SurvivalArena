using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    public event Action OnAttack;

    [SerializeField] protected WeaponConfig _config;

    public WeaponType Type => _config.Type;
    public WeaponConfig Config => _config;
    public int Damage => _config.Damage;

    public Sprite Icon => _config.Icon;

    private float _attackCooldown;
    private float _attackSpeedIncrease;
    protected float _lastAttackTime;

    private int _maxAttackSpeedUpgrades;
    private int _currentAttackSpeedUpgrades;

    private void Awake()
    {
        _attackCooldown = _config.AttackCooldown;
        _attackSpeedIncrease = _config.AttackSpeedIncrease;
        _maxAttackSpeedUpgrades = Config._maxAttackSpeedUpgrades;
    }

    public void Attack(Vector2 origin, IMouseInput mouseInput)
    {
        if (!CanAttack()) return;

        _lastAttackTime = Time.time;
        OnAttack?.Invoke();

        ExecuteAttack(origin, mouseInput);
    }
    protected abstract void ExecuteAttack(Vector2 origin, IMouseInput mouseInput);

    public virtual void Rotate(Vector2 direction)
    {
        if (!_config.RotateToTarget) return;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void IncreaseAttackSpeed()
    {
        if (_currentAttackSpeedUpgrades >= _maxAttackSpeedUpgrades)
            return;
        _currentAttackSpeedUpgrades++;
        _attackCooldown -= _attackSpeedIncrease;
        if (_attackCooldown < 0.05f)
            _attackCooldown = 0.05f;
    }

    protected bool CanAttack() => Time.time >= _lastAttackTime + _attackCooldown;
}
