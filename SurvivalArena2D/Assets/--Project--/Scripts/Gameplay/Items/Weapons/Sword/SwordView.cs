using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SwordView : MonoBehaviour
{

    [SerializeField] private DamageHitbox _hitbox;
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
            _sword.OnAttack += PlayAttackAnimation;
    }
    private void OnDisable()
    {
        if (_sword != null)
            _sword.OnAttack -= PlayAttackAnimation;
    }

    public void EnableHitbox()
    {
        _hitbox.Enable(_sword.Damage);
    }

    public void DisableHitbox()
    {
        _hitbox.Disable();
    }

    private void PlayAttackAnimation()
    {
        _animator.SetTrigger("Attack");
    }
}
