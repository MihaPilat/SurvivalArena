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
        _enemyEntity.Agent.SetDestination(_enemyEntity.Target.position);

        float distance = Vector3.Distance(_enemyEntity.transform.position, _enemyEntity.Target.position);

        _behaviour.TrySwitchState(distance,_enemyEntity.Config.StopDistance,_enemyEntity.StateSwitcher);
        if(_enemyEntity.IsDie)
            StateSwitcher.SwitchState<DeathState>();
    }
    public override void Exit() => _enemyEntity.Agent.isStopped = true;
}
