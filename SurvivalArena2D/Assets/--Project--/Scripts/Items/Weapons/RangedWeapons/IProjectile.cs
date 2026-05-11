using UnityEngine;

public interface IProjectile
{
    void Init(Vector2 direction, WeaponConfig config, int damage);
}
