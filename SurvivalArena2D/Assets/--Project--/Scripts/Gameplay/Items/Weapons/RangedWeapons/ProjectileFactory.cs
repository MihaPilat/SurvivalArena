using UnityEngine;

public class ProjectileFactory
{
    public IProjectile Create(
        GameObject prefab,
        Vector2 position,
        Vector2 direction,
        WeaponConfig config,
        int damage)
    {
        if (prefab == null)
        {
            Debug.LogError("ProjectileFactory: prefab is NULL");
            return null;
        }
        GameObject obj = Object.Instantiate(prefab, position, Quaternion.identity);

        var projectile = obj.GetComponent<IProjectile>();
        projectile.Init(direction, config, damage);

        return projectile;
    }
}
