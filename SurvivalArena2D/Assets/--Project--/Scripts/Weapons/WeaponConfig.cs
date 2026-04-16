using UnityEngine;

[CreateAssetMenu(menuName = "Game/Weapon Config")]
public class WeaponConfig : ScriptableObject
{
    public WeaponType Type;
    public int Damage;

    public float AttackCooldown;

    [Header("Ranged")]
    public GameObject ProjectilePrefab;
    public float ProjectileSpeed;
}