using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Character/StatsConfig", fileName ="CharacterStatsConfig")]
public class CharacterStatsConfig : ScriptableObject
{
    [field:SerializeField,Range(1,50)] public int MaxHealth { get; private set; }
    [field: SerializeField, Range(1, 50)] public float Speed { get; private set; }
    [field: SerializeField, Range(0.1f, 1f)] public float DamageCooldown { get; private set; }
}
