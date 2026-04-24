using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterView : MonoBehaviour
{
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

    private void UpdateAnimation()
    {
        float speed = _character.MoveDirection.magnitude;
        _animator.SetFloat("Speed", speed);
    }

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
