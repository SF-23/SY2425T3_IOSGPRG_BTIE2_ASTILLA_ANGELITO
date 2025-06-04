using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Background Stuff")]
    [SerializeField] private GameObject backGround;
    [SerializeField] private float _bgSpeed = 0.1f;
    private Vector2 _bgOffset;

    [Header("Player Variables")]
    [SerializeField] private Player _player;
    [SerializeField] private bool _isPlayerTypeSpeed = false;
    [SerializeField] private float _dashValue = 0.05f;
    [SerializeField] private int _scoreToAward = 10;
    [SerializeField] private int _currScore;
    [SerializeField] private int _extraLife = 1;
    [SerializeField] private float _randomChance = 3f; //random Chance for PowerUp to spawn

    private void Start()
    {
        PreGameStart();
    }

    // Update is called once per frame
    void Update()
    {
        MoveBackGround();
    }

    private void MoveBackGround()
    {
        _bgOffset.x += _bgSpeed * Time.deltaTime;

        backGround.GetComponent<SpriteRenderer>().material.mainTextureOffset = _bgOffset;
    }

    public void PlayerDashPlus()
    {
        if(!_isPlayerTypeSpeed)
        {
            _player._getSetDashV += _dashValue; //default val 0.05
        }
        else
        {
            _dashValue = 0.1f;
            _player._getSetDashV += _dashValue;
        }
       
    }

    public void AwardScore()
    {
        _currScore += _scoreToAward;
        UIManager.Instance.ScoreUiUpdate(_currScore);
    }

    public void RewardPowerUp()
    {
        float randomValue = Random.Range(0, 100);

        if(randomValue <= _randomChance)
        {
            _player._setPlayerLife += _extraLife;
        }
    }

    private void PreGameStart()
    {
        Time.timeScale = 0f;
    }

    private void GameStart()
    {
        Time.timeScale = 1f;
    }

    public void Button_Default()
    {
        _player.GetComponent<SpriteRenderer>().material.color = Color.gray;
        UIManager.Instance.TogglePanelPlayerSelect(false);
        GameStart();
    }

    public void Button_Tank()
    {
        _player.GetComponent<SpriteRenderer>().material.color = Color.black;
        _player._setPlayerLife = 5;
        UIManager.Instance.TogglePanelPlayerSelect(false);
        GameStart();
    }

    public void Button_Speed()
    {
        _player.GetComponent<SpriteRenderer>().material.color = Color.yellow;
        _isPlayerTypeSpeed = true;
        UIManager.Instance.TogglePanelPlayerSelect(false);
        GameStart();
    }


}
