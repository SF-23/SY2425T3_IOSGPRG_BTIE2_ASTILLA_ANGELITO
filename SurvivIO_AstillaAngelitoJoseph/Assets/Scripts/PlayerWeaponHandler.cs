using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWeaponHandler : MonoBehaviour
{
    [Header("Weapons In Hand")]
    [SerializeField] private GameObject[] _weaponInHand;
    [SerializeField] private Weapon currentEquippedWeapon;

    [Header("Rifle Ammo")]
    [SerializeField] private int _currRifleAmmoCount;
    [SerializeField] private int _maxRifleAmmoCount;
    [Header("Shotgun Ammo")]
    [SerializeField] private int _currShottyAmmoCount;
    [SerializeField] private int _maxShottyAmmoCount;
    [Header("Pistol Ammo")]
    [SerializeField] private int _currPistolAmmoCount;
    [SerializeField] private int _maxPistolAmmoCount;

    public UnityEvent OnShootButtonPressed;

    private void Awake()
    {
        if (OnShootButtonPressed == null)
            OnShootButtonPressed = new UnityEvent();

        OnShootButtonPressed.AddListener(CallCurrentWeaponFire);
    }

    public void CallCurrentWeaponFire()
    {
        if (currentEquippedWeapon != null)
        {
            currentEquippedWeapon.Button_FireWeapon(); // Call the Button_FireWeapon from the base Weapon class
        }
        else
        {
            Debug.LogWarning("No weapon equipped to fire!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy(collision.gameObject);
        if (collision.gameObject.GetComponent<Ammo>() != null)
        {
            Ammo ammo = collision.gameObject.GetComponent<Ammo>();

            switch(ammo._ammoType)
            {
                case AmmoType.pistolAmmo:
                    _currPistolAmmoCount++;
                    UiManager.Instance.PistolAmmoUpdate(_currPistolAmmoCount);
                    break;
                case AmmoType.shottyAmmo:
                    _currShottyAmmoCount++;
                    UiManager.Instance.ShottyAmmoUpdate(_currShottyAmmoCount);
                    break;
                case AmmoType.rifleAmmo:
                    _currRifleAmmoCount++;
                    UiManager.Instance.RifleAmmoUpdate(_currRifleAmmoCount);
                    break;
                default:
                    break;
            }

            Destroy(ammo.gameObject);
        }
    }
}
