using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WeaponController : MonoBehaviour
{
    public event Action<IWeapon> OnWeaponChanged;

    [SerializeField] private Character _character;
    [SerializeField] private Transform _weaponHolder;
    [SerializeField] private GameObject _startWeaponPrefab;

    private Dictionary<WeaponType, IWeapon> _weapons = new Dictionary<WeaponType, IWeapon>();
    private IWeapon _currentWeapon;
    private IInput _input;
    private InventoryService _inventoryService;
    private DiContainer _container;

    [Inject]
    private void Construct(IInput input, DiContainer container, InventoryService inventoryService)
    {
        _input = input;
        _container = container;
        _inventoryService = inventoryService;
    }
    private void OnEnable()
    {
        _inventoryService.OnWeaponSelected += HandleWeaponSelected;
    }

    private void OnDisable()
    {
        _inventoryService.OnWeaponSelected -= HandleWeaponSelected;
    }
    private void Start()
    {
        if (_startWeaponPrefab != null)
        {
            AddWeaponToInventory(_startWeaponPrefab, 0);
            _inventoryService.SelectSlot(0);
        }
    }
    private void Update()
    {
        if (_character.IsDie)
            return;

        _currentWeapon?.Rotate(_character.AimDirection);

        if (_input.Attack)
        {
            _character.Attack();
        }
    }
    public void TryAddWeapon(GameObject weaponPrefab)
    {
        int slotIndex = _inventoryService.GetFirstEmptySlot();

        if (slotIndex == -1)
        {
            slotIndex = _inventoryService.CurrentSlotIndex;
        }

        AddWeaponToInventory(weaponPrefab, slotIndex);
    }

    private IWeapon AddWeaponToInventory(GameObject weaponPrefab, int slotIndex)
    {
        GameObject obj = Instantiate(weaponPrefab, _weaponHolder);
        _container.InjectGameObject(obj);

        if (obj.TryGetComponent(out IWeapon weapon))
        {
            if (!_weapons.ContainsKey(weapon.Type))
            {
                _weapons[weapon.Type] = weapon;
            }

            obj.SetActive(false);

            _inventoryService.AddWeapon(slotIndex, weapon);

            return weapon;
        }

        Destroy(obj);
        return null;
    }
    private void HandleWeaponSelected(IWeapon targetWeapon)
    {
        if (targetWeapon == null || _character.IsDie) return;

        if (_currentWeapon != null)
            ((MonoBehaviour)_currentWeapon).gameObject.SetActive(false);

        _currentWeapon = targetWeapon;

        ((MonoBehaviour)_currentWeapon).gameObject.SetActive(true);

        OnWeaponChanged?.Invoke(_currentWeapon);

        _character.SetWeapon(_currentWeapon);
    }
}
