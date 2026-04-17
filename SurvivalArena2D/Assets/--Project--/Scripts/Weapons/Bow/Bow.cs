using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class Bow : MonoBehaviour, IWeapon
{
    public event Action OnAttack;

    [SerializeField] private WeaponConfig _config;
    [SerializeField] private Transform _firePoint;

    public WeaponType Type => _config.Type;
    public int Damage => _config.Damage;
    public WeaponConfig Config => _config;

    private ProjectileFactory _factory = new ProjectileFactory();

    private float _lastAttackTime;

    public void Attack(Vector2 origin, IMouseInput mouseInput)
    {
        if (!CanAttack())
            return;

        _lastAttackTime = Time.time;

        OnAttack?.Invoke();
        Debug.Log("Attack Bow");

        Vector2 dir = (mouseInput.MouseWorldPosition - (Vector2)_firePoint.position).normalized;

        float spread = Random.Range(-_config.Spread, _config.Spread);
        dir = Quaternion.Euler(0, 0, spread) * dir;

        _factory.Create(
        _config.ProjectilePrefab,
        _firePoint.position,
        dir,
        _config
    );
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
