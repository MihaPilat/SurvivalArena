using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;

    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    private const string MusicParam = "MusicVolume";
    private const string SfxParam = "SFXVolume";

    private void Start()
    {
        _musicSlider.onValueChanged.AddListener(SetMusicVolume);
        _sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        float savedMusic = PlayerPrefs.GetFloat("MusicVolumeValue", 0.75f);
        float savedSfx = PlayerPrefs.GetFloat("SFXVolumeValue", 0.75f);

        _musicSlider.value = savedMusic;
        _sfxSlider.value = savedSfx;
    }

    private void SetMusicVolume(float value)
    {
        float dbValue = Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20f;

        _audioMixer.SetFloat(MusicParam, dbValue);

        PlayerPrefs.SetFloat("MusicVolumeValue", value);
    }

    private void SetSFXVolume(float value)
    {
        float dbValue = Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20f;

        _audioMixer.SetFloat(SfxParam, dbValue);

        PlayerPrefs.SetFloat("SFXVolumeValue", value);
    }

    private void OnDestroy()
    {
        _musicSlider.onValueChanged.RemoveListener(SetMusicVolume);
        _sfxSlider.onValueChanged.RemoveListener(SetSFXVolume);
    }
}
