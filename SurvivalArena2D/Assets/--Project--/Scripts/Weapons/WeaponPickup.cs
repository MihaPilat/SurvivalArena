using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private GameObject _weaponPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IWeaponController weaponController =collision.GetComponentInChildren<IWeaponController>();
        
        if (weaponController!=null)
        {
            Debug.Log("34234");
            weaponController.AddWeapon(_weaponPrefab);
            Destroy(gameObject);
        }
    }
}
