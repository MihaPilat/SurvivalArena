using Zenject;
using System;

public class GameStartController : IInitializable, IDisposable
{
    private readonly IWaveHandler _waveHandler;
    private readonly TimerService _timerService;
    private readonly PoolFactory _poolFactory;
    private readonly ExplosionEffect _explosionPrefab;

    public GameStartController(
        IWaveHandler waveHandler,
        TimerService timerService,
        PoolFactory poolFactory,
        ExplosionEffect explosionPrefab)
    {
        _waveHandler = waveHandler;
        _timerService = timerService;
        _poolFactory = poolFactory;
        _explosionPrefab = explosionPrefab;
    }

    public void Initialize()
    {
        _waveHandler.OnWaveStarted += HandleWaveStarted;
        PrewarmExplosion();
    }

    private void PrewarmExplosion()
    {
        if (_explosionPrefab == null) return;

        ExplosionEffect tempEffect = _poolFactory.Get<ExplosionEffect>(_explosionPrefab);

        tempEffect.SetPoolData(_explosionPrefab, _poolFactory);
        _poolFactory.Reclaim<ExplosionEffect>(tempEffect, _explosionPrefab);
    }

    private void HandleWaveStarted(int waveNumber)
    {
        if (waveNumber == 1)
        {
            _timerService.StartTimer();
            _waveHandler.OnWaveStarted -= HandleWaveStarted;
        }
    }

    public void Dispose()
    {
        _waveHandler.OnWaveStarted -= HandleWaveStarted;
    }
}
