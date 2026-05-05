using UnityEngine;

public class ExperienceSpawner : MonoBehaviour
{
    [SerializeField] private ExperienceOrb _orbPrefab;

    private EnemyEntity _enemy;

    private void Awake()
    {
        _enemy = GetComponent<EnemyEntity>();
    }

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
        var orb = Instantiate(_orbPrefab, transform.position, Quaternion.identity);
        orb.Init(_enemy.Config.ExpAmount);
    }
}