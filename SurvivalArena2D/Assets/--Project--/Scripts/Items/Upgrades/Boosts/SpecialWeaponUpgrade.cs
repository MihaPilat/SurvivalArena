using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/WeaponSpecialUpgrade")]
public class SpecialWeaponUpgrade : UpgradeData
{
    public override void Apply(Character character)
        => character.UpgradeWeaponSpecial();
}
