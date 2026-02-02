using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] List<Material> enemyMaterials;
    public float spawnInterval = 2.0f;
    public bool isSpawning = true;
    [SerializeField] float spawnerSize = 5.0f;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] int maxAttempts = 20;

    private void Start()
    {
        StartCoroutine(spawnEnemyRoutine());
    }
    
    IEnumerator spawnEnemyRoutine()
    {
        while (isSpawning)
        {
            yield return new WaitForSeconds(spawnInterval);
            Vector3 spawnPosition = getValidSpawnPosition();
            GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], spawnPosition, transform.rotation);
            assignRandomMaterial(enemy);
        }
    }

    private Vector3 getValidSpawnPosition()
    {
        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnerSize;
            spawnPosition.y = transform.position.y;
            bool overlaps = Physics.CheckSphere(spawnPosition, enemyPrefabs[0].GetComponent<CapsuleCollider>().radius, enemyLayer);
            if (!overlaps)
            {
                return spawnPosition;
            }
        }
        Debug.LogWarning("Could not find valid position");
        return transform.position;
    }

    private void assignRandomMaterial(GameObject enemy)
    {
        Renderer rend = enemy.GetComponentInChildren<Renderer>();
        Material randomMat = enemyMaterials[Random.Range(0, enemyMaterials.Count)];
        if (rend != null)
        {
            rend.material = randomMat;
        }
    }
}
