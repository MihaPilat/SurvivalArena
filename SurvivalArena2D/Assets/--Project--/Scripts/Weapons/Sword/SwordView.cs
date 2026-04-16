using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SwordView : MonoBehaviour
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
            _sword.OnAttack += PlayAttackAnimation;
    }
    private void OnDisable()
    {
        if (_sword != null)
            _sword.OnAttack -= PlayAttackAnimation;
    }
    private void PlayAttackAnimation()
    {
        _animator.SetTrigger("Attack");
    }
}
