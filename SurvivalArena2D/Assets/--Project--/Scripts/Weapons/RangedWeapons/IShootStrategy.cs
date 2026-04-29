using UnityEngine;

public interface IShootStrategy
{
    void Shoot(Transform firePoint, Vector2 targetPos, WeaponConfig config, ProjectileFactory factory);
}