using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ExplosionEffect : MonoBehaviour
{
    private ParticleSystem _mainParticleSystem;
    private ParticleSystem[] _childParticleSystems;
    private PoolFactory _poolFactory;
    private ExplosionEffect _originPrefab;

    private void Awake()
    {
        _mainParticleSystem = GetComponent<ParticleSystem>();
        _childParticleSystems = GetComponentsInChildren<ParticleSystem>();
    }
    public void SetPoolData(ExplosionEffect prefab, PoolFactory factory)
    {
        _originPrefab = prefab;
        _poolFactory = factory;
    }
    public void PlayExplosion(Vector3 position, float radius, Color explosionColor)
    {
        transform.position = position;

        foreach (var ps in _childParticleSystems)
        {
            if (ps == null) continue;

            var main = ps.main;
            main.startColor = explosionColor;

            var shape = ps.shape;
            if (shape.enabled)
            {
                shape.radius = radius;
            }

            if (ps == _mainParticleSystem || ps.gameObject.name == "Shockwave")
            {
                main.startSize = radius * 2f;
            }

            // Настройка для Искр (Sparks)
            if (ps.gameObject.name == "Sparks")
            {
                main.startSpeed = new ParticleSystem.MinMaxCurve(radius * 3f, radius * 5f);

                int dynamicCount = Mathf.RoundToInt(15 + (radius * 20f));

                var emission = ps.emission;
                if (emission.burstCount > 0)
                {
                    ParticleSystem.Burst[] bursts = new ParticleSystem.Burst[emission.burstCount];
                    emission.GetBursts(bursts);

                    bursts[0].count = dynamicCount;

                    emission.SetBursts(bursts);
                }
            }
        }

        _mainParticleSystem.Play(withChildren: true);

        CancelInvoke(nameof(ReturnToPool));
        Invoke(nameof(ReturnToPool), _mainParticleSystem.main.duration + 0.5f);
    }

    private void ReturnToPool()
    {
        _poolFactory.Reclaim<ExplosionEffect>(this, _originPrefab);
    }
}
