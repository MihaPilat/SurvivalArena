using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SwordSlashView : MonoBehaviour
{
    private Sword _sword;
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _sword = GetComponentInParent<Sword>();
    }
    private void OnEnable()
    {
        if (_sword != null)
            _sword.OnAttack += PlaySlashAnimation;
    }
    private void OnDisable()
    {
        if (_sword != null)
            _sword.OnAttack -= PlaySlashAnimation;
    }
    public void PlaySlashAnimation()
    {
        _animator.SetTrigger("Attack");
    }
}
