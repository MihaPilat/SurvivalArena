using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _currentTimeText;
    [SerializeField] private GameObject _newRecordLabel;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _backToMenuButton;

    private TimerService _timerService;
    private RecordsService _recordsService;
    private Character _character;
    private PauseManager _pauseManager;

    [Inject]
    public void Construct(TimerService timerService, RecordsService recordsService, Character character, PauseManager pauseManager)
    {
        _timerService = timerService;
        _recordsService = recordsService;
        _character = character;
        _pauseManager = pauseManager;
    }

    private void Start()
    {
        _panel.gameObject.SetActive(false);
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        _newRecordLabel.SetActive(false);

        _restartButton.onClick.AddListener(RestartGame);
        _backToMenuButton.onClick.AddListener(BackToMenu);
        _character.OnDied += ShowGameOver;
    }

    private void BackToMenu()
    {
        Debug.Log("Back to menu");
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
            _newRecordLabel.transform.DOPunchScale(Vector3.one * 0.2f, 0.5f);
        }

        _currentTimeText.text = $"Продержался: {_timerService.GetFormattedTime()}";

        _panel.gameObject.SetActive(true);

        _canvasGroup.DOFade(1f, 0.5f).SetUpdate(true);
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;

        _pauseManager.SetPaused(true);
    }

    private void RestartGame()
    {
        _pauseManager.SetPaused(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDestroy()
    {
        if (_character != null) _character.OnDied -= ShowGameOver;
    }
}