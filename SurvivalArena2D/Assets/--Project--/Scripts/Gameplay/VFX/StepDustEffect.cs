using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class StepDustEffect : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private PoolFactory _poolFactory;
    private StepDustEffect _originPrefab;

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
