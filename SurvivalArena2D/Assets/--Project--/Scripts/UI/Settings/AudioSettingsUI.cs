using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class AudioSettingsUI : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    private AudioService _audioService;

    [Inject]
    public void Construct(AudioService audioService)
    {
        _audioService = audioService;
    }
    private void Start()
    {
        float savedMusic = _audioService.GetSavedVolume("MusicVolumeValue");
        float savedSfx = _audioService.GetSavedVolume("SFXVolumeValue");

        _musicSlider.SetValueWithoutNotify(savedMusic);
        _sfxSlider.SetValueWithoutNotify(savedSfx);

        _musicSlider.onValueChanged.AddListener(SetMusicVolume);
        _sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void SetMusicVolume(float value) => _audioService.SetMusicVolume(value);

    private void SetSFXVolume(float value) => _audioService.SetSFXVolume(value);

    private void OnDestroy()
    {
        if (_musicSlider != null) _musicSlider.onValueChanged.RemoveListener(SetMusicVolume);
        if (_sfxSlider != null) _sfxSlider.onValueChanged.RemoveListener(SetSFXVolume);
    }
}
