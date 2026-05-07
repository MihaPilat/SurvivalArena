using System;
using UnityEngine;

public class Sword : Weapon
{
    protected override void ExecuteAttack(Vector2 origin, IMouseInput mouseInput)
    {
        Debug.Log("Взмах мечом: " + _config.name);
    }
}
