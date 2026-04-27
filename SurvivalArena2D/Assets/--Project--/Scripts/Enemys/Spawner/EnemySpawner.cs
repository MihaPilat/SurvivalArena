using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<EnemySpawnData> _allEnemies;
    [SerializeField] private Transform[] _spawnPoints;

    [SerializeField] private float _spawnInterval = 10f;
    [SerializeField] int _budgetGenerationRate = 10;

    private float _dangerTimer;
    private float _spawnTimer;
    private int _currentDangerLevel = 1;
    private float _spawnBudget;

    void Update()
    {
        _dangerTimer += Time.deltaTime;
        if (_dangerTimer >= 60f)
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
            SpawnBatch();
        }
    }
    private void SpawnBatch()
    {
        int cheapestCost = GetCheapestEnemyCost();

        int safetyBreak = 0;
        while (_spawnBudget >= cheapestCost && safetyBreak < 50)
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

        var enemyToSpawn = availableEnemies[Random.Range(0, availableEnemies.Count)];

        Instantiate(enemyToSpawn.Prefab, GetRandomSpawnPoint(), Quaternion.identity);

        _spawnBudget -= enemyToSpawn.Cost;
        return true;
    }

    private int GetCheapestEnemyCost()
    {
        if (_allEnemies == null || _allEnemies.Count == 0) return 0;
        return _allEnemies.Min(e => e.Cost);
    }

    private Vector3 GetRandomSpawnPoint()
    {
        if (_spawnPoints == null || _spawnPoints.Length == 0) return transform.position;
        return _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
    }
}
