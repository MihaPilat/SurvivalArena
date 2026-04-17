using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MagicProjectile : MonoBehaviour, IProjectile
{
    [SerializeField] private LayerMask _targetLayer;

    private Rigidbody2D _rb;

    private int _damage;

    private float _radius;

    public void Init(Vector2 direction, WeaponConfig config)
    {
        if (_rb == null)
            _rb = GetComponent<Rigidbody2D>();
        _damage = config.Damage;

        _rb.velocity = direction.normalized * config.ProjectileSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        Destroy(gameObject, config.ProjectileLifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & _targetLayer) == 0)
            return;
        Explode();
    }

    private void Explode()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
        transform.position,
        _radius,
        _targetLayer
    );

        foreach (var hit in hits)
        {
            var damageable = hit.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(_damage);
            }
        }

        Destroy(gameObject);
    }
}
