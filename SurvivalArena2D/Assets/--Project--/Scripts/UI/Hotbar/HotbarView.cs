using UnityEngine;
using Zenject;

public class HotbarView : MonoBehaviour
{
    [SerializeField] private HotbarSlotView[] _slots;

    private InventoryService _inventoryService;

    [Inject]
    private void Construct(InventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    private void Awake()
    {
        for (int i = 0; i < _slots.Length; i++)
        {
            _slots[i].SetKeyText((i + 1).ToString());
        }
    }
    private void Start()
    {
        FullRedraw();
    }

    private void OnEnable()
    {
        _inventoryService.OnSlotChanged += UpdateSlotIcon;
        _inventoryService.OnWeaponSelected += HighlightActiveSlot;

    }

    private void OnDisable()
    {
        _inventoryService.OnSlotChanged -= UpdateSlotIcon;
        _inventoryService.OnWeaponSelected -= HighlightActiveSlot;
    }
    private void FullRedraw()
    {
        var weapons = _inventoryService.GetAllWeapons();
        int activeIndex = _inventoryService.CurrentSlotIndex;

        for (int i = 0; i < _slots.Length; i++)
        {
            IWeapon weapon = (i < weapons.Count) ? weapons[i] : null;

            _slots[i].SetIcon(weapon?.Icon);
            _slots[i].SetSelection(i == activeIndex);
        }
    }
    private void UpdateSlotIcon(int index, IWeapon weapon)
    {
        if (index >= 0 && index < _slots.Length)
        {
            _slots[index].SetIcon(weapon?.Icon);
        }
    }

    private void HighlightActiveSlot(IWeapon _)
    {
        int activeIndex = _inventoryService.CurrentSlotIndex;

        for (int i = 0; i < _slots.Length; i++)
        {
            _slots[i].SetSelection(i == activeIndex);
        }
    }
}


