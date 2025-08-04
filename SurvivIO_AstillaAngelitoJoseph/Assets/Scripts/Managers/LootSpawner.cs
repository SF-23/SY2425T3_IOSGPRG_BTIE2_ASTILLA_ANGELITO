using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [Header("Lootables")]
    public List<GameObject> _lootables;
    [SerializeField] private GameObject[] _weaponLoot;
    [SerializeField] private GameObject[] _ammoLoot;

    [SerializeField] private BoxCollider2D[] _spawnArea;
    [SerializeField] private int _numberOfLootItemsToSpawn = 10;

    public void SpawnLootInAllAreas()
    {

        if (_spawnArea == null || _spawnArea.Length == 0)
        {
            Debug.LogWarning("No spawn areas have been assigned. Please assign BoxCollider2D objects to the _spawnAreas array.");
            return;
        }

        foreach (BoxCollider2D spawnArea in _spawnArea)
        {
            SpawnLootInSingleArea(spawnArea);
        }
    }

    private void SpawnLootInSingleArea(BoxCollider2D _spawnArea)
    { 
        float _weaponLootChance = 0.3f;
        //float _ammoLootChance = 0.7f;

        for (int i = 0; i < _numberOfLootItemsToSpawn; i++)
        {
            Vector2 randomSpawnPosition = Vector2.zero;
            
            randomSpawnPosition = RandomSpawnPointInCollider(_spawnArea);

            if (true)
            {
                GameObject itemToInstantiate = null;
                float spawnRoll = Random.Range(0f, 1f);

                if (spawnRoll <= _weaponLootChance && _weaponLoot.Length > 0)
                {
                    // Spawn a random weapon from the array
                    itemToInstantiate = _weaponLoot[Random.Range(0, _weaponLoot.Length)];
                }
                else if (_ammoLoot.Length > 0) 
                {
                    // Spawn a random ammo type from the array
                    itemToInstantiate = _ammoLoot[Random.Range(0, _ammoLoot.Length)];
                }

                if (itemToInstantiate != null)
                {
                    GameObject spawnedItem = Instantiate(itemToInstantiate, randomSpawnPosition, Quaternion.identity);
                    _lootables.Add(spawnedItem);
                }
            }
        }
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
