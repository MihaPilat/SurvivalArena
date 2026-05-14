using UnityEngine;
using System;
public interface IWeapon
{
    event Action OnAttack;
    WeaponType Type { get; }
    WeaponConfig Config { get; }
    Sprite Icon { get; }
    void Attack(Vector2 origin, IMouseInput mouseInput);
    void Rotate(Vector2 direction);
    void IncreaseAttackSpeed();
    void IncreaseDamage(int amount);
    void ApplySpecialUpgrade();
}
