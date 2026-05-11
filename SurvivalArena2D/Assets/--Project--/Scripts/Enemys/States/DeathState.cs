using UnityEngine;

public class DeathState : EnemyState
{
    public DeathState(EnemyEntity enemyEntity) : base(enemyEntity)
    {
    }

    public override void Enter()
    {
        _enemyEntity.Agent.enabled = false;

        if (_enemyEntity.TryGetComponent(out Collider2D collider))
        {
            collider.enabled = false;
        }
        Object.Destroy(_enemyEntity.gameObject, 2f);
        //потом возращение в пулл
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
    }
}
