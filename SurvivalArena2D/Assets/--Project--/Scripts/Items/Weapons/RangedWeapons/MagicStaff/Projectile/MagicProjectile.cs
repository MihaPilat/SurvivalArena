using UnityEngine;

public class MagicProjectile : Projectile
{
    private float _explosionRadius;

    public override void Init(Vector2 direction, WeaponConfig config)
    {
        base.Init(direction, config);
        _explosionRadius = config.MagicRadius;
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

        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
