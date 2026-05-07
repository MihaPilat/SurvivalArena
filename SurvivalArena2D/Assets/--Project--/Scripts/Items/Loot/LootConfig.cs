using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LootConfig", menuName = "Configs/LootConfig")]
public class LootConfig : ScriptableObject
{
    public List<SpecialSpawnEvent> SpecialEvents;
    public GameObject DefaultPickupPrefab;
}