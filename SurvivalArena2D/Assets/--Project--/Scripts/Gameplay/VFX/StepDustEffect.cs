using UnityEngine;
using Zenject;

[RequireComponent(typeof(ParticleSystem))]
public class StepDustEffect : MonoBehaviour
{
    [SerializeField] private SoundData _stepSoundData;

    private AudioService _audioService;

    private ParticleSystem _particleSystem;
    private PoolFactory _poolFactory;
    private StepDustEffect _originPrefab;

    [Inject]
    public void Construct(AudioService audioService)
    {
        _audioService = audioService;
    }

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }
    public void SetPoolData(StepDustEffect prefab, PoolFactory factory)
    {
        _originPrefab = prefab;
        _poolFactory = factory;
    }
    public void PlayAt(Vector3 position)
    {
        _audioService.Play3DSound(_stepSoundData, position);

        transform.position = position;
        _particleSystem.Play();

        CancelInvoke(nameof(ReturnToPool));
        Invoke(nameof(ReturnToPool), _particleSystem.main.duration + 0.1f);
    }

    private void ReturnToPool()
    {
        _poolFactory.Reclaim(this, _originPrefab);
    }
}
