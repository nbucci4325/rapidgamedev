using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPrefabs;

    public float spawnInterval = 2.0f;
    public bool isSpawning = true;

    private void Start()
    {
        StartCoroutine(spawnEnemyRoutine());
    }

    IEnumerator spawnEnemyRoutine()
    {
        while (isSpawning)
        {
            yield return new WaitForSeconds(spawnInterval);
            Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], transform.position * Random.Range(0, 10), transform.rotation);
        }
    }
}
