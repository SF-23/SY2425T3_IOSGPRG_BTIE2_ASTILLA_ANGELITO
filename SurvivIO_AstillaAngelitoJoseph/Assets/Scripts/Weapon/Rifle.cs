using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    public override void Shoot()
    {
        SpawnBullet();
        _weaponStats._currentAmmo--;
        //insert sound
        Mathf.Clamp(_weaponStats._currentAmmo, 0, _weaponStats._maxAmmo);
    }

    private void SpawnBullet()
    {
        GameObject bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetBulletDmg(_weaponStats._bulletDamage);
        bullet.GetComponent<Bullet>().SetBulletRange(_weaponStats._weaponRange);
    }
}
