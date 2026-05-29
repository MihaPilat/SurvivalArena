using UnityEngine;
using System;
using Random = UnityEngine.Random;

[Serializable]
public struct SoundData
{
    [SerializeField] private AudioClip _clip;

    [SerializeField, Range(0f, 1f)] private float _volume;

    [SerializeField, Range(0.1f, 3f)] private float _minPitch;

    [SerializeField, Range(0.1f, 3f)] private float _maxPitch;

    public AudioClip Clip => _clip;
    public float Volume => _volume;

    public void ApplyTo(AudioSource source)
    {
        source.clip = Clip;
        source.volume = Volume;
        source.pitch = Random.Range(_minPitch, _maxPitch);
    }
}
