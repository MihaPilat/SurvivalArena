using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Heal")]
public class HealUpgrade : UpgradeData
{
    public int Amount = 20;

    public override void Apply(Character player)
        => player.AddHealth(Amount);
}