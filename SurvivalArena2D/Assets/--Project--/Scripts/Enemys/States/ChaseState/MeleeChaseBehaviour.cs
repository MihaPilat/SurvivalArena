using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeChaseBehaviour : IChaseBehaviour
{
    public void TrySwitchState(float distance, float stopDistance, IStateSwitcher stateSwitcher)
    {
        if (distance <= stopDistance)
            stateSwitcher.SwitchState<IdleState>();
    }

}
