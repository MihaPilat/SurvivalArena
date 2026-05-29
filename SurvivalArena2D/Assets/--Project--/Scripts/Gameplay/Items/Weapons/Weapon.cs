using System;
using UnityEngine;
using Zenject;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    public event Action OnAttack;

    [SerializeField] protected WeaponConfig _config;

    private int _specialUpgradeLevel = 0;

    private float _attackCooldown;
    private float _attackSpeedIncrease;
    protected float _lastAttackTime;

    private int _currentDamage;

    private int _maxAttackSpeedUpgrades;
    private int _currentAttackSpeedUpgrades;

    public Sprite Icon => _config.Icon;
    public WeaponType Type => _config.Type;
    public WeaponConfig Config => _config;
    public int Damage => _currentDamage;


    private void Awake()
    {
        _attackCooldown = _config.AttackCooldown;
        _attackSpeedIncrease = _config.AttackSpeedIncrease;
        _maxAttackSpeedUpgrades = _config._maxAttackSpeedUpgrades;
        _currentDamage = _config.Damage;
    }

    public void Attack(Vector2 origin, IMouseInput mouseInput)
    {
        if (!CanAttack()) return;

        _lastAttackTime = Time.time;
        OnAttack?.Invoke();

        ExecuteAttack(origin, mouseInput);
    }

    public virtual void Rotate(Vector2 direction)
    {
        if (!_config.RotateToTarget) return;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void ApplySpecialUpgrade()
    {
        if (_specialUpgradeLevel >= _config.MaxSpecialUpgrades)
            return;

        _specialUpgradeLevel++;
        ExecuteSpecialUpgrade();
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
    public void IncreaseDamage(int amount)
    {
        if (amount <= 0)
            return;
        _currentDamage += amount;
    }

    protected abstract void ExecuteAttack(Vector2 origin, IMouseInput mouseInput);

    protected abstract void ExecuteSpecialUpgrade();

    protected bool CanAttack() => Time.time >= _lastAttackTime + _attackCooldown;
}
