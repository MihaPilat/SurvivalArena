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
        View.StartIdling();
    }

    public override void Exit()
    {
        _enemyEntity.Agent.isStopped = false;

        _lastAttackTime = Time.time + Random.Range(0, 2f);
        View.StopIdling();
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

        Projectile prefabComponent = _enemyEntity.Config.ProjectilePrefab.GetComponent<Projectile>();

        Projectile projectile = _enemyEntity.PoolFactory.Get<Projectile>(prefabComponent);

        projectile.transform.position = _enemyEntity.transform.position;

        projectile.SetPoolData(prefabComponent, _enemyEntity.PoolFactory);

        Vector2 spreadDirection = AddSpread(direction, _enemyEntity.Config.Spread);

        projectile.Init(spreadDirection, _enemyEntity.Config, _enemyEntity.Damage);
    }

    private Vector2 AddSpread(Vector2 direction, float spreadDegrees)
    {
        float randomAngle = Random.Range(-spreadDegrees, spreadDegrees);
        return Quaternion.Euler(0, 0, randomAngle) * direction;
    }
}
