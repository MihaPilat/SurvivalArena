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

    private int _storedExperience;
    private bool _isWaitingForUpgrade;

    public void AddExperience(int amount)
    {
        if (amount <= 0) return;

        _storedExperience += amount;
        Debug.Log($"Exp added: {amount}. Total stored: {_storedExperience}");

        TryProcessExperience();
    }

    private void TryProcessExperience()
    {
        if (_isWaitingForUpgrade) return;

        if (_storedExperience > 0)
        {
            _currentExp += _storedExperience;
            _storedExperience = 0;
        }

        if (_currentExp >= _expToNextLevel)
        {
            int extra = _currentExp - _expToNextLevel;
            _currentExp = _expToNextLevel;
            _storedExperience += extra;

            TriggerLevelUp();
        }

        OnExpChanged?.Invoke(_currentExp, _expToNextLevel);
    }

    private void TriggerLevelUp()
    {
        if (_isWaitingForUpgrade) return;

        _isWaitingForUpgrade = true;

        OnLevelUp?.Invoke(CurrentLevel + 1);
    }

    public void ConfirmUpgrade()
    {
        CurrentLevel++;

        _currentExp = 0;

        _expToNextLevel = Mathf.RoundToInt(_expToNextLevel * _levelMultiplier);

        _isWaitingForUpgrade = false;

        Debug.Log($"Level Confirmed! New Level: {CurrentLevel}. Next goal: {_expToNextLevel}");

        TryProcessExperience();
    }
}