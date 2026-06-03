using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Zenject;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    public event Action OnPlayClicked;
    public event Action OnSettingsClicked;
    public event Action OnExitClicked;
    public event Action OnSettingsClosed;
    public event Action OnResetClicked;

    [SerializeField] private RectTransform _mainPanelRect;
    [SerializeField] private RectTransform _settingsPanelRect;

    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;

    [SerializeField] private Button _closeSettingsButton;
    [SerializeField] private Button _resetButton;

    [SerializeField] private TextMeshProUGUI _bestRecordText;

    [SerializeField] private int _gameSceneIndex = 1;

    private RecordsService _recordsService;
    private AudioService _audioService;

    [Inject]
    public void Construct(RecordsService recordsService, AudioService audioService)
    {
        _recordsService = recordsService;
        _audioService = audioService;
    }

    private void Start()
    {
        _settingsPanelRect.anchoredPosition = new Vector2(2000f, 0f);

        DisplayBestRecord();

        _playButton.onClick.AddListener(() => {
            OnPlayClicked?.Invoke();
            AnimateButtonClick(_playButton, StartGame);
        });

        _settingsButton.onClick.AddListener(() => {
            OnSettingsClicked?.Invoke();
            AnimateButtonClick(_settingsButton, OpenSettings);
        });

        _exitButton.onClick.AddListener(() => {
            OnExitClicked?.Invoke();
            AnimateButtonClick(_exitButton, ExitGame);
        });

        _closeSettingsButton.onClick.AddListener(() => {
            OnSettingsClosed?.Invoke();
            AnimateButtonClick(_closeSettingsButton, CloseSettings);
        });

        _resetButton.onClick.AddListener(() => {
            OnResetClicked?.Invoke();
            AnimateButtonClick(_resetButton, ResetGameProgress);
        });
    }
    private void ResetGameProgress()
    {
        _audioService.ResetToDefaultVolumes();

        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        DisplayBestRecord();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void DisplayBestRecord()
    {
        float recordTime = _recordsService.GetRecord();

        int minutes = Mathf.FloorToInt(recordTime / 60F);
        int seconds = Mathf.FloorToInt(recordTime % 60F);

        _bestRecordText.text = $"Рекорд: {minutes:00}:{seconds:00}";
    }

    private void AnimateButtonClick(Button button, Action onComplete)
    {
        button.interactable = false;

        button.transform.DOPunchScale(new Vector3(-0.1f, -0.1f, 0f), 0.15f)
            .OnComplete(() => {
                button.interactable = true;
                onComplete?.Invoke();
            });
    }

    private void StartGame() => SceneManager.LoadScene(_gameSceneIndex);

    private void OpenSettings()
    {
        _mainPanelRect.DOAnchorPos(new Vector2(-2000f, 0f), 0.5f).SetEase(Ease.OutCubic);
        _settingsPanelRect.DOAnchorPos(Vector2.zero, 0.5f).SetEase(Ease.OutCubic);
    }

    private void CloseSettings()
    {
        _mainPanelRect.DOAnchorPos(Vector2.zero, 0.5f).SetEase(Ease.OutCubic);
        _settingsPanelRect.DOAnchorPos(new Vector2(2000f, 0f), 0.5f).SetEase(Ease.OutCubic);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
