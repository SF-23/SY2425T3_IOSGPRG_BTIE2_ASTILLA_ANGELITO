using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public List<GameObject> _enemyList;
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

    public void DeListEnemy(GameObject enemy)
    {
        _enemyList.Remove(enemy);
    }

    public void ResetSpawner()
    {
        foreach (GameObject enemy in _enemyList)
        {
            Destroy(enemy);
        }
        _enemyList.Clear();

    }

    private void StartWave()
    {
        GameManager.Instance.OnRestart -= StartWave;
        StopCoroutine(CO_StartWave());
        Start();
    }

    private void SpawnEnemy()
    {
        _enemyToSpawn =  Instantiate(_enemyToSpawn, this.transform.position, this.transform.rotation);
        _enemyToSpawn.GetComponent<Enemy>().enabled = true;
        _enemyList.Add(_enemyToSpawn);
    }

    private IEnumerator CO_StartWave()
    {
        while (_spawnCount > 0)
        {
            SpawnEnemy();
            float _currentSpawnDelay = Random.Range(_minSpawnDelay, _maxSpawnDelay);
            Debug.Log("Next enemy will spawn in: " + _currentSpawnDelay.ToString("F2") + " seconds.");
            yield return new WaitForSeconds(_currentSpawnDelay);
        }
    }
}
