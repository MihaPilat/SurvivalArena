using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Projectile : MonoBehaviour, IProjectile, IDamageable
{
    [SerializeField] protected LayerMask _targetLayer;
    [SerializeField] private int _health = 1;

    protected Rigidbody2D _rb;
    protected int _damage;
    private int _currentHealth;

    protected PoolFactory _poolFactory;
    private Projectile _originPrefab;
    private Coroutine _lifeTimeCoroutine;
    private bool _isDestroyed;

    public void SetPoolData(Projectile prefab, PoolFactory factory)
    {
        _originPrefab = prefab;
        _poolFactory = factory;
    }

    public virtual void Init(Vector2 direction, WeaponConfig config, int damage)
    {
        InternalInit(direction, config.ProjectileSpeed, config.ProjectileLifetime, damage);
    }
    public virtual void Init(Vector2 direction, EnemyConfig config, int damage)
    {
        InternalInit(direction, config.ProjectileSpeed, config.ProjectileLifetime, damage);
    }

    public virtual void SetExplosionRadius(float radius) { }

    public void TakeDamage(int damage)
    {
        if (_isDestroyed || damage <= 0) return;

        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            _isDestroyed = true;
            OnProjectileDestroyed();
        }
    }

    protected virtual void OnProjectileDestroyed()
    {
        ReturnToPool();
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
    private void InternalInit(Vector2 direction, float speed, float lifetime, int damage)
    {
        if (_rb == null) _rb = GetComponent<Rigidbody2D>();

        if (_lifeTimeCoroutine != null) StopCoroutine(_lifeTimeCoroutine);

        _damage = damage;
        _isDestroyed = false;
        _currentHealth = _health;

        _rb.velocity = direction.normalized * speed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        ResetState();

        _lifeTimeCoroutine = StartCoroutine(LifetimeRoutine(lifetime));
    }

    protected virtual void ResetState()
    {
    }

    protected void ReturnToPool()
    {
        if (_lifeTimeCoroutine != null)
        {
            StopCoroutine(_lifeTimeCoroutine);
            _lifeTimeCoroutine = null;
        }
        _poolFactory.Reclaim<Projectile>(this, _originPrefab);
    }

    private IEnumerator LifetimeRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        ReturnToPool();
    }

    private bool IsTargetLayer(int layer)
    {
        return ((1 << layer) & _targetLayer) != 0;
    }
}
