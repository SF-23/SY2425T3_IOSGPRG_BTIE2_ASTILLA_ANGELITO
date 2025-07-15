using System.Collections;
using UnityEngine;

[System.Serializable]
public struct WeaponStats
{
    public WeaponType _weaponType;
    public LootType _ammoType;
    public float _fireRate;
    public float _reloadTime;
    public float _weaponRange;
    public int _maxAmmo; 
    public int _currentAmmo;
    public int _bulletDamage; 
}

public abstract class Weapon : MonoBehaviour
{
    public GameObject _bulletPrefab;
    public Transform _barrel;
    public bool _isFiring;
    public bool _isReloading;
    public bool _isWeaponPickedUp;

    public WeaponStats _weaponStats;

    // Start is called before the first frame update
    private void Start()
    {
        _weaponStats._currentAmmo = _weaponStats._maxAmmo;
    }

    // Update is called once per frame
    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Mouse0) && _isFiring && _isWeaponPickedUp) //&& !GameManager.instance.isGamePause)
        {
            StartCoroutine(CO_FiringWeapon());
        }
        */
    }

    public abstract void Shoot();

    public IEnumerator CO_FiringWeapon()
    {
        _isFiring = false;
        
        if(_weaponStats._currentAmmo <= 0)
        {
            StartCoroutine(CO_ReloadTimer());  
        }
        else
        {
            Shoot();
            StartCoroutine(CO_FireRateHandler());
        }

        yield return null;
    }

    private IEnumerator CO_FireRateHandler()
    {
        float timeToNextFire = 1 / _weaponStats._fireRate;
        yield return new WaitForSeconds(timeToNextFire);
        _isFiring = true;
    }

    private IEnumerator CO_ReloadTimer()
    {
        _isReloading = true;
        _isFiring = false;
        Debug.Log("RELOADING");
        yield return new WaitForSeconds(_weaponStats._reloadTime);
        Debug.Log("RELOADING DONE");
        _weaponStats._currentAmmo = _weaponStats._maxAmmo;
        _isReloading = false;
        _isFiring = true;
    }

}
