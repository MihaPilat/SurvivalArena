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

    private int _pendingLevels;
    private bool _isWaitingForUpgrade;

    public void AddExperience(int amount)
    {
        if (amount <= 0) return;

        _currentExp += amount;
        Debug.Log($"Exp added: {amount}. Current total: {_currentExp}");

        CheckLevelUp();
    }

    public void ConfirmUpgrade()
    {
        CurrentLevel++;
        _pendingLevels--;
        _isWaitingForUpgrade = false;

        Debug.Log($"Level Confirmed! New Level: {CurrentLevel}. Pending: {_pendingLevels}");

        if (_pendingLevels > 0)
        {
            ShowUpgradeWindow();
        }
        else
        {
            CheckLevelUp();
        }
    }

    private void CheckLevelUp()
    {
        while (_currentExp >= _expToNextLevel)
        {
            _currentExp -= _expToNextLevel;
            _pendingLevels++;

            _expToNextLevel = Mathf.RoundToInt(_expToNextLevel * _levelMultiplier);
        }

        OnExpChanged?.Invoke(_currentExp, _expToNextLevel);

        if (_pendingLevels > 0 && !_isWaitingForUpgrade)
        {
            ShowUpgradeWindow();
        }
    }

    private void ShowUpgradeWindow()
    {
        _isWaitingForUpgrade = true;
        OnLevelUp?.Invoke(CurrentLevel + 1);
    }

    
}
