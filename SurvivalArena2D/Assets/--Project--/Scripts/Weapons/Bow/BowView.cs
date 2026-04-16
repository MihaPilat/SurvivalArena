using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BowView : MonoBehaviour
{
    private Bow _bow;
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _bow = GetComponentInParent<Bow>();
    }
    private void OnEnable()
    {
        if (_bow != null)
            _bow.OnAttack += PlayAttackAnimation;
    }
    private void OnDisable()
    {
        if (_bow != null)
            _bow.OnAttack -= PlayAttackAnimation;
    }

    private void PlayAttackAnimation()
    {
        _animator.SetTrigger("Attack");
    }
}
