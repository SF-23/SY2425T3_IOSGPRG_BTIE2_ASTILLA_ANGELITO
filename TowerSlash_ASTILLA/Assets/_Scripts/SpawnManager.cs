using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    public bool _canSpawn = true;
    public List<GameObject> _enemyList;
    [SerializeField] private GameObject _enemyToSpawn;
    [SerializeField] private float _maxSpawnDelay = 5f;
    [SerializeField] private float _minSpawnDelay = 2f;

    public void StartWave()
    {
        StartCoroutine(CO_StartWave());
    }

    public void StopWave() 
    {
        StopCoroutine(CO_StartWave());
    }

    public void ClearEnemyList()
    {
        for(int i = 0;  i < _enemyList.Count; i++)
        {
            Destroy(_enemyList[i] );
        }
        _enemyList.Clear();
    }

    public void RemoveEnemy(GameObject _enemyToRemove)
    {
        _enemyList.Remove(_enemyToRemove);
    }

    private void SpawnEnemy()
    {
        GameObject _enemy =  Instantiate(_enemyToSpawn, this.transform.position, this.transform.rotation);
        _enemy.GetComponent<Enemy>().enabled = true;
        _enemyList.Add(_enemy);
    }

    private IEnumerator CO_StartWave()
    {
        while (_canSpawn)
        {
            SpawnEnemy();
            float _currentSpawnDelay = Random.Range(_minSpawnDelay, _maxSpawnDelay);
            Debug.Log("Next enemy will spawn in: " + _currentSpawnDelay.ToString("F2") + " seconds.");
            yield return new WaitForSeconds(_currentSpawnDelay);
        }
    }
}
