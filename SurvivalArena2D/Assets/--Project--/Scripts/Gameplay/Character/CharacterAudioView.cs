using UnityEngine;
using Zenject;

public class CharacterAudioView : MonoBehaviour
{
    [SerializeField] private SoundData _damagedSound;
    [SerializeField] private SoundData _deathStartedSound;

    private AudioService _audioService;
    private Character _character;

    [Inject]
    public void Construct(AudioService audioService)
    {
        _audioService = audioService;
    }

    private void Awake()
    {
        _character = GetComponentInParent<Character>();
    }
    private void OnEnable()
    {
        _character.OnDamaged += PlayDamagedSound;
        _character.OnDeathStarted += PlayDeathStartedSound;
    }

    private void OnDisable()
    {
        _character.OnDamaged -= PlayDamagedSound;
        _character.OnDeathStarted -= PlayDeathStartedSound;
    }

    private void PlayDamagedSound()
    {
        _audioService.Play3DSound(_damagedSound, transform.position);
    }

    private void PlayDeathStartedSound()
    {
        _audioService.Play2DSound(_deathStartedSound);
    }
}
