using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroy : MonoBehaviour
{
    [SerializeField] private EnemyAI _enemy;
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<HealthComponent>() != null)
        {
            //_enemy.DestroyTarget(collision.gameObject.GetComponent<HealthComponent>());
        }
    }
}
