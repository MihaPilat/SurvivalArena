using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    public event Action OnAttack;

    [SerializeField] private WeaponConfig _config;
    [SerializeField] private Transform _firePoint;

    public WeaponType Type => _config.Type;
    public int Damage => _config.Damage;
    public WeaponConfig Config => _config;

    private float _lastAttackTime;

    public void Attack(Vector2 origin, Vector2 direction)
    {
        if (!CanAttack())
            return;

        _lastAttackTime = Time.time;

        OnAttack?.Invoke();
        Debug.Log("Attack Bow");
    }
    public void Rotate(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private bool CanAttack()
    {
        return Time.time >= _lastAttackTime + _config.AttackCooldown;
    }
}
