using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : IStateSwitcher
{
    private List<IState> _states;
    private IState _currentState;

    public StateMachine(List<IState> states)
    {
        _states = states;
        _currentState = _states[0];
        _currentState.Enter();
    }

    public void SwitchState<T>() where T : IState
    {
        IState state = _states.FirstOrDefault(state => state is T);
        ApplyState(state, typeof(T).Name);

    }
    public void Update() => _currentState.Update();
    private void ApplyState(IState state, string typeName)
    {
        if (state == null)
        {
            throw new KeyNotFoundException($"[StateMachine] State {typeName} not registered!");
        }

        _currentState?.Exit();
        _currentState = state;
        _currentState.Enter();
    }
}