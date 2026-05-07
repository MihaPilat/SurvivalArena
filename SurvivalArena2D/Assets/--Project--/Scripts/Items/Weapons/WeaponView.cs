using UnityEngine;

[RequireComponent(typeof(Animator))]
public class WeaponView : MonoBehaviour
{
    private Animator _animator;
    private WeaponController _weaponController;
    private IWeapon _currentWeapon;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _weaponController = GetComponentInParent<WeaponController>();
    }
    private void OnEnable()
    {
        if (_weaponController == null)
        {
            _weaponController = GetComponentInParent<WeaponController>();
        }

        if (_weaponController == null)
        {
            Debug.LogError("WeaponController not found", this);
            return;
        }

        _weaponController.OnWeaponChanged += OnWeaponChanged;
    }

    private void OnDisable()
    {
        _weaponController.OnWeaponChanged -= OnWeaponChanged;
    }

    private void OnWeaponChanged(IWeapon weapon)
    {
        if (_currentWeapon != null)
            _currentWeapon.OnAttack -= PlayAttackAnimation;

        _currentWeapon = weapon;

        if (_currentWeapon != null)
            _currentWeapon.OnAttack += PlayAttackAnimation;
    }

    private void PlayAttackAnimation()
    {
        _animator.SetTrigger("Attack");
    }
}