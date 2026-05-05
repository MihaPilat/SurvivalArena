using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class LevelUpScreen : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private RectTransform _panelRect;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private UpgradeCard[] _cards;

    private float _outOfScreenY = -1200f;

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

        _panelRect.anchoredPosition = new Vector2(0, _outOfScreenY);
        _canvasGroup.alpha = 0;

        _panelRect.DOAnchorPos(Vector2.zero, 0.6f)
            .SetEase(Ease.OutBack)
            .SetUpdate(true);

        _canvasGroup.DOFade(1, 0.4f).SetUpdate(true);

        AnimateCards();
    }

    private void Hide()
    {
        _panelRect.DOAnchorPos(new Vector2(0, _outOfScreenY), 0.4f)
            .SetEase(Ease.InBack)
            .SetUpdate(true)
            .OnComplete(() => {
                _panelRect.gameObject.SetActive(false);
                _pauseManager.SetPaused(false);
            });

        _canvasGroup.DOFade(0, 0.3f).SetUpdate(true);
    }

    private void AnimateCards()
    {
        for (int i = 0; i < _cards.Length; i++)
        {
            Transform cardTransform = _cards[i].transform;

            cardTransform.localScale = Vector3.zero;

            cardTransform.localRotation = Quaternion.Euler(0, 0, 15f); // Изначально наклонена
            cardTransform.DORotate(Vector3.zero, 0.6f)
                .SetEase(Ease.OutBack, 4.0f) // Сильно пружиним вращение к нулю
                .SetUpdate(true);
        }
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
