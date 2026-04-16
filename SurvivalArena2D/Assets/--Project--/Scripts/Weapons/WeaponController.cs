using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WeaponController : MonoBehaviour, IWeaponController
{
    [SerializeField] private Character _character;
    [SerializeField] private Transform _weaponHolder;
    [SerializeField] private GameObject _startWeaponPrefab;

    private Dictionary<WeaponType, IWeapon> _weapons = new Dictionary<WeaponType, IWeapon>();
    private Dictionary<WeaponType, MonoBehaviour> _weaponObjects = new Dictionary<WeaponType, MonoBehaviour>();
    private IWeapon _currentWeapon;
    private IInput _input;

    [Inject]
    private void Construct(IInput input)
    {
        _input = input;
    }
    private void Start()
    {
        AddWeapon(_startWeaponPrefab);
    }
    private void Update()
    {
        _currentWeapon?.Rotate(_character.AimDirection);

        if (_input.Attack)
        {
            _character.Attack();
        }
    }
    public void AddWeapon(GameObject weaponPrefab)
    {
        GameObject obj = Instantiate(weaponPrefab, _weaponHolder);
        IWeapon weapon = obj.GetComponent<IWeapon>();
        if (weapon == null)
        {
            Debug.LogError("Prefab has no IWeapon!");
            return;
        }

        if (_weapons.ContainsKey(weapon.Type))
        {
            Destroy(obj);
            return;
        }

        _weapons[weapon.Type] = weapon;
        _weaponObjects[weapon.Type] = obj.GetComponent<MonoBehaviour>();
        SetWeapon(weapon.Type);
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
