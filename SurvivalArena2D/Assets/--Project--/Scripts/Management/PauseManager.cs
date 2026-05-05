using System;
using UnityEngine;

public class PauseManager : IPauseHandler
{
    public event Action<bool> OnPauseChanged;
    public bool IsPaused { get; private set; }

    public void SetPaused(bool isPaused)
    {
        if (IsPaused == isPaused) return;

        IsPaused = isPaused;
        Time.timeScale = isPaused ? 0f : 1f;

        OnPauseChanged?.Invoke(IsPaused);
        Debug.Log(isPaused ? "Game Paused" : "Game Resumed");
    }
}
