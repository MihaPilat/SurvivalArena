using UnityEngine;
using Zenject;

public abstract class RangedWeapon : Weapon
{
    [SerializeField] protected Transform _firePoint;
    private ProjectileFactory _factory;

    [Inject]
    private void Construct(ProjectileFactory projectileFactory)
    {
        _factory = projectileFactory;
    }
    protected override void ExecuteAttack(Vector2 origin, IMouseInput mouseInput)
    {
        Vector2 dir = (mouseInput.MouseWorldPosition - (Vector2)_firePoint.position).normalized;
        dir = ProcessDirection(dir);

        _factory.Create(_config.ProjectilePrefab, _firePoint.position, dir, _config, Damage);
    }
    protected virtual Vector2 ProcessDirection(Vector2 direction) => direction;
}
