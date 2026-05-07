using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class LootSpawner: IInitializable, IDisposable
{
    private readonly LootConfig _config;
    private readonly List<UpgradeData> _allUpgrades;
    private readonly List<Transform> _spawnPoints;
    private readonly List<Transform> _specialSpawnPoints;

    private readonly IInstantiator _instantiator;
    private readonly IWaveHandler _waveHandler;

    private GameObject _currentActivePickup;

    public LootSpawner(
        IInstantiator instantiator,
        IWaveHandler waveHandler,
        List<UpgradeData> upgrades,
        LootConfig config,
        [Inject(Id = "DefaultPoints")] List<Transform> spawnPoints,
        [Inject(Id = "SpecialPoints")] List<Transform> specialPoints)
    {
        _instantiator = instantiator;
        _waveHandler = waveHandler;
        _allUpgrades = upgrades;
        _config = config;
        _spawnPoints = spawnPoints;
        _specialSpawnPoints = specialPoints;
    }

    public void Initialize()
    {
        _waveHandler.OnWaveStarted += HandleWaveStarted;
    }

    public void Dispose()
    {
        _waveHandler.OnWaveStarted -= HandleWaveStarted;
    }

    private void HandleWaveStarted(int waveNumber)
    {
        if (_config == null) { Debug.LogError("LootSpawner: _config is NULL!"); return; }
        if (_config.SpecialEvents == null) { Debug.LogError("LootSpawner: SpecialEvents list is NULL!"); return; }
        if (_spawnPoints == null) { Debug.LogError("LootSpawner: _spawnPoints is NULL!"); return; }
        if (_specialSpawnPoints == null) { Debug.LogError("LootSpawner: _specialSpawnPoints is NULL!"); return; }
        var specialEvent = _config.SpecialEvents.Find(e => e.TargetWave == waveNumber);
        if (specialEvent.PrefabToSpawn != null)
        {
            SpawnSpecialItem(specialEvent);
        }

        bool isCorrectWave = (waveNumber == 1 || (waveNumber - 1) % 6 == 0);

        bool noActivePickup = _currentActivePickup == null;

        if (isCorrectWave && noActivePickup)
        {
            SpawnRandomUpgrade();
        }
        Debug.Log($"Wave received: {waveNumber}. Correct wave: {isCorrectWave}. No active: {noActivePickup}");
    }
    private void SpawnSpecialItem(SpecialSpawnEvent ev)
    {
        if (_specialSpawnPoints == null || _specialSpawnPoints.Count == 0)
        {
            Debug.LogError("Special Spawn Points are missing!");
            return;
        }

        var point = _specialSpawnPoints[UnityEngine.Random.Range(0, _specialSpawnPoints.Count)];
        _instantiator.InstantiatePrefab(ev.PrefabToSpawn, point.position, Quaternion.identity, null);
    }
    private void SpawnRandomUpgrade()
    {
        if (_config.DefaultPickupPrefab == null || _spawnPoints == null || _spawnPoints.Count == 0) return;

        var point = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
        var data = _allUpgrades[Random.Range(0, _allUpgrades.Count)];

        _currentActivePickup = _instantiator.InstantiatePrefab(_config.DefaultPickupPrefab, point.position, Quaternion.identity, null);

        if (_currentActivePickup.TryGetComponent(out UpgradePickup pickup))
        {
            pickup.Setup(data);
        }
    }
}
