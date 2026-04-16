using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    public event Action OnAttack;
    public WeaponType Type => WeaponType.Sword;
    public void Attack(Vector2 origin, Vector2 direction)
    {
        OnAttack?.Invoke();
        Debug.Log("Attack sword");
    }

    public void Rotate(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
