public class RangeChaseBehaviour : IChaseBehaviour
{
    public void TrySwitchState(float distance, EnemyConfig config, IStateSwitcher stateSwitcher)
    {
        if (distance <= config.MinAttackRange)
            stateSwitcher.SwitchState<RangeAttackState>();
    }
}
