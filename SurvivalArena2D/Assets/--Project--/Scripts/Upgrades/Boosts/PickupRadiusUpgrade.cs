using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/PickupRadius")]
public class PickupRadiusUpgrade : UpgradeData
{
    public override void Apply(Character character) => character.IncreasePickupRadius();
}
