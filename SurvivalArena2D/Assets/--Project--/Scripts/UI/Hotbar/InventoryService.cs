using System;
using System.Collections.Generic;
using Zenject;

public class InventoryService : IInitializable, IDisposable
{
    public event Action<int, IWeapon> OnSlotChanged;
    public event Action<IWeapon> OnWeaponSelected;

    private readonly IWeapon[] _slots = new IWeapon[5];
    private IInput _input;

    private int _currentSlotIndex = -1;

    public int CurrentSlotIndex => _currentSlotIndex;

    [Inject]
    private void Construct(IInput input)
    {
        _input = input;
    }
    public void Initialize()
    {
        _input.OnSlotPressed += SelectSlot;
    }

    public void SelectSlot(int index)
    {
        if (index < 0 || index >= _slots.Length || index == _currentSlotIndex) return;

        _currentSlotIndex = index;
        OnWeaponSelected?.Invoke(_slots[index]);
    }

    public void AddWeapon(int index, IWeapon weapon)
    {
        if (index < 0 || index >= _slots.Length) return;

        _slots[index] = weapon;
        OnSlotChanged?.Invoke(index, weapon);
            
        if (index == _currentSlotIndex)
            OnWeaponSelected?.Invoke(weapon);
    }
    public int GetFirstEmptySlot()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            if (_slots[i] == null)
            {
                return i;
            }
        }
        return -1;
    }

    public bool IsSlotBusy(int index) => _slots[index] != null;

    public void Dispose()
    {
        _input.OnSlotPressed -= SelectSlot;
    }
    public IReadOnlyList<IWeapon> GetAllWeapons()
    {
        return _slots;
    }
}
