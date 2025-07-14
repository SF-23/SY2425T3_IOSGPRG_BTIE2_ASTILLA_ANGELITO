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

    public void Button_EquipPrimary()
    {
        _player.GetComponent<PlayerWeaponHandler>().EquipPrimaryWeapon();
    }

    public void Button_EquipSecondary()
    {
        _player.GetComponent<PlayerWeaponHandler>().EquipSecondaryWeapon();
    }
}
