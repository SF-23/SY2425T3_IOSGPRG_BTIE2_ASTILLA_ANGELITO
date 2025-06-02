using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Normal, Opp, Random
}

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private float speed = 0.2f;
    [SerializeField] private ArrowClass _arrow;
    
    [SerializeField] private bool isPlayerNear;
    public bool _setPlayerNear { get { return _setPlayerNear; } set { _setPlayerNear = value; } }

    private void Start()
    {
        _enemyType = (EnemyType)Random.Range(0, 3);
        SetArrowColor();
    }
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
        GameManager.Instance.PlayerDashPlus();
        Destroy(this.gameObject);
    }

    private void SetArrowColor()
    {
        switch (_enemyType)
        {
            case EnemyType.Normal:
                _arrow._setEnumArrowColor = ArrowColor.Green;
                break;
            case EnemyType.Opp:
                _arrow._setEnumArrowColor = ArrowColor.Red;
                break;
            case EnemyType.Random:
                _arrow._setEnumArrowColor = ArrowColor.Yellow;
                break;
            default:
                return;
        }
    }

    private void SwipeDestryEnemy()
    {
        SwipeDirectionManager sDM = SwipeDirectionManager.Instance;

        if (_arrow._getIsColorRed)    //red means opposite swipe
        {
            if(
               (_arrow._getEnumArrowDir == Direction.Right && sDM.enum_currentDir == Direction.Left) || 
               (_arrow._getEnumArrowDir == Direction.Left && sDM.enum_currentDir == Direction.Right) ||
               (_arrow._getEnumArrowDir == Direction.Up && sDM.enum_currentDir == Direction.Down) ||
               (_arrow._getEnumArrowDir == Direction.Down && sDM.enum_currentDir == Direction.Up)
              )
            {
                DoDestoryEnemy();
            }
        }
        else 
        {
            if(_arrow._getEnumArrowDir == sDM.enum_currentDir)
            {
                DoDestoryEnemy();
            }
        }
    }

}
