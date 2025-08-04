using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private HealthComponent _healthComponent;
    [SerializeField] private Slider _healthBar;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Bullet>() != null)
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();

            _healthComponent.ThisTakeDmg(bullet.GetBulletDmg());
            _healthBar.value = _healthComponent.GetCurrentHP();
            PlayerDeath();
        }
    }

    private void PlayerDeath()
    {
        if(!_healthComponent.GetIsAlive)
        {
            GameManager.Instance.DoGameOver();
            gameObject.SetActive(false);
        }
    }
}
