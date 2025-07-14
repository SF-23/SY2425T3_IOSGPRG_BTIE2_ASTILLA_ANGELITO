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

    private void Start()
    {
        //to set pistol as the main gun
        //_currentEquippedWeapon = _secondaryWeapon;
    }

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
                    SetCurrentWeapon(_weaponInHand[0]);
                    break;
                case LootType.lootRifle:
                    _weaponInHand[1].SetActive(true);
                    SetCurrentWeapon(_weaponInHand[1]);
                    break;
                case LootType.lootShotty:
                    _weaponInHand[2].SetActive(true);
                    SetCurrentWeapon(_weaponInHand[2]);
                    break;
                default:
                    break;
            }
            Destroy(_lootItem.gameObject);
        } 
    }



    private void SetCurrentWeapon(GameObject weapon)
    {
        if(_currentEquippedWeapon == null)
        {
            _currentEquippedWeapon = weapon.GetComponent<Weapon>();
        }
        else if(_secondaryWeapon == null && _currentEquippedWeapon != null)
        {
            _secondaryWeapon = _currentEquippedWeapon;
        }
    }
}
