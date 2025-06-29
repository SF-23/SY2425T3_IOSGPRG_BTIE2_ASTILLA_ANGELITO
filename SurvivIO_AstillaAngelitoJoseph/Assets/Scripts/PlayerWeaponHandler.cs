using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponHandler : MonoBehaviour
{
    [Header("Rifle Ammo")]
    [SerializeField] private int _currRifleAmmoCount;
    [SerializeField] private int _maxRifleAmmoCount;
    [Header("Shotgun Ammo")]
    [SerializeField] private int _currShottyAmmoCount;
    [SerializeField] private int _maxShottyAmmoCount;
    [Header("Pistol Ammo")]
    [SerializeField] private int _currPistolAmmoCount;
    [SerializeField] private int _maxPistolAmmoCount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Destroy(collision.gameObject);
        if (collision.gameObject.GetComponent<Ammo>() != null)
        {
            Ammo ammo = collision.gameObject.GetComponent<Ammo>();

            switch(ammo._ammoType)
            {
                case AmmoType.pistol:
                    _currPistolAmmoCount++;
                    UiManager.Instance.PistolAmmoUpdate(_currPistolAmmoCount);
                    break;
                case AmmoType.shotty:
                    _currShottyAmmoCount++;
                    UiManager.Instance.ShottyAmmoUpdate(_currShottyAmmoCount);
                    break;
                case AmmoType.rifle:
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
