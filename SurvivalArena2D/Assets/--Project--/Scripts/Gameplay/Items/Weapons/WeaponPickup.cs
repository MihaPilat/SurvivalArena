using System;
using UnityEngine;

public class WeaponPickup : MonoBehaviour,IPickup
{
    public event Action OnPickedUp;

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
            OnPickedUp?.Invoke();
            Destroy(gameObject);
        }
    }
}
