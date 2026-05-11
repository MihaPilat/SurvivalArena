using UnityEngine;

[CreateAssetMenu(menuName = "Game/Weapon Config")]
public class WeaponConfig : ScriptableObject
{
    public Sprite Icon;
    public WeaponType Type;
    public int Damage;

    public float AttackCooldown= 0.8f;
    public float AttackSpeedIncrease=0.05f;
    public int _maxAttackSpeedUpgrades=5;

    [Header("Ranged")]
    public GameObject ProjectilePrefab;
    public float Spread = 2f;
    public float ProjectileSpeed=12f;
    public float ProjectileLifetime = 3f;
    public bool RotateToTarget=true;

    [Header("Magic")]
    public float MagicRadius = 1.5f;
}
