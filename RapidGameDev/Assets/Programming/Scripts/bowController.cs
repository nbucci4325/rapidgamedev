using UnityEngine;

public class bowController : MonoBehaviour
{
    public GameObject bow;
    public bool canAttack = true;
    public bool isAttacking = false;
    public float shotCooldown = 0.75f;
    public float damage = 100.0f;
    public float arrowSpeed = 1000.0f;

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
        GameObject arr = Instantiate(arrow, arrowPoint.transform.position, transform.rotation);
        arr.GetComponent<Rigidbody>().AddForce(transform.forward * arrowSpeed);
        Destroy(arr, 1f);
    }
}
