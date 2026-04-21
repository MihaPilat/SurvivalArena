using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeChaseBehaviour : IChaseBehaviour
{
    public IState GetNextState(float distance, float stopDistance)
    {
        if (distance <= stopDistance)
            return null;

        return null;
    }

}
