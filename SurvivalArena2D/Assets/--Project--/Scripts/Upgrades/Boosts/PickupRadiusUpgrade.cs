using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/PickupRadius")]
public class PickupRadiusUpgrade : UpgradeData
{
    public override void Apply(Character character) => character.IncreasePickupRadius();
}
