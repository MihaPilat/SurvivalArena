using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeChaseBehaviour : IChaseBehaviour
{
    public void TrySwitchState(float distance, EnemyConfig config, IStateSwitcher stateSwitcher)
    {
        if (distance <= config.MinAttackRange)
            stateSwitcher.SwitchState<RangeAttackState>();
    }
}
