public interface IChaseBehaviour
{
    void TrySwitchState(float distance,EnemyConfig config, IStateSwitcher stateSwitcher);
}