using System;
using UnityEngine;

public class LevelSystem : ILevelable
{
    public event Action<int, int> OnExpChanged;
    public event Action<int> OnLevelUp;

    public int CurrentLevel { get; private set; } = 1;

    private int _currentExp;
    private int _expToNextLevel = 100;
    private float _levelMultiplier = 1.2f;

    public void AddExperience(int amount)
    {
        if (amount <= 0) return;

        _currentExp += amount;

        while (_currentExp >= _expToNextLevel)
        {
            LevelUp();
        }

        OnExpChanged?.Invoke(_currentExp, _expToNextLevel);
    }

    private void LevelUp()
    {
        _currentExp -= _expToNextLevel;
        CurrentLevel++;

        _expToNextLevel = Mathf.RoundToInt(_expToNextLevel * _levelMultiplier);

        OnLevelUp?.Invoke(CurrentLevel);
        Debug.Log($"New Level: {CurrentLevel}");
    }
}
