using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private List<Enemy> list_enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.GetComponentInChildren<ArrowClass>()._isPlayerNear = true;
            list_enemy.Add(enemy);
        }
    }
}
