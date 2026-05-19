using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState: IState
{
    protected readonly EnemyEntity _enemyEntity;
    protected EnemyView View => _enemyEntity.View;

    protected IStateSwitcher StateSwitcher => _enemyEntity.StateSwitcher;

    protected EnemyState(EnemyEntity enemyEntity)
    {
        _enemyEntity = enemyEntity;
    }
    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
