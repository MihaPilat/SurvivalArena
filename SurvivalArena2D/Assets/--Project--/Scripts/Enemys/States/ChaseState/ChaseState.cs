using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : EnemyState
{
    private IChaseBehaviour _behaviour;
    public ChaseState(EnemyEntity enemyEntity, IChaseBehaviour chaseBehaviour) : base(enemyEntity)
    {
        _behaviour = chaseBehaviour;
    }
    public override void Enter() => _enemyEntity.Agent.isStopped = false;
    public override void Update()
    {
        _enemyEntity.Agent.SetDestination(_enemyEntity.Character.transform.position);

        float distance = Vector3.Distance(_enemyEntity.transform.position, _enemyEntity.Character.transform.position);

        IState next = _behaviour.GetNextState(distance,_enemyEntity.Config.StopDistance);

        if (next != null)
            StateSwitcher.SwitchState(next);
    }
    public override void Exit() => _enemyEntity.Agent.isStopped = true;
}
