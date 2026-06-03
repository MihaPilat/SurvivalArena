using System;
using UnityEngine;
using Zenject;

public class InputReader : IInitializable, ITickable, IInput
{
    public event Action<int> OnSlotPressed;
    public event Action OnPausePressed;

    public Vector2 Move { get; private set; }

    public bool Attack { get; private set; }

    private PlayerInputActions _input;
    private PauseManager _pauseManager;

    [Inject]
    public void Construct(PauseManager pauseManager)
    {
        _pauseManager = pauseManager;
    }

    public void Initialize()
    {
        _input = new PlayerInputActions();
        _input.Enable();

        _input.Character.Hotbar1.performed += _ => { if (!_pauseManager.IsPaused) OnSlotPressed?.Invoke(0); };
        _input.Character.Hotbar2.performed += _ => { if (!_pauseManager.IsPaused) OnSlotPressed?.Invoke(1); };
        _input.Character.Hotbar3.performed += _ => { if (!_pauseManager.IsPaused) OnSlotPressed?.Invoke(2); };
        _input.Character.Pause.performed += _ => OnPausePressed?.Invoke();
    }
    public void Tick()
    {
        if (_pauseManager.IsPaused)
        {
            Move = Vector2.zero;
            Attack = false;
            return;
        }

        Move = _input.Character.Move.ReadValue<Vector2>();
        Attack = _input.Character.Attack.IsPressed();
    }

}
