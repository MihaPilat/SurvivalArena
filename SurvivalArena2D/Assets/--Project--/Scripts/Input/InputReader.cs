using System;
using UnityEngine;
using Zenject;

public class InputReader : IInitializable, ITickable, IInput
{
    public event Action<int> OnSlotPressed;

    public Vector2 Move { get; private set; }

    public bool Attack { get; private set; }

    private PlayerInputActions _input;
    public void Initialize()
    {
        _input = new PlayerInputActions();
        _input.Enable();

        _input.Character.Hotbar1.performed += _ => OnSlotPressed?.Invoke(0);
        _input.Character.Hotbar2.performed += _ => OnSlotPressed?.Invoke(1);
        _input.Character.Hotbar3.performed += _ => OnSlotPressed?.Invoke(2);
    }
    public void Tick()
    {
        Move = _input.Character.Move.ReadValue<Vector2>();
        Attack = _input.Character.Attack.IsPressed();
    }

}
