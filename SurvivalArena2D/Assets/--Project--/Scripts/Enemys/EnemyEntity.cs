using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]

public abstract class EnemyEntity : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyConfig _config;

    protected StateMachine _stateMachine;
    protected Character _character;
    private NavMeshAgent _agent;

    public NavMeshAgent Agent => _agent;
    public Character Character => _character;
    public EnemyConfig Config => _config;

    public IStateSwitcher StateSwitcher => _stateMachine;
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
    }
    private void Update()
    {
        _stateMachine.Update();
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("AAAAAAAAAAA");
    }
    protected abstract List<IState> AddStates();
}
