using UnityEngine;

public class MagicProjectile : Projectile
{
    private float _explosionRadius;

    public override void Init(Vector2 direction, WeaponConfig config, int damage)
    {
        base.Init(direction, config, damage);
        _explosionRadius = config.MagicRadius;
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

        ReturnToPool();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
