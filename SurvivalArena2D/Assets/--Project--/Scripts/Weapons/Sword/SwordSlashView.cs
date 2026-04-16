using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SwordSlashView : MonoBehaviour
{
    [SerializeField] private Sword _sword;
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
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
