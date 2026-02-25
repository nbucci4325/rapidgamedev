using UnityEngine;

public class projectile : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] int speed;
    [SerializeField] int damage;

    public void shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
