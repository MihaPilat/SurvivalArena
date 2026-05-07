using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private GameObject _weaponPrefab;

    private bool _isPickedUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isPickedUp) return;

        WeaponController weaponController =collision.GetComponentInChildren<WeaponController>();
        
        if (weaponController!=null)
        {
            _isPickedUp = true;
            Debug.Log($"Picked up: {_weaponPrefab.name}");
            weaponController.TryAddWeapon(_weaponPrefab);
            Destroy(gameObject);
        }
    }
}
