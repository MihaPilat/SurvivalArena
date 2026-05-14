using System;
using UnityEngine;

public interface IInput
{
    event Action<int> OnSlotPressed;
    bool Attack { get; }
    Vector2 Move { get; }
}
