using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] public List<GameObject> _enemyList;
    [SerializeField] private GameObject _enemyToSpawn;
    [SerializeField] private int _spawnCount = 5;
    [SerializeField] private float _maxSpawnDelay = 5f;
    [SerializeField] private float _minSpawnDelay = 2f;
    //[SerializeField] private float _spawnDelay = 5f;   

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CO_StartWave());
    }

    private IEnumerator CO_StartWave()
    {
        while(_spawnCount > 0)
        {
            SpawnEnemy();
            float _currentSpawnDelay = Random.Range(_minSpawnDelay, _maxSpawnDelay);
            Debug.Log("Next enemy will spawn in: " + _currentSpawnDelay.ToString("F2") + " seconds.");
            yield return new WaitForSeconds(_currentSpawnDelay);
            _spawnCount--;
        }
    }

    private void SpawnEnemy()
    {
        _enemyToSpawn =  Instantiate(_enemyToSpawn, this.transform.position, this.transform.rotation);
        _enemyList.Add(_enemyToSpawn);
    }

    public void DeListEnemy(GameObject enemy)
    {
        _enemyList.Remove(enemy);
    }
}
