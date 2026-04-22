using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Game/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    public float Health = 100f;
    public float Speed = 3.5f;
    public float StopDistance = 1.5f;
    public float WaitingTime = 1f;

    [Header("Ranged")]
    public float AttackRange = 10f;
    public float AttackCooldown=5f;
    public GameObject ProjectilePrefab;
    public float Spread = 2f;
    public float ProjectileSpeed = 12f;
    public float ProjectileLifetime = 3f;
}