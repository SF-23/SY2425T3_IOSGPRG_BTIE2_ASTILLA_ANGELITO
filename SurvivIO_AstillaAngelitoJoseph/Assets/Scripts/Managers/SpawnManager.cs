using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> _enemyList;
    public int _spawnCount;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private BoxCollider2D _spawnArea;

    public void SpawnEnemy()
    {
        Vector2 randomSpawnPosition = Vector2.zero;
        randomSpawnPosition = RandomSpawnPointInCollider(_spawnArea);
        GameObject enemy = Instantiate(_enemy, randomSpawnPosition, Quaternion.identity);
        _enemyList.Add(enemy);
        UiManager.Instance.EnemyCountUpdate(_enemyList.Count);
    }

    private Vector2 RandomSpawnPointInCollider(BoxCollider2D boxCollider)
    {

        Vector2 _center = boxCollider.bounds.center;
        Vector2 _extents = boxCollider.bounds.extents;

        float randomX = Random.Range(_center.x - _extents.x, _center.x + _extents.x);
        float randomY = Random.Range(_center.y - _extents.y, _center.y + _extents.y);

        return new Vector2(randomX, randomY);
    }
}
