using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private GameObject _weaponPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        WeaponController weaponController =collision.GetComponentInChildren<WeaponController>();
        
        if (weaponController!=null)
        {
            Debug.Log($"Picked up: {_weaponPrefab.name}");
            weaponController.TryAddWeapon(_weaponPrefab);
            Destroy(gameObject);
        }
    }
}
