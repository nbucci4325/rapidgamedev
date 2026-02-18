using System.Collections;
using UnityEngine;

public class bowController : MonoBehaviour
{
    public GameObject bow;
    public bool canAttack = true;
    public bool isAttacking = false;
    public float shotCooldown = 0.75f;
    public float damage = 100.0f;
    public float arrowSpeed = 5000.0f;

    [SerializeField]
    public GameObject arrow;
    [SerializeField]
    public GameObject arrowPoint;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
            BowAttack();
    }

    public void BowAttack()
    {
        isAttacking = true;
        canAttack = false;
        Invoke("Shoot", 0.75f);
        Invoke("ResetAttack", 0.75f);
    }

    public void Shoot()
    {
        GameObject arr = Instantiate(arrow, arrowPoint.transform.position, transform.rotation);
        arr.GetComponent<Rigidbody>().AddForce(transform.up * arrowSpeed);
        Destroy(arr, 1f);
    }

    public void ResetAttack()
    {
        isAttacking = false;
        canAttack = true;
    }
}
