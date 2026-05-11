using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/AttackSpeed")]
public class AttackSpeedUpgrade : UpgradeData
{
    public override void Apply(Character character)
    {
        character.UpgradeWeaponAttackSpeed();
    }
}
