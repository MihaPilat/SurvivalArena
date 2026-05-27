using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ExplosionEffect : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private PoolFactory _poolFactory;
    private ExplosionEffect _originPrefab;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }
    public void SetPoolData(ExplosionEffect prefab, PoolFactory factory)
    {
        _originPrefab = prefab;
        _poolFactory = factory;
    }
    public void PlayExplosion(Vector3 position, float radius, Color explosionColor)
    {
        transform.position = position;

        var shape = _particleSystem.shape;
        shape.radius = radius;

        var main = _particleSystem.main;
        main.startColor = explosionColor;

        _particleSystem.Play();
        Invoke(nameof(ReturnToPool), main.duration + main.startLifetime.constantMax);
    }

    private void ReturnToPool()
    {
        _poolFactory.Reclaim<ExplosionEffect>(this, _originPrefab);
    }
}
