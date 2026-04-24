using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]

public abstract class EnemyEntity : MonoBehaviour, IDamageable
{
    public event Action OnHit;
    public event Action OnDied;

    [SerializeField] private EnemyConfig _config;

    protected StateMachine _stateMachine;
    protected Character _character;
    private NavMeshAgent _agent;

    public NavMeshAgent Agent => _agent;
    public Character Character => _character;
    public EnemyConfig Config => _config;
    public Transform Target => _character.transform;

    public bool IsDie;
    public IStateSwitcher StateSwitcher => _stateMachine;

    private float _currentHp;

    [Inject]
    protected void Construct(Character character)
    {
        _character = character;
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
        _currentHp -= damage;

        Debug.Log($"Enemy took {damage} damage. Health: {_currentHp}");

        if (_currentHp <= 0)
            Die();
        else
            OnHit?.Invoke();
    }
    private void Die()
    {
        IsDie = true;
        OnDied?.Invoke();
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
}
