using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using System;

[RequireComponent(typeof(AudioSource))]
public class PooledAudioSource : MonoBehaviour
{
    private Action<PooledAudioSource> _onFinished;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        _audioSource.playOnAwake = false;
        _audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
        _audioSource.minDistance = 1f;
        _audioSource.maxDistance = 20f;
    }

    public void Play(SoundData data, Vector3 position, AudioMixerGroup audioMixerGroup, Action<PooledAudioSource> callback, bool is2D)
    {
        _onFinished = callback;

        transform.position = position;

        data.ApplyTo(_audioSource);

        _audioSource.outputAudioMixerGroup = audioMixerGroup;

        _audioSource.spatialBlend = is2D ? 0f : 1f;

        _audioSource.Play();

        StartCoroutine(WaitForEnd());
    }

    private IEnumerator WaitForEnd()
    {
        yield return new WaitWhile(() => _audioSource.isPlaying);

        _onFinished?.Invoke(this);
    }
}
