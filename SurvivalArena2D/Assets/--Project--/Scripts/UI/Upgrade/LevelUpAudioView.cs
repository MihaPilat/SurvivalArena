using UnityEngine;
using Zenject;

public class LevelUpAudioView : MonoBehaviour
{
    [SerializeField] private SoundData _showSound;

    private AudioService _audioService;
    private LevelUpScreen _levelUpScreen;

    [Inject]
    public void Construct(AudioService audioService)
    {
        _audioService = audioService;
    }

    private void Awake()
    {
        _levelUpScreen = GetComponentInParent<LevelUpScreen>();
    }

    private void OnEnable()
    {
        _levelUpScreen.OnShow += PlayShowSound;
    }

    private void OnDisable()
    {
        _levelUpScreen.OnShow -= PlayShowSound;
    }

    private void PlayShowSound()
    {
        _audioService.Play2DSound(_showSound);
    }
}
