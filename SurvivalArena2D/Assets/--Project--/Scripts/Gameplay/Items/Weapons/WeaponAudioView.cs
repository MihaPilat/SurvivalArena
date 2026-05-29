using UnityEngine;
using Zenject;

public class WeaponAudioView : MonoBehaviour
{
    [SerializeField] private SoundData _swordSoundData; // Звук настраивается прямо здесь!

    private AudioService _audioService;
    private IWeapon _weapon;

    [Inject]
    public void Construct(AudioService audioService)
    {
        _audioService = audioService;
    }
    private void Awake()
    {
        _weapon = GetComponentInParent<IWeapon>();
    }
    private void OnEnable()
    {
        _weapon.OnAttack += PlayAttackSound;
    }

    private void OnDisable()
    {
        _weapon.OnAttack -= PlayAttackSound;
    }

    private void PlayAttackSound()
    {
        Debug.Log("PlayAttackSound()");
        _audioService.Play3DSound(_swordSoundData, transform.position);
    }
}
