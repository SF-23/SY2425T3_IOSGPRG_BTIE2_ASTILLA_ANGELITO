using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public bool _canSpawn = true;
    [SerializeField] private GameObject _enemyToSpawn;
    [SerializeField] private int _spawnCount = 5;
    [SerializeField] private float _maxSpawnDelay = 5f;
    [SerializeField] private float _minSpawnDelay = 2f;
   

    // Start is called before the first frame update
    private void Start()
    {
        GameManager.Instance.OnRestart += StartWave;
        StartCoroutine(CO_StartWave());
    }

    private void StartWave()
    {
        StopCoroutine(CO_StartWave());
        GameManager.Instance.OnRestart -= StartWave;
        Start();
    }

    private void SpawnEnemy()
    {
        _enemyToSpawn =  Instantiate(_enemyToSpawn, this.transform.position, this.transform.rotation);
        _enemyToSpawn.GetComponent<Enemy>().enabled = true;
    }

    private IEnumerator CO_StartWave()
    {
        while (_spawnCount > 0 && _canSpawn)
        {
            SpawnEnemy();
            float _currentSpawnDelay = Random.Range(_minSpawnDelay, _maxSpawnDelay);
            Debug.Log("Next enemy will spawn in: " + _currentSpawnDelay.ToString("F2") + " seconds.");
            yield return new WaitForSeconds(_currentSpawnDelay);
        }
    }
}
