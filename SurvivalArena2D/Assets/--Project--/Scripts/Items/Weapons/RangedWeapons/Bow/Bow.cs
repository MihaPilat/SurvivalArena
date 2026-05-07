using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bow : RangedWeapon
{
    protected override Vector2 ProcessDirection(Vector2 direction)
    {
        float spread = UnityEngine.Random.Range(-_config.Spread, _config.Spread);
        return Quaternion.Euler(0, 0, spread) * direction;
    }
}
