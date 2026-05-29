using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

public class AudioService
{
    private readonly IInstantiator _instantiator;
    private readonly Transform _poolRoot;

    private readonly Queue<PooledAudioSource> _pool = new Queue<PooledAudioSource>();

    private readonly AudioMixerGroup _sfxGroup;
    private readonly AudioMixerGroup _musicGroup;

    private AudioSource _musicSource;

    public AudioService(IInstantiator instantiator, AudioMixer mixer, Context context)
    {
        _instantiator = instantiator;

        GameObject poolGO = new GameObject("--- AUDIO_POOL ---");
        _poolRoot = poolGO.transform;

        _poolRoot.SetParent(context.transform);

        _sfxGroup = mixer.FindMatchingGroups("SFX")[0];
        _musicGroup = mixer.FindMatchingGroups("Music")[0];

        SetupMusicSource();
    }

    private void SetupMusicSource()
    {
        GameObject musicObj = new GameObject("Background_Music_Source");
        musicObj.transform.SetParent(_poolRoot);
        _musicSource = musicObj.AddComponent<AudioSource>();
        _musicSource.outputAudioMixerGroup = _musicGroup;
        _musicSource.loop = true;
        _musicSource.spatialBlend = 0f;
    }

    public void Play3DSound(SoundData soundData, Vector3 position)
    {
        if (soundData.Clip == null) return;

        PooledAudioSource source = GetSourceFromPool();
        source.Play(soundData, position, _sfxGroup, ReturnToPool, is2D: false);
    }

    public void Play2DSound(SoundData soundData)
    {
        if (soundData.Clip == null) return;

        PooledAudioSource source = GetSourceFromPool();
        source.Play(soundData, Vector3.zero, _sfxGroup, ReturnToPool, is2D: true);
    }

    public void PlayMusic(SoundData soundData)
    {
        if (soundData.Clip == null) return;

        _musicSource.clip = soundData.Clip;
        _musicSource.volume = soundData.Volume;
        _musicSource.Play();
    }

    private PooledAudioSource GetSourceFromPool()
    {
        if (_pool.Count > 0)
        {
            PooledAudioSource source = _pool.Dequeue();
            source.gameObject.SetActive(true);
            return source;
        }

        GameObject go = new GameObject("PooledAudioSource");
        go.transform.SetParent(_poolRoot);

        PooledAudioSource newSource = go.AddComponent<PooledAudioSource>();
        return newSource;
    }

    private void ReturnToPool(PooledAudioSource source)
    {
        source.gameObject.SetActive(false);
        _pool.Enqueue(source);
    }
}
