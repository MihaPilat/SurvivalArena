using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    WeaponType Type { get; }
    WeaponConfig Config { get; }
    void Attack(Vector2 origin, Vector2 direction);
    void Rotate(Vector2 direction);
}
