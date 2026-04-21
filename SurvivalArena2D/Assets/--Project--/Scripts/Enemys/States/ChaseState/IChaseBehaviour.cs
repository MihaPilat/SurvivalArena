public interface IChaseBehaviour
{
    void TrySwitchState(float distance,float stopDistance, IStateSwitcher stateSwitcher);
}