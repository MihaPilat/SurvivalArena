using Zenject;
using System;

public class GameStartController : IInitializable, IDisposable
{
    private readonly IWaveHandler _waveHandler;
    private readonly TimerService _timerService;

    public GameStartController(IWaveHandler waveHandler, TimerService timerService)
    {
        _waveHandler = waveHandler;
        _timerService = timerService;
    }

    public void Initialize()
    {
        _waveHandler.OnWaveStarted += HandleWaveStarted;
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