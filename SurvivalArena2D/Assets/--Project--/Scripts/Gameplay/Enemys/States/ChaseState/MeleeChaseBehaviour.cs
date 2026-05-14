public class MeleeChaseBehaviour : IChaseBehaviour
{
    public void TrySwitchState(float distance, EnemyConfig config, IStateSwitcher stateSwitcher)
    {
        if (distance <= config.StopDistance)
            stateSwitcher.SwitchState<IdleState>();
    }

}
