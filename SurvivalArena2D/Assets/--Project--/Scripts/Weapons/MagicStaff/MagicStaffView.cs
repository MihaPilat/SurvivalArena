using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MagicStaffView : MonoBehaviour
{

    private MagicStaff _magicStaff;
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _magicStaff = GetComponentInParent<MagicStaff>();
    }
    private void OnEnable()
    {
        _magicStaff.OnAttack += PlayAttackAnimation;
    }
    private void OnDisable()
    {
        _magicStaff.OnAttack -= PlayAttackAnimation;
    }

    private void PlayAttackAnimation()
    {
        _animator.SetTrigger("Attack");
    }
}
