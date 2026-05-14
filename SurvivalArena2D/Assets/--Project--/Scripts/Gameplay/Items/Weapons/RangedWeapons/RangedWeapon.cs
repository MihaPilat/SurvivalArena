using UnityEngine;
using Zenject;

public abstract class RangedWeapon : Weapon
{
    [SerializeField] protected Transform _firePoint;
    private PoolFactory _poolFactory;

    protected int _extraProjectiles = 0;
    protected float _explosionRadiusModifier = 0f;

    [Inject]
    private void Construct(PoolFactory poolFactory)
    {
        _poolFactory = poolFactory;
    }
    protected override void ExecuteAttack(Vector2 origin, IMouseInput mouseInput)
    {
        Vector2 baseDir = (mouseInput.MouseWorldPosition - (Vector2)_firePoint.position).normalized;

        int totalProjectiles = 1 + _extraProjectiles;
        float angleStep = 10f;

        Projectile prefabComponent = _config.ProjectilePrefab.GetComponent<Projectile>();

        for (int i = 0; i < totalProjectiles; i++)
        {
            float offset = (i - (totalProjectiles - 1) / 2f) * angleStep;
            Vector2 dir = Quaternion.Euler(0, 0, offset) * baseDir;

            dir = ProcessDirection(dir);

            Projectile projectile = _poolFactory.Get<Projectile>(prefabComponent);

            projectile.transform.position = _firePoint.position;

            projectile.SetPoolData(prefabComponent, _poolFactory);

            projectile.Init(dir, _config, Damage);

            float finalRadius = _config.MagicRadius + _explosionRadiusModifier;
            projectile.SetExplosionRadius(finalRadius);

        }
    }
    protected virtual Vector2 ProcessDirection(Vector2 direction) => direction;
}
