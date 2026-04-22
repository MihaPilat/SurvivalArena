using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ArrowProjectile : MonoBehaviour, IProjectile
{ 
    [SerializeField] private LayerMask _targetLayer;

    private Rigidbody2D _rb;

    private int _damage;

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
        TryDamage(other);
    }

    private void TryDamage(Collider2D other)
    {
        if (other == null)
            return;

        if (((1 << other.gameObject.layer) & _targetLayer) == 0)
            return;

        var damageable = other.GetComponent<IDamageable>();

        if (damageable == null)
            return;

        damageable.TakeDamage(_damage);
    }

}
