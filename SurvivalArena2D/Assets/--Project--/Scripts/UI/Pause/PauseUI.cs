using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Zenject;

public class PauseUI : MonoBehaviour
{
    public event Action OnPauseOpened;
    public event Action OnPauseClosed;
    public event Action OnResumeClicked;
    public event Action OnExitClicked;

    [SerializeField] private GameObject _panel;
    [SerializeField] private RectTransform _windowRect;
    [SerializeField] private CanvasGroup _canvasGroup;

    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _exitButton;

    [SerializeField] private int _menuSceneIndex = 0;

    private PauseManager _pauseManager;
    private IInput _input;
    private ISceneLoader _sceneLoader;

    [Inject]
    public void Construct(PauseManager pauseManager, IInput input, ISceneLoader sceneLoader)
    {
        _pauseManager = pauseManager;
        _input = input;
        _sceneLoader = sceneLoader;
    }

    private void Start()
    {
        _panel.SetActive(false);
        _canvasGroup.alpha = 0f;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

        _resumeButton.onClick.AddListener(() => {
            OnResumeClicked?.Invoke();
            AnimateButtonClick(_resumeButton, ResumeGame);
        });

        _exitButton.onClick.AddListener(() => {
            OnExitClicked?.Invoke();
            AnimateButtonClick(_exitButton, ExitToMenu);
        });

        _input.OnPausePressed += TogglePause;
    }

    private void TogglePause()
    {
        if (_panel.activeSelf)
        {
            OnResumeClicked?.Invoke();
            ResumeGame();
        }
        else
        {
            OpenPause();
        }
    }

    private void OpenPause()
    {
        OnPauseOpened?.Invoke();

        _panel.SetActive(true);
        _pauseManager.SetPaused(true);

        _windowRect.anchoredPosition = new Vector2(0f, 1000f);

        _canvasGroup.DOFade(1f, 0.2f).SetUpdate(true);
        _windowRect.DOAnchorPos(Vector2.zero, 0.4f).SetEase(Ease.OutBack).SetUpdate(true)
            .OnComplete(() => {
                _canvasGroup.interactable = true;
                _canvasGroup.blocksRaycasts = true;
            });
    }

    private void ResumeGame()
    {
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

        _canvasGroup.DOFade(0f, 0.2f).SetUpdate(true);
        _windowRect.DOAnchorPos(new Vector2(0f, 1000f), 0.3f).SetEase(Ease.InCubic).SetUpdate(true)
            .OnComplete(() => {
                _panel.SetActive(false);
                _pauseManager.SetPaused(false);
                OnPauseClosed?.Invoke();
            });
    }

    private void ExitToMenu()
    {
        _sceneLoader.LoadScene(_menuSceneIndex);
    }

    private void AnimateButtonClick(Button button, Action onComplete)
    {
        button.interactable = false;
        button.transform.DOPunchScale(new Vector3(-0.1f, -0.1f, 0f), 0.15f)
            .SetUpdate(true)
            .OnComplete(() => {
                button.interactable = true;
                onComplete?.Invoke();
            });
    }

    private void OnDestroy()
    {
        if (_input != null)
        {
            _input.OnPausePressed -= TogglePause;
        }

        _windowRect.DOKill();
        _canvasGroup.DOKill();
    }
}
