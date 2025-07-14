using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Shotty : Weapon
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
        float offsetX = Random.Range(-0.1f, 0.1f);
        float offsetY = Random.Range(-0.3f, 0.3f);

        Quaternion randomBarrelRotation = Quaternion.Euler(0f, offsetY, offsetX);
        Quaternion finalBarrelRotation = _barrel.rotation * randomBarrelRotation;

        GameObject bullet = Instantiate(_bulletPrefab, transform.position, finalBarrelRotation);
        bullet.GetComponent<Bullet>().SetBulletDmg(_weaponStats._bulletDamage);
        bullet.GetComponent<Bullet>().SetBulletRange(_weaponStats._weaponRange);
    }
}
