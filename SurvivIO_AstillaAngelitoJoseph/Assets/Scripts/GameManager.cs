using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject _player;

    // Start is called before the first frame update
    private void Start()
    {
       
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void Button_PlayerShoot()
    {
        _player.GetComponent<PlayerWeaponHandler>().CurrentWeaponFire();
    }

    public void Button_SpamShoot()
    {
        if(_player.GetComponent<PlayerWeaponHandler>()._currentEquippedWeapon._weaponStats._weaponType == WeaponType.rifle)
        {
            _player.GetComponent<PlayerWeaponHandler>().CurrentWeaponFire();
        }
    }

    public void Button_EquipPrimary()
    {
        _player.GetComponent<PlayerWeaponHandler>().EquipPrimaryWeapon();
        Debug.Log("Equipped Primary");
    }

    public void Button_EquipSecondary()
    {
        _player.GetComponent<PlayerWeaponHandler>().EquipSecondaryWeapon();
        Debug.Log("Equipped Secondary");
    }

    private IEnumerator CO_FireDelay()
    {
        yield return new WaitForSeconds(30);
        _player.GetComponent<PlayerWeaponHandler>().CurrentWeaponFire();
    }
}
