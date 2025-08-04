using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject _player;
    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private LootSpawner _lootSpawner;
    [SerializeField] private bool _isWin;

    public void DoGameOver()
    {
        if(!_player.GetComponent<HealthComponent>().GetIsAlive)
        {
            Time.timeScale = 0f;
            UiManager.Instance.ToggleGameOver(true);
            ResetGame();
        }
    }

    public void DelistEnemy(GameObject enemy)
    {
        _spawnManager._enemyList.Remove(enemy);
        UiManager.Instance.EnemyCountUpdate(_spawnManager._enemyList.Count);
    }

    public void Button_RestartGame()
    {
        //_player.transform.position = new Vector3(0, 0, 0);

        _player.SetActive(true);

        if(_isWin)
        {
            UiManager.Instance.ToggleWin(false);
        }
        else
        {
            UiManager.Instance.ToggleGameOver(false);
        }

        Time.timeScale = 1f;
        DoStartGame();
    }

    public void Button_StartGame()
    { 
        UiManager.Instance.ToggleStart(true);
        Time.timeScale = 1f;
        DoStartGame();
    }

    public void Button_PlayerShoot()
    {
        _player.GetComponent<PlayerWeaponHandler>().CurrentWeaponFire();
    }

    public void Button_SpamShoot()
    {
        if(_player.GetComponent<PlayerWeaponHandler>()._currentEquippedWeapon._weaponStats._weaponType == WeaponType.rifle)
        {
            _player.GetComponent<PlayerWeaponHandler>().CurrentWeaponFire();
        }
    }

    public void Button_EquipPrimary()
    {
        _player.GetComponent<PlayerWeaponHandler>().EquipPrimaryWeapon();
        Debug.Log("Equipped Primary");
    }

    public void Button_EquipSecondary()
    {
        _player.GetComponent<PlayerWeaponHandler>().EquipSecondaryWeapon();
        Debug.Log("Equipped Secondary");
    }

    // Start is called before the first frame update
    private void Start()
    {
        Time.timeScale = 0f;
    }

    private void Update() 
    {
        DoGameWin();
    }

    private void DoGameWin()
    {
        if (_player.GetComponent<HealthComponent>().GetIsAlive && _spawnManager._enemyList.Count <= 0)
        {
            Time.timeScale = 0f;
            _isWin = true;
            ResetGame();
            UiManager.Instance.ToggleWin(true);
        }
    }

    private void ResetGame()
    {
        _player.GetComponent<PlayerWeaponHandler>().ResetWeaponHandler();

        //Clear enemies still present
        for (int i = 0; i < _spawnManager._enemyList.Count; i++)
        {
            Destroy(_spawnManager._enemyList[i]);
        }
        _spawnManager._enemyList.Clear();

        //Clear loot still present
        for (int i = 0; i < _lootSpawner._lootables.Count; i++)
        {
            Destroy(_lootSpawner._lootables[i]);
        }
        _lootSpawner._lootables.Clear();
    }

    private void DoStartGame()
    {
        _player.SetActive(true);

        for (int i = 0; i < _spawnManager._spawnCount; i++)
        {
            _spawnManager.SpawnEnemy();
        }

        _lootSpawner.SpawnLootInAllAreas();
    }
}
