public interface IStateSwitcher 
{
    void SwitchState<T>() where T : IState;
    void SwitchState(IState state);
}
