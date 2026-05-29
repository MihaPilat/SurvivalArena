using UnityEngine;
using Zenject;

public class ProjectileAudioView : MonoBehaviour
{
    [SerializeField] private SoundData _explosionSound;

    private AudioService _audioService;
    private MagicProjectile _projectile;

    [Inject]
    public void Construct(AudioService audioService)
    {
        _audioService = audioService;
    }

    private void Awake()
    {
        _projectile = GetComponentInParent<MagicProjectile>();
    }

    private void OnEnable()
    {
        _projectile.OnExploded -= PlayExplosionSound;
        _projectile.OnExploded += PlayExplosionSound;
    }

    private void OnDisable()
    {
        _projectile.OnExploded -= PlayExplosionSound;
    }

    private void PlayExplosionSound()
    {
        _audioService.Play3DSound(_explosionSound, transform.position);
    }
}
