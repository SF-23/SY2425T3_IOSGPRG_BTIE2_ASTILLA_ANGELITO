using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCheck : MonoBehaviour
{
    [SerializeField] private Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            //regardless of which enemy is near, I want the boolean for the arrowBG to appear
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();           
            enemy.GetComponentInChildren<ArrowClass>()._SetIsPlayerNear = true;


            player.EnemyDeteced(enemy);
        }
    }
}
