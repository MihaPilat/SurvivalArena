using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PickupAudioView : MonoBehaviour
{
    [SerializeField] private SoundData _pickupSound;

    private AudioService _audioService;
    private IPickup _pickup;

    [Inject]
    public void Construct(AudioService audioService)
    {
        _audioService = audioService;
    }

    private void Awake()
    {
        _pickup = GetComponentInParent<IPickup>();
    }
    private void OnEnable()
    {
        if (_pickup != null) _pickup.OnPickedUp += PlaySound;
    }

    private void OnDisable()
    {
        if (_pickup != null) _pickup.OnPickedUp -= PlaySound;
    }

    private void PlaySound()
    {
        _audioService.Play2DSound(_pickupSound);
    }
}
