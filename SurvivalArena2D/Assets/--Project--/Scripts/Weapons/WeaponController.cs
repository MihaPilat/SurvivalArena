using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WeaponController : MonoBehaviour, IWeaponController
{
    public event Action<IWeapon> OnWeaponChanged;

    [SerializeField] private Character _character;
    [SerializeField] private Transform _weaponHolder;
    [SerializeField] private GameObject _startWeaponPrefab;

    private Dictionary<WeaponType, IWeapon> _weapons = new Dictionary<WeaponType, IWeapon>();
    private IWeapon _currentWeapon;
    private IInput _input;
    private DiContainer _container;

    [Inject]
    private void Construct(IInput input, DiContainer container)
    {
        _input = input;
        _container = container;
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

        _container.InjectGameObject(obj);

        if (obj.TryGetComponent(out IWeapon weapon))
        {
            if (_weapons.ContainsKey(weapon.Type))
            {
                Destroy(obj);
                return;
            }

            _weapons[weapon.Type] = weapon;

            obj.SetActive(false);

            SetWeapon(weapon.Type);
        }
        else
        {
            Debug.LogError($"Prefab {weaponPrefab.name} has no IWeapon component!");
            Destroy(obj);
        }
    }
    public void SetWeapon(WeaponType type)
    {
        if (!_weapons.TryGetValue(type, out var targetWeapon))
            return;

        if (_currentWeapon != null)
            ((MonoBehaviour)_currentWeapon).gameObject.SetActive(false);

        _currentWeapon = targetWeapon;
        ((MonoBehaviour)_currentWeapon).gameObject.SetActive(true);

        OnWeaponChanged?.Invoke(_currentWeapon);
        _character.SetWeapon(_currentWeapon);
    }
}
