using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWeaponHandler : MonoBehaviour
{
    [Header("Weapons In Hand")]
    //pistol, rifle, shotty (0,1,2)
    [SerializeField] private GameObject[] _weaponInHand; 
    [SerializeField] private Weapon _currentEquippedWeapon;
    [SerializeField] private Weapon _primaryWeapon;
    [SerializeField] private Weapon _secondaryWeapon;

    [Header("Rifle Ammo")]
    [SerializeField] private int _currRifleAmmoCount;
    [SerializeField] private int _maxRifleAmmoCount;
    [Header("Shotgun Ammo")]
    [SerializeField] private int _currShottyAmmoCount;
    [SerializeField] private int _maxShottyAmmoCount;
    [Header("Pistol Ammo")]
    [SerializeField] private int _currPistolAmmoCount;
    [SerializeField] private int _maxPistolAmmoCount;

    public void CurrentWeaponFire()
    {
        if (_currentEquippedWeapon != null)
        {
            _currentEquippedWeapon.Shoot();
        }
        else
        {
            Debug.LogWarning("No weapon equipped to fire!");
        }
    }

    // Call this method from your UI button for primary weapon
    public void EquipPrimaryWeapon()
    {
        if (_primaryWeapon != null)
        {
            _currentEquippedWeapon = _primaryWeapon;
            UpdateWeaponVisuals();
        }
    }

    // Call this method from your UI button for secondary weapon
    public void EquipSecondaryWeapon()
    {
        if (_secondaryWeapon != null)
        {
            _currentEquippedWeapon = _secondaryWeapon;
            UpdateWeaponVisuals();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy(collision.gameObject);
        if (collision.gameObject.GetComponent<LootableItem>() != null)
        {
            LootableItem _lootItem = collision.gameObject.GetComponent<LootableItem>();

            switch(_lootItem._lootType)
            {
                case LootType.pistolAmmo:
                    _currPistolAmmoCount += Random.Range(4, 15);
                    UiManager.Instance.PistolAmmoUpdate(_currPistolAmmoCount);
                    break;
                case LootType.shottyAmmo:
                    _currShottyAmmoCount += Random.Range(1, 5);
                    UiManager.Instance.ShottyAmmoUpdate(_currShottyAmmoCount);
                    break;
                case LootType.rifleAmmo:
                    _currRifleAmmoCount += Random.Range(2, 10);
                    UiManager.Instance.RifleAmmoUpdate(_currRifleAmmoCount);
                    break;
                case LootType.lootPistol:
                    _weaponInHand[0].SetActive(true);
                    HandleWeaponPickup(_weaponInHand[0].GetComponent<Weapon>(), LootType.lootPistol);
                    break;
                case LootType.lootRifle:
                    _weaponInHand[1].SetActive(true);
                    HandleWeaponPickup(_weaponInHand[1].GetComponent<Weapon>(), LootType.lootPistol);
                    break;
                case LootType.lootShotty:
                    _weaponInHand[2].SetActive(true);
                    HandleWeaponPickup(_weaponInHand[2].GetComponent<Weapon>(), LootType.lootPistol);
                    break;
                default:
                    break;
            }
            Destroy(_lootItem.gameObject);
        } 
    }

    private void HandleWeaponPickup(Weapon newWeapon, LootType weaponLootType)
    {
        // Deactivate all weapon visuals initially
        foreach (GameObject weaponGO in _weaponInHand)
        {
            weaponGO.SetActive(false);
        }

        if (weaponLootType == LootType.lootPistol)
        {
            // This is a pistol, always goes into the secondary slot
            if (_secondaryWeapon != null)
            {
                // Discard the old pistol, deduct whatever ammo here
                Debug.Log("Discarded old secondary weapon: " + _secondaryWeapon.name);
                // No need to explicitly destroy the GameObject as it's from _weaponInHand array
                // and we'll just re-assign the reference. The visual will be handled by UpdateWeaponVisuals.
            }
            _secondaryWeapon = newWeapon;
        }
        else // It's an Automatic Rifle or Shotgun (primary weapon types)
        {
            if (_primaryWeapon != null)
            {
                // Discard the old primary weapon, deduct whatever ammo here
                Debug.Log("Discarded old primary weapon: " + _primaryWeapon.name);
            }
            _primaryWeapon = newWeapon;
        }

        // After handling the slot, equip the newly picked up weapon
        _currentEquippedWeapon = newWeapon;
        UpdateWeaponVisuals();
        Debug.Log("Picked up and equipped: " + newWeapon.name);
    }

    private void UpdateWeaponVisuals()
    {
        // Deactivate all weapon game objects first
        foreach (GameObject weaponGO in _weaponInHand)
        {
            weaponGO.SetActive(false);
        }

        // Activate the game object of the currently equipped weapon
        if (_currentEquippedWeapon != null)
        {
            _currentEquippedWeapon.gameObject.SetActive(true);
        }
    }
}
