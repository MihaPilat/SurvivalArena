using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterView : MonoBehaviour
{
    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int HitHash = Animator.StringToHash("Hit");
    private static readonly int IsDeadHash = Animator.StringToHash("IsDead");

    private Character _character;

    private Animator _animator;
    private void Awake()
    {
        _character = GetComponentInParent<Character>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        Flip();
        UpdateAnimation();

    }
    private void OnEnable()
    {
        if (_character != null)
        {
            _character.OnDamaged += PlayHitAnimation;
            _character.OnDeathStarted += PlayDeathAnimation;
        }
    }

    private void OnDisable()
    {
        if (_character != null)
        {
            _character.OnDamaged -= PlayHitAnimation;
            _character.OnDeathStarted -= PlayDeathAnimation;
        }
    }

    private void UpdateAnimation()
    {
        float speed = _character.MoveDirection.magnitude;
        _animator.SetFloat(SpeedHash, speed);
    }

    private void PlayHitAnimation() => _animator.SetTrigger(HitHash);

    private void PlayDeathAnimation() => _animator.SetBool(IsDeadHash, true);

        private void Flip()
    {
        float dirX = _character.AimDirection.x;

        if (dirX > 0)
        {
            transform.localScale = new Vector3(0.1f, 0.1f, 1);
        }
        else if (dirX < 0)
        {
            transform.localScale = new Vector3(-0.1f, 0.1f, 1);
        }
    }
}
