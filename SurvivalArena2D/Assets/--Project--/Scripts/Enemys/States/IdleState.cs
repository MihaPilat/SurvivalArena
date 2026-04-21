using System.Collections;
using System.Collections.Generic;
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
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _enemyEntity.Config.WaitingTime)
        {
            StateSwitcher.SwitchState<ChaseState>();
        }
    }
}
