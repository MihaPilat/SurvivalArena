using UnityEngine;
using Zenject;

[RequireComponent(typeof(EnemyEntity))]
public class EnemyAudioView : MonoBehaviour
{
    [Header("Audio Data")]
    [SerializeField] private SoundData _attackSound;
    [SerializeField] private SoundData _hitSound;
    [SerializeField] private SoundData _dieSound;

    private AudioService _audioService;
    private EnemyEntity _enemy;

    [Inject]
    public void Construct(AudioService audioService)
    {
        _audioService = audioService;
    }

    private void Awake()
    {
        _enemy = GetComponent<EnemyEntity>();

        _enemy.OnSpawned += SubscribeToGameplayEvents;
    }

    private void OnEnable()
    {
        SubscribeToGameplayEvents();
    }

    private void OnDisable()
    {
        UnsubscribeFromGameplayEvents();
    }

    private void SubscribeToGameplayEvents()
    {
        UnsubscribeFromGameplayEvents();

        _enemy.OnAttack += PlayAttackSound;
        _enemy.OnHit += PlayHitSound;
        _enemy.OnDied += PlayDieSound;
    }

    private void UnsubscribeFromGameplayEvents()
    {
        _enemy.OnAttack -= PlayAttackSound;
        _enemy.OnHit -= PlayHitSound;
        _enemy.OnDied -= PlayDieSound;
    }
    private void PlayAttackSound()
    {
        _audioService.Play3DSound(_attackSound, transform.position);
    }

    private void PlayHitSound()
    {
        _audioService.Play3DSound(_hitSound, transform.position);
    }

    private void PlayDieSound()
    {
        _audioService.Play3DSound(_dieSound, transform.position);
    }
}
