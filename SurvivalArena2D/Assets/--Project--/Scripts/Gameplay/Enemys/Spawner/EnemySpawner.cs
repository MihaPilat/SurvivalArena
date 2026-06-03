using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour, IWaveHandler
{
    public event Action<int> OnWaveStarted;
    public event Action<float> OnPreWaveCountdown;

    [SerializeField] private List<EnemySpawnData> _allEnemies;

    [SerializeField] private float _spawnInterval = 10f;
    [SerializeField] int _budgetGenerationRate = 10;

    [SerializeField] private float _minSpawnRadius = 10f;
    [SerializeField] private float _maxSpawnRadius = 20f;

    [SerializeField] private int _maxEnemiesOnMap = 70;
    [SerializeField] private float _dangerUpgradeInterval=30f;
    private Transform _characterTransform;

    private float _dangerTimer;
    private float _spawnTimer;
    private int _currentDangerLevel = 1;
    private float _spawnBudget;
    private int _currentWave = 0;
    private int _currentEnemiesCount = 0;

    private bool _firstWaveStarted = false;
    private float _startDelayTimer = 5f;

    private PoolFactory _enemyFactory;

    [Inject]
    private void Construct(Character character, PoolFactory enemyFactory)
    {
        _characterTransform = character.transform;
        _enemyFactory = enemyFactory;
    }

    private void Start()
    {
        PreWarmPools();
        _spawnBudget = _startDelayTimer * (_budgetGenerationRate * _currentDangerLevel);
    }
    void Update()
    {
        if (!_firstWaveStarted)
        {
            _startDelayTimer -= Time.deltaTime;

            OnPreWaveCountdown?.Invoke(_startDelayTimer);

            if (_startDelayTimer <= 0)
            {
                _firstWaveStarted = true;
                _currentWave = 1;
                OnWaveStarted?.Invoke(_currentWave);
                SpawnBatch();
            }
            return;
        }

        _dangerTimer += Time.deltaTime;
        if (_dangerTimer >= _dangerUpgradeInterval)
        {
            _dangerTimer = 0;
            _currentDangerLevel++;
            Debug.Log($"Level Increased: {_currentDangerLevel}");
        }
        _spawnBudget += Time.deltaTime * (_budgetGenerationRate * _currentDangerLevel);

        _spawnTimer += Time.deltaTime;
        if (_spawnTimer >= _spawnInterval)
        {
            _spawnTimer = 0;
            _currentWave++;
            OnWaveStarted?.Invoke(_currentWave);
            SpawnBatch();
        }
    }

    private void PreWarmPools()
    {
        if (_allEnemies == null || _allEnemies.Count == 0)
            return;


        foreach (var enemyData in _allEnemies)
        {
            if (enemyData.Prefab == null) continue;

            EnemyEntity prefabComponent = enemyData.Prefab.GetComponent<EnemyEntity>();
            if (prefabComponent == null) continue;

            EnemyEntity temporaryEnemy = _enemyFactory.Get<EnemyEntity>(prefabComponent);

            _enemyFactory.Reclaim(temporaryEnemy, prefabComponent);

            if (prefabComponent.Config != null && prefabComponent.Config.ProjectilePrefab != null)
            {
                Projectile projectilePrefab = prefabComponent.Config.ProjectilePrefab.GetComponent<Projectile>();
                if (projectilePrefab != null)
                {
                    Projectile temporaryProjectile = _enemyFactory.Get<Projectile>(projectilePrefab);

                    _enemyFactory.Reclaim(temporaryProjectile, projectilePrefab);
                }
            }
        }
    }

    private void SpawnBatch()
    {
        int cheapestCost = GetCheapestEnemyCost();

        int safetyBreak = 0;
        while (_spawnBudget >= cheapestCost && _currentEnemiesCount < _maxEnemiesOnMap && safetyBreak < 50)
        {
            if (!TrySpawnEnemy()) break;
            safetyBreak++;
        }
    }
    private bool TrySpawnEnemy()
    {
        var availableEnemies = _allEnemies
            .Where(e => e.MinDangerLevel <= _currentDangerLevel && e.Cost <= _spawnBudget)
            .ToList();

        if (availableEnemies.Count == 0) return false;

        var enemyData = availableEnemies[Random.Range(0, availableEnemies.Count)];

        EnemyEntity prefabComponent = enemyData.Prefab.GetComponent<EnemyEntity>();
        EnemyEntity enemy = _enemyFactory.Get<EnemyEntity>(prefabComponent);

        enemy.Init(prefabComponent, _enemyFactory);

        enemy.TeleportTo(GetRandomSpawnPoint());

        _currentEnemiesCount++;
        enemy.OnDied += OnEnemyDestroyed;

        _spawnBudget -= enemyData.Cost;
        return true;
    }

    private void OnEnemyDestroyed()
    {
        _currentEnemiesCount--;

        if (_currentEnemiesCount < 0)
            _currentEnemiesCount = 0;
    }

    private int GetCheapestEnemyCost() => _allEnemies?.Min(e => e.Cost) ?? 0;

    private Vector3 GetRandomSpawnPoint()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector2 randomDir = Random.insideUnitCircle.normalized;
            float randomDist = Random.Range(_minSpawnRadius, _maxSpawnRadius);
            Vector3 randomPoint = _characterTransform.position + (Vector3)(randomDir * randomDist);

            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 3f, NavMesh.AllAreas))
            {
                if (Vector3.Distance(_characterTransform.position, hit.position) >= _minSpawnRadius)
                {
                    return hit.position;
                }
            }
        }
        return transform.position;
    }
}
