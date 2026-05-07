using System;

public interface IWaveHandler
{
    event Action<int> OnWaveStarted;
}
