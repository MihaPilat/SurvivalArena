using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WeaponController : MonoBehaviour, IWeaponController
{
    [SerializeField] private Character _character;
    [SerializeField] private List<MonoBehaviour> _weaponBehaviours;

    private Dictionary<WeaponType, IWeapon> _weapons = new Dictionary<WeaponType, IWeapon>();
    private Dictionary<WeaponType, MonoBehaviour> _weaponObjects = new Dictionary<WeaponType, MonoBehaviour>();
    private IWeapon _currentWeapon;
    private IInput _input;

    [Inject]
    private void Construct(IInput input)
    {
        _input = input;
    }
    private void Awake()
    {
        foreach (var wb in _weaponBehaviours)
        {
            if (wb is IWeapon weapon)
            {
                _weapons[weapon.Type] = weapon;
                _weaponObjects[weapon.Type] = wb;
            }

            wb.gameObject.SetActive(false);
        }

        SetWeapon(WeaponType.Sword);
    }
    private void Update()
    {
        _currentWeapon?.Rotate(_character.AimDirection);

        if (_input.Attack)
        {
            _character.Attack();
            Debug.Log("fdsfasfasdf");
        }
    }
    public void SetWeapon(WeaponType type)
    {
        if (!_weapons.ContainsKey(type))
            return;

        foreach (var obj in _weaponObjects.Values)
            obj.gameObject.SetActive(false);

        _weaponObjects[type].gameObject.SetActive(true);

        _currentWeapon = _weapons[type];
        _character.SetWeapon(_currentWeapon);
    }
}
