using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] public List<GameObject> _enemyList;
    [SerializeField] private GameObject _enemyToSpawn;
    [SerializeField] private int _spawnCount = 5;
    [SerializeField] private float _spawnDelay = 5f;   

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
            yield return new WaitForSeconds(_spawnDelay);
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
