using System;
using UnityEngine;

public class CharacterView : MonoBehaviour
{
    [SerializeField] private Character _character;

    [SerializeField] private Animator _animator;
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
