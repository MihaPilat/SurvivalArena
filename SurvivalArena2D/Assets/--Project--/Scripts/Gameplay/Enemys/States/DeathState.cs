using UnityEngine;

public class DeathState : EnemyState
{
    public DeathState(EnemyEntity enemyEntity) : base(enemyEntity)
    {
    }

    public override void Enter()
    {
        View.StartDead();
        _enemyEntity.Agent.enabled = false;

        if (_enemyEntity.TryGetComponent(out Collider2D collider))
        {
            collider.enabled = false;
        }
    }

    public override void Exit()
    {
        View.StopDead();
        _enemyEntity.Agent.enabled = true;

        if (_enemyEntity.TryGetComponent(out Collider2D collider))
        {
            collider.enabled = true;
        }
    }

    public override void Update()
    {
        if(_enemyEntity.IsDie==false)
            StateSwitcher.SwitchState<ChaseState>();
    }
}
