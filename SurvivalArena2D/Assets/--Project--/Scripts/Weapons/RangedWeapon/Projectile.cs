using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Projectile : MonoBehaviour, IProjectile
{
    [SerializeField] protected LayerMask _targetLayer;

    protected Rigidbody2D _rb;
    protected int _damage;

    public virtual void Init(Vector2 direction, WeaponConfig config)
    {
        if (_rb == null)
            _rb = GetComponent<Rigidbody2D>();

        _damage = config.Damage;
        _rb.velocity = direction.normalized * config.ProjectileSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        Destroy(gameObject, config.ProjectileLifetime);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsTargetLayer(other.gameObject.layer))
            return;

        OnHit(other);
    }

    protected abstract void OnHit(Collider2D other);

    protected bool TryApplyDamage(Collider2D other)
    {
        if (other == null) return false;

        if (!IsTargetLayer(other.gameObject.layer)) return false;

        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(_damage);
            return true;
        }

        return false;
    }
    private bool IsTargetLayer(int layer)
    {
        return ((1 << layer) & _targetLayer) != 0;
    }
}