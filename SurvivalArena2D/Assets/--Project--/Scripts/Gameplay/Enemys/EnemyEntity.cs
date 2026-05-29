using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Random = UnityEngine.Random;
[RequireComponent(typeof(NavMeshAgent))]

public abstract class EnemyEntity : MonoBehaviour, IDamageable
{
    public event Action OnHit;
    public event Action OnDied;
    public event Action OnAttack;
    public event Action OnSpawned;

    [SerializeField] private EnemyConfig _config;
    [SerializeField] private EnemyView _enemyView;

    [SerializeField] private Transform _projectileSpawnPoint;

    protected StateMachine _stateMachine;
    protected Character _character;
    private NavMeshAgent _agent;

    private PoolFactory _originFactory;
    private EnemyEntity _originPrefab;

    private float _deathDelay = 2f;

    public EnemyView View => _enemyView;
    public NavMeshAgent Agent => _agent;
    public Character Character => _character;
    public EnemyConfig Config => _config;
    public Transform Target => _character.transform;
    public Transform ProjectileSpawnPoint => _projectileSpawnPoint;

    public bool IsDie => _isDie;
    public int Damage => _config.Damage;

    public PoolFactory PoolFactory => _originFactory;

    private bool _isDie;
    public IStateSwitcher StateSwitcher => _stateMachine;

    private float _currentHp;

    [Inject]
    protected void Construct(Character character)
    {
        _character = character;
    }

    public void Init(EnemyEntity prefab, PoolFactory factory)
    {
        _originPrefab = prefab;
        _originFactory = factory;
        _isDie = false;
        _currentHp = _config.Health;

        OnSpawned?.Invoke();
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        _stateMachine = new StateMachine(AddStates());
        _agent.speed = _config.Speed;
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.avoidancePriority = Random.Range(30, 60);
        _currentHp = _config.Health;
    }
    private void Update()
    {
        _stateMachine.Update();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleContactDamage(collision);
    }


    public void TakeDamage(int damage)
    {
        if (IsDie || damage <= 0) return;

        _currentHp -= damage;

        Debug.Log($"Enemy took {damage} damage. Health: {_currentHp}");

        if (_currentHp <= 0)
            Die();
        else
            OnHit?.Invoke();
    }
    public void TriggerAttack()
    {
        OnAttack?.Invoke();
    }

    public void PerformRangeAttack()
    {
        if (IsDie || _character == null) return;

        Vector3 spawnPosition = _projectileSpawnPoint != null ? _projectileSpawnPoint.position : transform.position;

        Vector2 direction = (Target.position - spawnPosition).normalized;
        Projectile prefabComponent = _config.ProjectilePrefab.GetComponent<Projectile>();
        Projectile projectile = _originFactory.Get<Projectile>(prefabComponent);

        projectile.transform.position = spawnPosition;
        projectile.SetPoolData(prefabComponent, _originFactory);

        float randomAngle = Random.Range(-_config.Spread, _config.Spread);
        Vector2 spreadDirection = Quaternion.Euler(0, 0, randomAngle) * direction;

        projectile.Init(spreadDirection, _config, Damage);
    }

    public void TeleportTo(Vector3 position)
    {
        if (_agent != null)
        {
            _agent.Warp(position);
        }
        else
        {
            transform.position = position;
        }
    }

    protected abstract List<IState> AddStates();
    private void HandleContactDamage(Collider2D other)
    {
        if (IsDie) return;

        if (other.TryGetComponent(out IDamageable damageable))
        {
            if (other.GetComponent<Character>() != null)
            {
                damageable.TakeDamage(_config.ContactDamage);
                Debug.Log($"Enemy dealt {_config.ContactDamage} contact damage to Player");
            }
        }
    }
    private void Die()
    {
        if (_isDie)
            return;
        _isDie = true;
        OnDied?.Invoke();

        OnDied = null;

        StartCoroutine(DeathCoroutine());
    }

    private IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(_deathDelay);
        _originFactory.Reclaim(this, _originPrefab);
    }
}
