using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotty : Weapon
{
    public override void Shoot()
    {
        SpawnBullet();
    }

    private void SpawnBullet()
    {
        GameObject bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
    }
}
