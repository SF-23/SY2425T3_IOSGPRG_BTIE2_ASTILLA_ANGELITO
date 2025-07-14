using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Shotty : Weapon
{
    public override void Shoot()
    {
        _weaponStats._currentAmmo--;
        for (int i = 0; i < 8; i++) 
        {
            SpawnBullet();
        }
        //insert sound
        Mathf.Clamp(_weaponStats._currentAmmo, 0, _weaponStats._maxAmmo);
    }

    private void SpawnBullet()
    {
        float offsetZ = Random.Range(-5f, 5f);

        Quaternion randomBarrelRotation = Quaternion.Euler(0, 0, offsetZ);
        Quaternion finalBarrelRotation = _barrel.rotation * randomBarrelRotation;

        GameObject bullet = Instantiate(_bulletPrefab, transform.position, finalBarrelRotation);
        bullet.GetComponent<Bullet>().SetBulletDmg(_weaponStats._bulletDamage);
        bullet.GetComponent<Bullet>().SetBulletRange(_weaponStats._weaponRange);
    }
}
