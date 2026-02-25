using System.Collections;
using UnityEngine;

public class hammerController : MonoBehaviour
{
    public GameObject hammer;
    public bool canAttack = true;
    public float attackCooldown = 1.2f;
    public bool isAttacking = false;
    public int colourIndex = 1;
    public float damage = 100.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            HammerAttack();
        }
    }

    public void HammerAttack()
    {
        isAttacking = true;
        canAttack = false;
        Animator anim = hammer.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(1.2f);
        isAttacking = false;
    }
}
