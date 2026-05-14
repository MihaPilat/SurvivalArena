using System;
using UnityEngine;

public class MagicStaff : RangedWeapon
{
    protected override void ExecuteSpecialUpgrade()
    {
        _explosionRadiusModifier += _config.ExplosionRadiusIncreasePerUpgrade;
    }
}
