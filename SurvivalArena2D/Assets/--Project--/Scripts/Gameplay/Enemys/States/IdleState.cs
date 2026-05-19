using UnityEngine;

public class IdleState : EnemyState
{
    private float _timer;
    public IdleState(EnemyEntity enemyEntity) : base(enemyEntity)
    {
    }

    public override void Enter()
    {
        _timer = 0f;
        View.StartIdling();
    }

    public override void Exit()
    {
        View.StopIdling();
    }

    public override void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _enemyEntity.Config.WaitingTime)
        {
            StateSwitcher.SwitchState<ChaseState>();
        }
        if (_enemyEntity.IsDie)
            StateSwitcher.SwitchState<DeathState>();
    }
}
