using System.Collections;
using UnityEngine;

public class swordController : MonoBehaviour
{
    public GameObject sword;
    public bool canAttack = true;
    public float attackCooldown = 0.6f;
    public bool isAttacking = false;
    public int colourIndex = 2;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            SwordAttack();
        }
    }

    public void SwordAttack()
    {
        isAttacking = true;
        canAttack = false;
        Animator anim = sword.GetComponent<Animator>();
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
        yield return new WaitForSeconds(0.6f);
        isAttacking = false;
    }
}
