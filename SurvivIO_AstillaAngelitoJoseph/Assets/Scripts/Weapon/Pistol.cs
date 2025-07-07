using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    public override void FireWeapon()
    {
        SpawnBullet();
        _weaponStats._currentAmmo--;
        //insert sound
        Mathf.Clamp(_weaponStats._currentAmmo, 0, _weaponStats._maxAmmo);
    }

    private void SpawnBullet()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _barrel.position, _barrel.rotation);
        bullet.GetComponent<Bullet>().SetBulletDmg(_weaponStats._bulletDamage);
        bullet.GetComponent<Bullet>().SetBulletRange(_weaponStats._weaponRange);
    }
}
