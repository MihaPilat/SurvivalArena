using UnityEngine;
using Zenject;

public class UpgradeCardAudioView : MonoBehaviour
{
    [SerializeField] private SoundData _clickSound;

    private AudioService _audioService;
    private UpgradeCard _card;

    [Inject]
    public void Construct(AudioService audioService)
    {
        _audioService = audioService;
    }

    private void Awake()
    {
        _card = GetComponentInParent<UpgradeCard>();
    }

    private void OnEnable()
    {
        _card.OnClicked += PlayClickSound;
    }

    private void OnDisable()
    {
        _card.OnClicked -= PlayClickSound;
    }

    private void PlayClickSound()
    {
        _audioService.Play2DSound(_clickSound);
    }
}
