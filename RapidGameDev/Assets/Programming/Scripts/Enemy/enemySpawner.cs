using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] List<Material> enemyMaterials;
    [SerializeField] public float spawnInterval = 2.0f;
    private bool isSpawning = true;
    public float spawnerSize = 5.0f;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] int maxAttempts = 20;
    [SerializeField] int maxEnemies = 25;
    [SerializeField] objectPool ObjectPool;
    public Color color = Color.red;

    private void Start()
    {
        StartCoroutine(spawnEnemyRoutine());
    }

    IEnumerator spawnEnemyRoutine()
    {
        while (isSpawning && maxEnemies > 0)
        {
            yield return new WaitForSeconds(spawnInterval);
            int rand = Random.Range(0, enemyPrefabs.Count);
            Vector3 spawnPosition = getValidSpawnPosition(rand);
            ObjectPool.prefab = enemyPrefabs[rand];
            GameObject enemy = ObjectPool.getObject();
            enemy.transform.position = spawnPosition;
            enemy.transform.rotation = transform.rotation;
            assignRandomMaterial(enemy);
            maxEnemies--;
        }
    }

    private Vector3 getValidSpawnPosition(int rand)
    {
        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnerSize;
            spawnPosition.y = transform.position.y;
            if (rand == 0) spawnPosition.y += 10.0f;
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
            rend.material = randomMat;

        Transform tagChild = enemy.transform.GetChild(0);

        string matName = randomMat.name.ToLower();

        if (matName.Contains("cyan"))
            tagChild.gameObject.tag = "Cyan";
        else if (matName.Contains("yellow"))
            tagChild.gameObject.tag = "Yellow";
        else if (matName.Contains("magenta"))
            tagChild.gameObject.tag = "Magenta";
    }
}
