using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [Header("Background Stuff")]
    [SerializeField] private GameObject backGround;
    [SerializeField] private float _bgSpeed = 0.1f;
    private Vector2 _bgOffset;

    [Header("Player Variables")]
    [SerializeField] private GameObject _player;
    [SerializeField] private bool _isPlayerTypeSpeed = false;
    [SerializeField] private float _dashValue = 0.05f;
    [SerializeField] private int _scoreToAward = 10;
    [SerializeField] private int _currScore;
    [SerializeField] private int _extraLife = 1;
    [SerializeField] private float _randomChance = 3f; //random Chance for PowerUp to spawn

    private void Start()
    {
       // PreGameStart();
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
            _player.GetComponentInChildren<Player>()._getSetDashV += _dashValue; //default val 0.05
        }
        else
        {
            _dashValue = 0.1f;
            _player.GetComponentInChildren<Player>()._getSetDashV += _dashValue;
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
            _player.GetComponent<Player>()._getSetPlayerLife += _extraLife;
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

    private void DoGameReset()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Button_Default()
    {
        _player.SetActive(true);
        _player.GetComponentInChildren<SpriteRenderer>().material.color = Color.gray;
        UIManager.Instance.TogglePanelPlayerSelect(false);
        GameStart();
    }

    public void Button_Tank()
    {
        _player.SetActive(true);
        _player.GetComponent<Player>()._getSetPlayerLife = 5;
        _player.GetComponentInChildren<SpriteRenderer>().material.color = Color.black;
        UIManager.Instance.TogglePanelPlayerSelect(false);
        GameStart();
    }

    public void Button_Speed()
    {
        _player.SetActive(true);
        _isPlayerTypeSpeed = true;
        _player.GetComponentInChildren<SpriteRenderer>().material.color = Color.yellow;
        UIManager.Instance.TogglePanelPlayerSelect(false);
        GameStart();
    }

    public void Button_Restart()
    {
        UIManager.Instance.ToggleGameOverPanel(false);
        DoGameReset();
    }


}
