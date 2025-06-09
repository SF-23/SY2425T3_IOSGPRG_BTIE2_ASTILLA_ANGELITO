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

    [Header("Booleans")]
    [SerializeField] private bool isKilled = false;
    [SerializeField] private bool _canSwipe = false;
    [SerializeField] private bool _isCorrectSwipe;
    [SerializeField] private bool _isPlayerNear;


    public bool _setPlayerNear { get { return _setPlayerNear; } set { _setPlayerNear = value; } }

    public bool _getIsKilled { get { return isKilled; } }

    public bool _setCanSwipe { get { return _canSwipe; } set { _canSwipe = value; } }

    private void Start()
    {
        _enemyType = (EnemyType)Random.Range(0, 3);
        SetArrowColor();
    }

    // Update is called once per frame
    private void Update()
    {
        EnemyMove();

        if(_canSwipe && SwipeDirectionManager.Instance.IsSwipeDetectedThisFrame())
        {
            CompareSwipeDirection();
        }
    }

    public void DoEnemyCollidePlayer(bool isDashing)
    {
        SpawnManager.Instance.DeListEnemy(this.gameObject);

        if (isDashing)
        {
            GameManager.Instance.AwardScore();
            GameManager.Instance.RewardPowerUp();
        }

        Destroy(this.gameObject);
    }

    public void CompareSwipeDirection()
    {
        SwipeDirectionManager sDM = SwipeDirectionManager.Instance;

        if (_enemyType == EnemyType.Opp)    //red means opposite swipe
        {
            if (
               (_arrow._getEnumArrowDir == Direction.Right && sDM.enum_currentDir == Direction.Left) ||
               (_arrow._getEnumArrowDir == Direction.Left && sDM.enum_currentDir == Direction.Right) ||
               (_arrow._getEnumArrowDir == Direction.Up && sDM.enum_currentDir == Direction.Down) ||
               (_arrow._getEnumArrowDir == Direction.Down && sDM.enum_currentDir == Direction.Up)
              )
            {
                _isCorrectSwipe = true;
            }
        }
        else
        {
            if (_arrow._getEnumArrowDir == sDM.enum_currentDir)
            {
                _isCorrectSwipe = true;
            }
        }

        if (_isCorrectSwipe)
        {
            DoSwipeDestoryEnemy();
        }
        else
        {
            GameManager.Instance.PlayerWrongSwipe();
        }
    }

    private void EnemyMove()
    {
        this.transform.Translate(0, -speed * Time.deltaTime, 0);
    }

    private void DoSwipeDestoryEnemy()
    {

        SpawnManager.Instance.DeListEnemy(this.gameObject);
        _canSwipe = false;
        GameManager.Instance.PlayerDashPlus();
        GameManager.Instance.AwardScore();
        GameManager.Instance.RewardPowerUp();

        if (!isKilled)
        {
            Destroy(this.gameObject);
        }
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

   
}
