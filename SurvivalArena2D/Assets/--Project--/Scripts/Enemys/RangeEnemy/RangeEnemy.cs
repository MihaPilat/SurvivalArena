using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : EnemyEntity
{
    private RangeChaseBehaviour _rangeChaseBehaviour= new RangeChaseBehaviour();
    protected override List<IState> AddStates()
    {
        return new List<IState>()
        {
            new ChaseState(this, _rangeChaseBehaviour),
            new RangeAttackState(this),
            new DeathState(this)
        };
    }
}
