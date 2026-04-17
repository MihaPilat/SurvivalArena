using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicStaff : MonoBehaviour, IWeapon
{
    public event Action OnAttack;

    [SerializeField] private WeaponConfig _config;
    [SerializeField] private Transform _firePoint;

    public WeaponType Type => _config.Type;

    public WeaponConfig Config => _config;

    public int Damage => _config.Damage;

    private ProjectileFactory _factory = new ProjectileFactory();

    private float _lastAttackTime;

    public void Attack(Vector2 origin, IMouseInput mouseInput)
    {
        if (!CanAttack())
            return;

        _lastAttackTime = Time.time;

        OnAttack?.Invoke();

        Vector2 dir = (mouseInput.MouseWorldPosition - (Vector2)_firePoint.position).normalized;

        _factory.Create(
        _config.ProjectilePrefab,
        _firePoint.position,
        dir,
        _config);
    }

    public void Rotate(Vector2 direction)
    {

    }

    private bool CanAttack()
    {
        return Time.time >= _lastAttackTime + _config.AttackCooldown;
    }
}
