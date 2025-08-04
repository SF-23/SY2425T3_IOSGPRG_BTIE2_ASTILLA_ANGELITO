using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private HealthComponent _healthComponent;
    [SerializeField] private GameObject[] _weapons;
    [SerializeField] private GameObject _healthBar;
    [SerializeField] private Weapon _mainWeapon;

    public void ShootWeapon()
    {
        if (_mainWeapon != null)
        {
            _mainWeapon.StartCoroutine(_mainWeapon.CO_FiringWeapon());
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (_healthComponent != null)
        {
            _healthBar.GetComponent<Slider>().value = _healthComponent.GetCurrentHP();
            Debug.LogWarning(_healthComponent.GetCurrentHP());
            Debug.LogWarning(_healthBar.GetComponent<Slider>().value.ToString());
        }

        switch (Random.Range(0, _weapons.Length))
        {
            case 0:
                _weapons[0].SetActive(true);
                _mainWeapon = _weapons[0].GetComponent<Pistol>();
                _mainWeapon._isEnemyWeapon = true;
                break;
            case 1:
                _weapons[1].SetActive(true);
                _mainWeapon = _weapons[1].GetComponent<Rifle>();
                _mainWeapon._isEnemyWeapon = true;
                break;
            case 2:
                _weapons[2].SetActive(true);
                _mainWeapon = _weapons[2].GetComponent<Shotty>();
                _mainWeapon._isEnemyWeapon = true;
                break;
            default:
                Debug.LogWarning("Missing Weapon");
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Bullet>() != null) 
        { 
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();

            _healthComponent.ThisTakeDmg(bullet.GetBulletDmg());
            _healthBar.GetComponent<Slider>().value = _healthComponent.GetCurrentHP();
            StartCoroutine(CO_HealthBarVisibility());
            DoDeath();
        }
    }

    private void DoDeath()
    {
        if(!_healthComponent.GetIsAlive)
        {
            GameManager.Instance.DelistEnemy(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    private IEnumerator CO_HealthBarVisibility()
    {
        _healthBar.SetActive(true);
        yield return new WaitForSeconds(5);
        _healthBar.SetActive(false);
    }

    private IEnumerator CO_Revive()
    {
        yield return new WaitForSeconds(5);
        _healthComponent.ResetHealth();
        this.gameObject.SetActive(true);
    }
}
