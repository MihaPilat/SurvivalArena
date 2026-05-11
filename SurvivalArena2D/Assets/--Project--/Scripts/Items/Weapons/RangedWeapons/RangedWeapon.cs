using UnityEngine;
using Zenject;

public abstract class RangedWeapon : Weapon
{
    [SerializeField] protected Transform _firePoint;
    private ProjectileFactory _factory;

    protected int _extraProjectiles = 0;
    protected float _explosionRadiusModifier = 0f;

    [Inject]
    private void Construct(ProjectileFactory projectileFactory)
    {
        _factory = projectileFactory;
    }
    protected override void ExecuteAttack(Vector2 origin, IMouseInput mouseInput)
    {
        Vector2 baseDir = (mouseInput.MouseWorldPosition - (Vector2)_firePoint.position).normalized;

        int totalProjectiles = 1 + _extraProjectiles;
        float angleStep = 10f;

        for (int i = 0; i < totalProjectiles; i++)
        {
            float offset = (i - (totalProjectiles - 1) / 2f) * angleStep;
            Vector2 dir = Quaternion.Euler(0, 0, offset) * baseDir;

            dir = ProcessDirection(dir);

            var projectile = _factory.Create(_config.ProjectilePrefab, _firePoint.position, dir, _config, Damage);

            float finalRadius = _config.MagicRadius + _explosionRadiusModifier;
            projectile.SetExplosionRadius(finalRadius);

        }
    }
    protected virtual Vector2 ProcessDirection(Vector2 direction) => direction;
}
