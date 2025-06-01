using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 0.2f;
    [SerializeField] private ArrowClass arrow;

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
        SwipeDestryEnemy();
    }

    private void EnemyMove()
    {
        this.transform.Translate(0, -speed * Time.deltaTime, 0);
    }

    private void DoDestoryEnemy()
    {
        SpawnManager.Instance.DeListEnemy(this.gameObject);
        Destroy(this.gameObject);
    }

    private void SwipeDestryEnemy()
    {
        SwipeDirectionManager sDM = SwipeDirectionManager.Instance;

        if (arrow._getIsColorRed)    //red means opposite swipe
        {
            if(
               (arrow._getEnumArrowDir == Direction.Right && sDM.enum_currentDir == Direction.Left) || 
               (arrow._getEnumArrowDir == Direction.Left && sDM.enum_currentDir == Direction.Right) ||
               (arrow._getEnumArrowDir == Direction.Up && sDM.enum_currentDir == Direction.Down) ||
               (arrow._getEnumArrowDir == Direction.Down && sDM.enum_currentDir == Direction.Up)
              )
            {
                DoDestoryEnemy();
            }
        }
        else 
        {
            if(arrow._getEnumArrowDir == sDM.enum_currentDir)
            {
                DoDestoryEnemy();
            }
        }
    }

}
