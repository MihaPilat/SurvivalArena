using UnityEngine;

public class MagicProjectile : Projectile
{
    [SerializeField] private ExplosionEffect _explosionEffectPrefab;

    private float _explosionRadius;
    private Color _explosionColor;

    public override void Init(Vector2 direction, WeaponConfig config, int damage)
    {
        base.Init(direction, config, damage);
        _explosionRadius = config.MagicRadius;
        _explosionColor = config.MagicColor;
    }

    public override void Init(Vector2 direction, EnemyConfig config, int damage)
    {
        base.Init(direction, config, damage);
        _explosionRadius = config.MagicRadius;
        _explosionColor = config.MagicColor;
    }

    public override void SetExplosionRadius(float radius)
    {
        _explosionRadius = radius;
    }

    protected override void OnHit(Collider2D other)
    {
        Explode();
    }

    private void Explode()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _explosionRadius, _targetLayer);

        foreach (var hit in hits)
        {
            TryApplyDamage(hit);
        }

        if (_explosionEffectPrefab != null)
        {
            ExplosionEffect effectInstance = _poolFactory.Get<ExplosionEffect>(_explosionEffectPrefab);

            effectInstance.SetPoolData(_explosionEffectPrefab, _poolFactory);

            effectInstance.PlayExplosion(transform.position, _explosionRadius, _explosionColor);
        }

        ReturnToPool();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
