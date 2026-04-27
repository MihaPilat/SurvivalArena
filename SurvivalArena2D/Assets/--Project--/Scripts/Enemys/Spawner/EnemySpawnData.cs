using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnData", menuName = "Game/EnemySpawnData")]
public class EnemySpawnData : ScriptableObject
{
    public GameObject Prefab;
    public int Cost=1;
    public int MinDangerLevel=1;
}
