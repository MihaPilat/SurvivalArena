using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : Projectile
{
    private HashSet<IDamageable> _hitTargets = new HashSet<IDamageable>();
    protected override void OnHit(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            if (!_hitTargets.Contains(damageable))
            {
                TryApplyDamage(other);
                _hitTargets.Add(damageable);
            }
        }
    }
    //после перенесения в пулл, не забыть очистить старые цели
}
