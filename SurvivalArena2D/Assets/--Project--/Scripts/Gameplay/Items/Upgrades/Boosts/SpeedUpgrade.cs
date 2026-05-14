using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Speed")]
public class SpeedUpgrade : UpgradeData
{
    public override void Apply(Character character)
    => character.IncreaseSpeed();
}
