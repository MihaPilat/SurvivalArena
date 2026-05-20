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
            _enemyEntity.TriggerAttack();
            _lastAttackTime = Time.time;
        }
    }

    private Vector2 AddSpread(Vector2 direction, float spreadDegrees)
    {
        float randomAngle = Random.Range(-spreadDegrees, spreadDegrees);
        return Quaternion.Euler(0, 0, randomAngle) * direction;
    }
}
