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
    private bool _isClosing;

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
        if (_panel.activeSelf) return;

        DOVirtual.DelayedCall(0.5f, Show).SetUpdate(true);
    }

    public void Show()
    {
        _isClosing = false;
        _canvasGroup.interactable = true;
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
        _isClosing = true;
        _canvasGroup.interactable = false;

        _panelRect.DOAnchorPos(new Vector2(0, _outOfScreenY), 0.4f)
            .SetEase(Ease.InBack)
            .SetUpdate(true)
            .OnComplete(() => {
                _panelRect.gameObject.SetActive(false);
                _pauseManager.SetPaused(false);
                _isClosing = false;
            });

        _canvasGroup.DOFade(0, 0.3f).SetUpdate(true);
    }

    private void AnimateCards()
    {
        for (int i = 0; i < _cards.Length; i++)
        {
            if (!_cards[i].gameObject.activeSelf) continue;

            Transform cardTransform = _cards[i].transform;
            cardTransform.DOKill();
            cardTransform.localScale = Vector3.zero;
            cardTransform.localRotation = Quaternion.Euler(0, 0, 15f);

            cardTransform.DOScale(Vector3.one, 0.5f)
                .SetEase(Ease.OutBack)
                .SetUpdate(true);

            cardTransform.DORotate(Vector3.zero, 0.6f)
                .SetEase(Ease.OutBack, 4.0f)
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
        if (_isClosing) return;
        _isClosing = true;
        _canvasGroup.interactable = false;

        upgrade.Apply(_character);
        ((LevelSystem)_levelSystem).ConfirmUpgrade();

        UpgradeCard selectedCard = _cards.FirstOrDefault(c => c.CurrentUpgrade == upgrade);

        if (selectedCard != null)
        {
            Sequence sequence = DOTween.Sequence().SetUpdate(true);
            sequence.Append(selectedCard.transform.DOScale(1.1f, 0.1f).SetEase(Ease.OutQuad));
            sequence.Append(selectedCard.transform.DOScale(0f, 0.2f).SetEase(Ease.InBack));

            sequence.OnComplete(() =>
            {
                Hide();
            });
        }
        else
        {
            Hide();
        }
    }
}
