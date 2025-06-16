using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [Header("Background Stuff")]
    [SerializeField] private GameObject backGround;
    [SerializeField] private float _bgSpeed = 0.1f;   

    [Header("Player Variables")]
    [SerializeField] private GameObject _player;
    [SerializeField] private bool _isPlayerTypeSpeed = false;
    [SerializeField] private float _dashValue = 0.05f;
    [SerializeField] private int _scoreToAward = 10;
    [SerializeField] private int _currScore;
    [SerializeField] private int _extraLife = 1;

    //random Chance for PowerUp to spawn
    [SerializeField] private float _randomChance = 3f; 

    private Vector2 _bgOffset;

    public System.Action OnRestart{ get; set; }

    private void Start()
    {
        PreGameStart();
    }

    // Update is called once per frame
    private void Update()
    {
        MoveBackGround();
    }

    public void PlayerWrongSwipe()
    {
        if (_player != null)
        {
            _player.GetComponentInChildren<Player>().TakeDmg();
        }
    }

    public void Button_Default()
    {
        _player.SetActive(true);
        _player.GetComponentInChildren<Player>()._GetSetPlayerMaxLife = 3;
        _player.GetComponentInChildren<SpriteRenderer>().material.color = Color.gray;
        UIManager.Instance.TogglePanelPlayerSelect(false);
        GameStart();
    }

    public void Button_Tank()
    {
        _player.SetActive(true);
        _player.GetComponentInChildren<Player>()._GetSetPlayerMaxLife = 5;
        _player.GetComponentInChildren<SpriteRenderer>().material.color = Color.black;
        UIManager.Instance.TogglePanelPlayerSelect(false);
        GameStart();
    }

    public void Button_Speed()
    {
        _player.SetActive(true);
        _isPlayerTypeSpeed = true;
        _player.GetComponentInChildren<Player>()._GetSetPlayerMaxLife = 3;
        _player.GetComponentInChildren<SpriteRenderer>().material.color = Color.yellow;
        UIManager.Instance.TogglePanelPlayerSelect(false);
        GameStart();
    }

    public void Button_Restart()
    {
        UIManager.Instance.ToggleGameOverPanel(false);
        DoGameReset();
    }

    public void PlayerDashPlus()
    {
        if (!_isPlayerTypeSpeed)
        {
            //default val 0.05
            _player.GetComponentInChildren<Player>()._GetSetDashV += _dashValue;
            _player.GetComponentInChildren<Player>().DashSliderUpdate();

        }
        else
        {
            _dashValue = 0.1f;
            _player.GetComponentInChildren<Player>()._GetSetDashV += _dashValue;
            _player.GetComponentInChildren<Player>().DashSliderUpdate();
        }
    }

    public void RewardPlayer(bool isDashing)
    {
        //rewards Player with score and dash
        _currScore += _scoreToAward;
        RewardPowerUp();
        if (!isDashing)
        {
            PlayerDashPlus();
        }

        UIManager.Instance.ScoreUiUpdate(_currScore);
    }

    public void RewardPowerUp()
    {
        float randomValue = Random.Range(0, 100);

        if(randomValue <= _randomChance)
        {
            _player.GetComponentInChildren<Player>()._GetSetPlayerMaxLife += _extraLife;
        }
    }

    private void MoveBackGround()
    {
        _bgOffset.x += _bgSpeed * Time.deltaTime;

        backGround.GetComponent<SpriteRenderer>().material.mainTextureOffset = _bgOffset;
    }

    private void PreGameStart()
    {
        Time.timeScale = 0f;
        UIManager.Instance.TogglePanelPlayerSelect(true);
    }

    private void GameStart()
    {
        Time.timeScale = 1f;
        SpawnManager.Instance.StartWave();
    }

    private void DoGameReset()
    {
        _currScore = 0;
        _player.SetActive(true);
        _player.GetComponent<Player>().LifeReset();
        OnRestart?.Invoke();
        PreGameStart();
    }
}
