using UnityEngine;
using Zenject;

[RequireComponent(typeof(MainMenuUI))]
public class MainMenuAudioView : MonoBehaviour
{
    [SerializeField] private SoundData _clickSound;
    [SerializeField] private SoundData _playClickSound;

    private AudioService _audioService;
    private MainMenuUI _mainMenuUI;

    [Inject]
    public void Construct(AudioService audioService)
    {
        _audioService = audioService;
    }

    private void Awake()
    {
        _mainMenuUI = GetComponent<MainMenuUI>();
    }

    private void OnEnable()
    {
        _mainMenuUI.OnPlayClicked += PlayStartSound;
        _mainMenuUI.OnSettingsClicked += PlayDefaultClick;
        _mainMenuUI.OnExitClicked += PlayDefaultClick;
        _mainMenuUI.OnSettingsClosed += PlayDefaultClick;
        _mainMenuUI.OnResetClicked += PlayDefaultClick;
    }

    private void OnDisable()
    {
        _mainMenuUI.OnPlayClicked -= PlayStartSound;
        _mainMenuUI.OnSettingsClicked -= PlayDefaultClick;
        _mainMenuUI.OnExitClicked -= PlayDefaultClick;
        _mainMenuUI.OnSettingsClosed -= PlayDefaultClick;
        _mainMenuUI.OnResetClicked -= PlayDefaultClick;
    }
    private void PlayDefaultClick()
    {
        Debug.Log($"[AudioCheck] Service: {_audioService != null}");
        _audioService.Play2DSound(_clickSound);
    }

    private void PlayStartSound()
    {
        _audioService.Play2DSound(_playClickSound);
    }
}
