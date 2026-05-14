using UnityEngine;
using Zenject;

public class ExperienceSpawner : MonoBehaviour
{
    [SerializeField] private ExperienceOrb _orbPrefab;
    private EnemyEntity _enemy;
    private PoolFactory _poolFactory;

    [Inject]
    private void Construct(PoolFactory poolFactory)
    {
        _poolFactory = poolFactory;
    }

    private void Awake() => _enemy = GetComponent<EnemyEntity>();

    private void OnEnable()
    {
        if (_enemy != null)
            _enemy.OnDied += SpawnExperience;
    }

    private void OnDisable()
    {
        if (_enemy != null)
            _enemy.OnDied -= SpawnExperience;
    }

    private void SpawnExperience()
    {
        var orb = _poolFactory.Get(_orbPrefab);
        orb.transform.position = transform.position;

        orb.Init(_enemy.Config.ExpAmount, _orbPrefab, _poolFactory);
    }
}
