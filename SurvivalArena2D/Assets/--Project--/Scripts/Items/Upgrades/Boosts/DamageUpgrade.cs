using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Damage")]
public class DamageUpgrade : UpgradeData
{
    public int Amount = 10;

    public override void Apply(Character character)
        => character.UpgradeWeaponDamage(Amount);
}
