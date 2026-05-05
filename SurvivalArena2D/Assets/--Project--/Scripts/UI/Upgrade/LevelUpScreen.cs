using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class LevelUpScreen : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private UpgradeCard[] _cards;

    private ILevelable _levelSystem;
    private PauseManager _pauseManager;
    private List<UpgradeData> _allUpgrades;
    private Character _character;

    [Inject]
    public void Construct(ILevelable levelSystem, PauseManager pauseManager, List<UpgradeData> upgrades, Character character)
    {
        _levelSystem = levelSystem;
        _pauseManager = pauseManager;
        _allUpgrades = upgrades;
        _character = character;
    }

    private void OnEnable()
    {
        _levelSystem.OnLevelUp += HandleLevelUp;
    }

    private void OnDisable()
    {
        _levelSystem.OnLevelUp -= HandleLevelUp;
    }

    private void HandleLevelUp(int newLevel)
    {
        DOVirtual.DelayedCall(0.5f, Show).SetUpdate(true);
    }

    public void Show()
    {
        _panel.SetActive(true);
        _pauseManager.SetPaused(true);

        SetupUpgrades();
    }

    private void Hide()
    {
        _panel.SetActive(false);
        _pauseManager.SetPaused(false);
    }

    private void SetupUpgrades()
    {
        var selectedUpgrades = _allUpgrades
            .OrderBy(x => Random.value)
            .Take(_cards.Length)
            .ToList();

        for (int i = 0; i < _cards.Length; i++)
        {
            if (i < selectedUpgrades.Count)
            {
                _cards[i].gameObject.SetActive(true);
                _cards[i].Setup(selectedUpgrades[i], SelectUpgrade);
            }
            else
            {
                _cards[i].gameObject.SetActive(false);
            }
        }
    }
    private void SelectUpgrade(UpgradeData upgrade)
    {
        upgrade.Apply(_character);
        Hide();
    }
}
