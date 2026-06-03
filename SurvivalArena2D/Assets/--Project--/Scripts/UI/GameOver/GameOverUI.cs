using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;
using System;

public class GameOverUI : MonoBehaviour
{
    public event Action OnRestartClicked;
    public event Action OnBackToMenuClicked;

    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private GameObject _panel;
    [SerializeField] private RectTransform _windowRect;
    [SerializeField] private TextMeshProUGUI _currentTimeText;
    [SerializeField] private GameObject _newRecordLabel;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _backToMenuButton;

    [SerializeField] private int _gameSceneIndex = 1;
    [SerializeField] private int _menuSceneIndex = 0;

    private TimerService _timerService;
    private RecordsService _recordsService;
    private Character _character;
    private PauseManager _pauseManager;
    private ISceneLoader _sceneLoader;

    [Inject]
    public void Construct(TimerService timerService, RecordsService recordsService, Character character, PauseManager pauseManager, ISceneLoader sceneLoaderService)
    {
        _timerService = timerService;
        _recordsService = recordsService;
        _character = character;
        _pauseManager = pauseManager;
        _sceneLoader = sceneLoaderService;
    }

    private void Start()
    {
        _panel.gameObject.SetActive(false);
        _canvasGroup.interactable = false;
        _newRecordLabel.SetActive(false);

        _restartButton.onClick.AddListener(() => {
            OnRestartClicked?.Invoke();
            OnButtonClicked(_restartButton, RestartGame);
        });

        _backToMenuButton.onClick.AddListener(() => {
            OnBackToMenuClicked?.Invoke();
            OnButtonClicked(_backToMenuButton, BackToMenu);
        });

        _character.OnDied += ShowGameOver;
    }

    private void OnButtonClicked(Button button, Action action)
    {
        button.interactable = false;

        button.transform.DOPunchScale(new Vector3(-0.1f, -0.1f, 0), 0.2f)
            .SetUpdate(true)
            .OnComplete(() => {
                action?.Invoke();
            });
    }

    private void ShowGameOver()
    {
        _timerService.StopTimer();
        float finalTime = _timerService.CurrentTime;

        float oldRecord = _recordsService.GetRecord();
        bool isNewRecord = finalTime > oldRecord;

        if (isNewRecord)
        {
            _recordsService.TrySaveRecord(finalTime);
            _newRecordLabel.SetActive(true);
            _newRecordLabel.transform.DOPunchScale(Vector3.one * 0.2f, 0.5f).SetUpdate(true);
        }

        _currentTimeText.text = $"Продержался: {_timerService.GetFormattedTime()}";

        _panel.gameObject.SetActive(true);

        _canvasGroup.interactable = true;

        _pauseManager.SetPaused(true);

        _windowRect.anchoredPosition = new Vector2(0, -1000f);

        Sequence showSeq = DOTween.Sequence().SetUpdate(true);

        showSeq.Append(_canvasGroup.DOFade(1f, 0.3f));

        showSeq.Join(_windowRect.DOAnchorPos(Vector2.zero, 0.6f).SetEase(Ease.OutBack));

        showSeq.OnComplete(() => {
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        });
    }

    private void BackToMenu()
    {
        _pauseManager.SetPaused(false);
        _sceneLoader.LoadScene(_menuSceneIndex);
    }

    private void RestartGame()
    {
        _pauseManager.SetPaused(false);
        _sceneLoader.LoadScene(_gameSceneIndex);
    }

    private void OnDestroy()
    {
        if (_character != null) _character.OnDied -= ShowGameOver;
        _windowRect.DOKill();
    }
}
