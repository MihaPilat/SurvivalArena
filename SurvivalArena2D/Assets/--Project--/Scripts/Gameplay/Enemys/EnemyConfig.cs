using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Game/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    public float Health = 100f;
    public float Speed = 3.5f;
    public float StopDistance = 1.5f;
    public float WaitingTime = 1f;
    public int ContactDamage = 5;
    public int ExpAmount = 10;

    [Header("Ranged")]
    public float MinAttackRange = 5f;
    public float MaxAttackRange = 8f;
    public float AttackCooldown=5f;
    public float Spread = 2f;
    public float ProjectileSpeed = 12f;
    public float ProjectileLifetime = 3f;
    public GameObject ProjectilePrefab;
    public int Damage=5;

    [Header("Magic")]
    public float MagicRadius = 1.5f;
}
