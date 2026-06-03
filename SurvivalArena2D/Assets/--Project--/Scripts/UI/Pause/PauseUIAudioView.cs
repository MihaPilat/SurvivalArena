using UnityEngine;
using Zenject;

[RequireComponent(typeof(PauseUI))]
public class PauseUIAudioView : MonoBehaviour
{
    [SerializeField] private SoundData _clickSound;
    [SerializeField] private SoundData _showSound;

    private AudioService _audioService;
    private PauseUI _pauseUI;

    [Inject]
    public void Construct(AudioService audioService)
    {
        _audioService = audioService;
    }

    private void Awake()
    {
        _pauseUI = GetComponent<PauseUI>();
    }

    private void OnEnable()
    {
        _pauseUI.OnPauseOpened += PlayShowSound;
        _pauseUI.OnResumeClicked += PlayClickSound;
        _pauseUI.OnExitClicked += PlayClickSound;
    }

    private void OnDisable()
    {
        if (_pauseUI != null)
        {
            _pauseUI.OnPauseOpened -= PlayShowSound;
            _pauseUI.OnResumeClicked -= PlayClickSound;
            _pauseUI.OnExitClicked -= PlayClickSound;
        }
    }

    private void PlayShowSound() => _audioService.Play2DSound(_showSound);

    private void PlayClickSound() => _audioService.Play2DSound(_clickSound);
}
