using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWeaponHandler : MonoBehaviour
{
    [Header("Weapons In Hand")]
    //pistol, rifle, shotty (0,1,2)
    [SerializeField] public Weapon _currentEquippedWeapon;
    [SerializeField] private GameObject[] _weaponInHand; 
    [SerializeField] private Weapon _primaryWeapon;
    [SerializeField] private Weapon _secondaryWeapon;

    [Header("Pistol Ammo")]
    [SerializeField] private int _currPistolAmmoCount;
    [SerializeField] private int _maxPistolAmmoCount;

    [Header("Rifle Ammo")]
    [SerializeField] private int _currRifleAmmoCount;
    [SerializeField] private int _maxRifleAmmoCount;

    [Header("Shotgun Ammo")]
    [SerializeField] private int _currShottyAmmoCount;
    [SerializeField] private int _maxShottyAmmoCount;

    public void ResetWeaponHandler()
    {
        _primaryWeapon = null; 
        _secondaryWeapon = null;
        _currentEquippedWeapon = null;
        _currPistolAmmoCount = 0;
        _currRifleAmmoCount = 0;
        _currShottyAmmoCount = 0;
    }

    public void CurrentWeaponFire()
    {
        if (_currentEquippedWeapon != null)
        {
            StartCoroutine(_currentEquippedWeapon.CO_FiringWeapon());
            UiManager.Instance.CurrentWeaponAmmoUpdate(_currentEquippedWeapon._weaponStats._currentAmmo, 
                                                       _currentEquippedWeapon._weaponStats._clipCapacity);
        }
        else
        {
            Debug.LogWarning("No weapon equipped to fire!");
        }
    }

    public void ReloadCurrentWeapon()
    {
        if (_currentEquippedWeapon == null)
        {
            Debug.LogWarning("No weapon equipped to reload!");
            return;
        }

        int clipCapacity = _currentEquippedWeapon._weaponStats._clipCapacity;
        int currentAmmoInMag = _currentEquippedWeapon._weaponStats._currentAmmo;
        int ammoNeeded = clipCapacity - currentAmmoInMag;

        if (ammoNeeded <= 0)
        {
            Debug.Log("Magazine is already full.");
            return;
        }

        int totalAmmoCarried = 0;
        switch (_currentEquippedWeapon._weaponStats._weaponType)
        {
            case WeaponType.pistol:
                totalAmmoCarried = _currPistolAmmoCount;
                break;
            case WeaponType.rifle:
                totalAmmoCarried = _currRifleAmmoCount;
                break;
            case WeaponType.shotty:
                totalAmmoCarried = _currShottyAmmoCount;
                break;
        }

        if (totalAmmoCarried > 0)
        {
            // Calculate how much ammo to take from the total carry
            int ammoToLoad = Mathf.Min(ammoNeeded, totalAmmoCarried);

            // Update the current weapon's ammo
            _currentEquippedWeapon._weaponStats._currentAmmo += ammoToLoad;

            // Update the player's total carried ammo
            switch (_currentEquippedWeapon._weaponStats._weaponType)
            {
                case WeaponType.pistol:
                    _currPistolAmmoCount -= ammoToLoad;
                    UiManager.Instance.PistolAmmoUpdate(_currPistolAmmoCount);
                    break;
                case WeaponType.rifle:
                    _currRifleAmmoCount -= ammoToLoad;
                    UiManager.Instance.RifleAmmoUpdate(_currRifleAmmoCount);
                    break;
                case WeaponType.shotty:
                    _currShottyAmmoCount -= ammoToLoad;
                    UiManager.Instance.ShottyAmmoUpdate(_currShottyAmmoCount);
                    break;
            }

            // Update the UI for the equipped weapon's magazine
            UiManager.Instance.CurrentWeaponAmmoUpdate(_currentEquippedWeapon._weaponStats._currentAmmo, clipCapacity);
            Debug.Log($"Reloaded {_currentEquippedWeapon._weaponStats._weaponType}. Loaded {ammoToLoad} rounds.");
        }
        else
        {
            Debug.Log("No Ammo to reload!");
        }
    }

    // Call this method from your UI button for primary weapon
    public void EquipPrimaryWeapon()
    {
        if (_primaryWeapon != null)
        {
            _currentEquippedWeapon = _primaryWeapon;
            UiManager.Instance.CurrentWeaponAmmoUpdate(_currentEquippedWeapon._weaponStats._currentAmmo, _currentEquippedWeapon._weaponStats._maxAmmo);
            UpdateWeaponVisuals();
        }
    }

    // Call this method from your UI button for secondary weapon
    public void EquipSecondaryWeapon()
    {
        if (_secondaryWeapon != null)
        {
            _currentEquippedWeapon = _secondaryWeapon;
            UiManager.Instance.CurrentWeaponAmmoUpdate(_currentEquippedWeapon._weaponStats._currentAmmo, _currentEquippedWeapon._weaponStats._maxAmmo);
            UpdateWeaponVisuals();
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
            UiManager.Instance.ImageWeaponUpdate(0, true);
            if (_secondaryWeapon != null)
            {
                // Discard the old pistol, deduct whatever ammo here
                Debug.Log("Discarded old secondary weapon: " + _secondaryWeapon.name);
                // and we'll just re-assign the reference. The visual will be handled by UpdateWeaponVisuals
            }
            _secondaryWeapon = newWeapon;
        }
        // It's an Automatic Rifle or Shotgun (primary weapon types)
        else if (weaponLootType == LootType.lootRifle || weaponLootType == LootType.lootShotty)
        {
            if (weaponLootType == LootType.lootRifle)
            {
                UiManager.Instance.ImageWeaponUpdate(1, true);
                UiManager.Instance.ImageWeaponUpdate(2, false);
            }
            else
            {
                UiManager.Instance.ImageWeaponUpdate(1, false);
                UiManager.Instance.ImageWeaponUpdate(2, true);
            }

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy(collision.gameObject);
        if (collision.gameObject.GetComponent<LootableItem>() != null)
        {
            LootableItem _lootItem = collision.gameObject.GetComponent<LootableItem>();

            switch(_lootItem._lootType)
            {
                case LootType.pistolAmmo:

                    if(_currPistolAmmoCount <= 90)
                    {
                        _currPistolAmmoCount += Random.Range(1, 8);
                        Mathf.Clamp(_currPistolAmmoCount, 1, _maxPistolAmmoCount);
                    }
                    
                    UiManager.Instance.PistolAmmoUpdate(_currPistolAmmoCount);

                    break;
                case LootType.shottyAmmo:

                    if(_currShottyAmmoCount <= 60)
                    {
                        _currShottyAmmoCount += Random.Range(1, 2);
                        Mathf.Clamp(_currPistolAmmoCount, 1, _maxShottyAmmoCount);
                    }

                    UiManager.Instance.ShottyAmmoUpdate(_currShottyAmmoCount);
                    break;

                case LootType.rifleAmmo:

                    if(_currRifleAmmoCount <= 120)
                    {
                        _currRifleAmmoCount += Random.Range(5, 15);
                        Mathf.Clamp(_currRifleAmmoCount, 1, _maxRifleAmmoCount);
                    }
                    
                    UiManager.Instance.RifleAmmoUpdate(_currRifleAmmoCount);
                    break;
                case LootType.lootPistol:
                    _weaponInHand[0].SetActive(true);
                    HandleWeaponPickup(_weaponInHand[0].GetComponent<Weapon>(), LootType.lootPistol);
                    break;
                case LootType.lootRifle:
                    _weaponInHand[1].SetActive(true);
                    HandleWeaponPickup(_weaponInHand[1].GetComponent<Weapon>(), LootType.lootRifle);
                    break;
                case LootType.lootShotty:
                    _weaponInHand[2].SetActive(true);
                    HandleWeaponPickup(_weaponInHand[2].GetComponent<Weapon>(), LootType.lootShotty);
                    break;
                default:
                    break;
            }
            Destroy(_lootItem.gameObject);
        } 
    }

   

   
}
