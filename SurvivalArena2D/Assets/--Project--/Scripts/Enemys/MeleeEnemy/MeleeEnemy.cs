using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyEntity : EnemyEntity
{
    private MeleeChaseBehaviour _chaseBehaviour = new MeleeChaseBehaviour();
    protected override List<IState> AddStates()
    {
        return new List<IState>()
        {
            new ChaseState(this,_chaseBehaviour)
        };
    }
}
