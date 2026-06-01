using UnityEngine;
using Zenject;

public class GameOverAudioView : MonoBehaviour
{
    [SerializeField] private SoundData _clickSound;

    private AudioService _audioService;
    private GameOverUI _gameOverUI;

    [Inject]
    public void Construct(AudioService audioService)
    {
        _audioService = audioService;
    }

    private void Awake()
    {
        _gameOverUI = GetComponentInParent<GameOverUI>();
    }

    private void OnEnable()
    {
        _gameOverUI.OnRestartClicked += PlayClickSound;
        _gameOverUI.OnBackToMenuClicked += PlayClickSound;
    }

    private void OnDisable()
    {
        _gameOverUI.OnRestartClicked -= PlayClickSound;
        _gameOverUI.OnBackToMenuClicked -= PlayClickSound;
    }

    private void PlayClickSound()
    {
        _audioService.Play2DSound(_clickSound);
    }
}
