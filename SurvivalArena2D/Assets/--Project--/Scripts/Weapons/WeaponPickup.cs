using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private GameObject _weaponPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IWeaponController weaponController = GetComponentInChildren<IWeaponController>();

        if(weaponController!=null)
        {
            weaponController.AddWeapon(_weaponPrefab);
            Destroy(gameObject);
        }
    }
}
