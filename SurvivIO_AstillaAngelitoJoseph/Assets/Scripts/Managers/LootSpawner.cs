using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [Header("Lootables")]
    [SerializeField] private GameObject[] _weaponLoot;
    [SerializeField] private GameObject[] _ammoLoot;

    [SerializeField] private BoxCollider2D _spawnArea;
    [SerializeField] private int _numberOfLootItemsToSpawn = 10;
    [SerializeField] private int _maxSpawnAttempts = 10; 
    [SerializeField] private float _spawnCheckRadius = 0.5f;

    // Start is called before the first frame update
    private void Start()
    {
        SpawnLoot();
        //StartCoroutine(CO_SpawnLoot());
    }

    private void SpawnLoot()
    { 
        float _weaponLootChance = 0.3f;
        //float _ammoLootChance = 0.7f;

        for (int i = 0; i < _numberOfLootItemsToSpawn; i++)
        {
            Vector2 randomSpawnPosition = Vector2.zero;
            //bool positionFound = false;

            /*
            // Attempt to find a non-overlapping spawn position
            for (int attempt = 0; attempt < _maxSpawnAttempts; attempt++)
            {
                randomSpawnPosition = RandomSpawnPointInCollider(_spawnArea);

                // Check for overlaps using Physics2D.OverlapCircle
                Collider2D hitCollider = Physics2D.OverlapCircle(randomSpawnPosition, _spawnCheckRadius);

                if (hitCollider == null) 
                {
                    positionFound = true;
                    break; 
                }
            }
            */
            
            randomSpawnPosition = RandomSpawnPointInCollider(_spawnArea);

            if (true)
            {
                GameObject itemToInstantiate = null;
                float spawnRoll = Random.Range(0f, 100f);

                if (spawnRoll <= _weaponLootChance && _weaponLoot.Length > 0)
                {
                    // Spawn a random weapon from the array
                    itemToInstantiate = _weaponLoot[Random.Range(0, _weaponLoot.Length)];
                }
                else if (_ammoLoot.Length > 0) // If not a weapon (or no weapons to spawn), try for ammo
                {
                    // Spawn a random ammo type from the array
                    itemToInstantiate = _ammoLoot[Random.Range(0, _ammoLoot.Length)];
                }

                if (itemToInstantiate != null)
                {
                    GameObject spawnedItem = Instantiate(itemToInstantiate, randomSpawnPosition, Quaternion.identity);
                    //_spawnedLoot.Add(spawnedItem);
                }
            }
            else
            {
                //Debug.LogWarning($"SpawnManager: Failed to find a non-overlapping position for loot item {i + 1} after {_maxSpawnAttempts} attempts.");
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
