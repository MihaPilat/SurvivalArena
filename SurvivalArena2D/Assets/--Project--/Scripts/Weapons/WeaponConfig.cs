using UnityEngine;

[CreateAssetMenu(menuName = "Game/Weapon Config")]
public class WeaponConfig : ScriptableObject
{
    public WeaponType Type;
    public int Damage;

    public float AttackCooldown;

    [Header("Ranged")]
    public GameObject ProjectilePrefab;
    public float Spread = 2f;
    public float ProjectileSpeed=12f;
    public float ProjectileLifetime = 3f;

    [Header("Magic")]
    public float MagicRadius = 1.5f;
}