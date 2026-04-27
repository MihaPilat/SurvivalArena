using UnityEngine;
using Random = UnityEngine.Random;

public class RangeAttackState : EnemyState
{
    private float _lastAttackTime;

    public RangeAttackState(EnemyEntity enemyEntity) : base(enemyEntity)
    {
    }

    public override void Enter()
    {
        _enemyEntity.Agent.isStopped = true;
    }

    public override void Exit()
    {
        _enemyEntity.Agent.isStopped = false;

        _lastAttackTime = Time.time + Random.Range(0, 2f);
    }

    public override void Update()
    {
        if (_enemyEntity.IsDie)
        {
            StateSwitcher.SwitchState<DeathState>();
            return;
        }

        float distance = Vector3.Distance(_enemyEntity.transform.position, _enemyEntity.Target.position);

        if (distance > _enemyEntity.Config.MaxAttackRange)
            StateSwitcher.SwitchState<ChaseState>();

        if (Time.time >= _lastAttackTime + _enemyEntity.Config.AttackCooldown)
        {
            Attack();
            _enemyEntity.TriggerAttack();
            _lastAttackTime = Time.time;
        }
    }
    private void Attack()
    {
        Vector2 direction = (_enemyEntity.Target.position - _enemyEntity.transform.position).normalized;

        GameObject projectileObj = Object.Instantiate(
            _enemyEntity.Config.ProjectilePrefab,
            _enemyEntity.transform.position,
            Quaternion.identity
        );
        if (projectileObj.TryGetComponent(out Projectile projectile))
        {
            Vector2 spreadDirection = AddSpread(direction, _enemyEntity.Config.Spread);

            projectile.Init(spreadDirection, _enemyEntity.Config);
        }
    }

    private Vector2 AddSpread(Vector2 direction, float spreadDegrees)
    {
        float randomAngle = Random.Range(-spreadDegrees, spreadDegrees);
        return Quaternion.Euler(0, 0, randomAngle) * direction;
    }
}
