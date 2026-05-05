using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/MaxHealth")]
public class MaxHealthUpgrade : UpgradeData
{
    public int Amount = 20;

    public override void Apply(Character player)
        => player.IncreaseMaxHealth(Amount);
}