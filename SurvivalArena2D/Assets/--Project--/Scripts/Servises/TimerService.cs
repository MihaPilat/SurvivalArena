using UnityEngine;
using Zenject;

public class TimerService : ITickable
{
    public float CurrentTime { get; private set; }
    public bool IsRunning { get; private set; }

    public void StartTimer() => IsRunning = true;
    public void StopTimer() => IsRunning = false;
    public void ResetTimer() => CurrentTime = 0;

    public void Tick()
    {
        if (IsRunning)
        {
            CurrentTime += Time.deltaTime;
        }
    }

    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(CurrentTime / 60);
        int seconds = Mathf.FloorToInt(CurrentTime % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
