using System;
using UnityEngine;

public interface IInput
{
    event Action<int> OnSlotPressed;
    event Action OnPausePressed;
    bool Attack { get; }
    Vector2 Move { get; }
}
