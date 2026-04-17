using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.InputSystem;

public class InputReader : IInitializable, ITickable, IInput
{
    public Vector2 Move { get; private set; }

    public bool Attack { get; private set; }

    private PlayerInputActions _input;
    public void Initialize()
    {
        _input = new PlayerInputActions();
        _input.Enable();

    }
    public void Tick()
    {
        Move = _input.Character.Move.ReadValue<Vector2>();
        Attack = _input.Character.Attack.IsPressed();
    }

}
