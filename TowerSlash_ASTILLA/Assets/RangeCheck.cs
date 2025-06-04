using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();           //regardless of which enemy is near, I want the boolean for the arrowBG to appear
            enemy.GetComponentInChildren<ArrowClass>()._setIsPlayerNear = true;

            if (SpawnManager.Instance._enemyList.Count > 0)
            {
                SpawnManager.Instance._enemyList[0].GetComponent<Enemy>()._setCanSwipe = true;  //this is to specifically target the 1st in the index of the list
                Debug.Log("The enemy added is the first game object in the list!");
            }
        }
    }
}
