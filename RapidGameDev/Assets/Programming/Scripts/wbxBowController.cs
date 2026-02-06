using UnityEngine;
using System.Collections;

public class wbxBowController : MonoBehaviour
{
    public GameObject bow;
    public bool canAttack = true;
    public float attackCooldown = 0.9f;
    public bool isAttacking = false;
    public int colourIndex = 0;
    public float damage = 100.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            BowAttack();
        }
    }

    public void BowAttack()
    {
        isAttacking = true;
        canAttack = false;
        Animator anim = bow.GetComponent<Animator>();
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
        yield return new WaitForSeconds(0.9f);
        isAttacking = false;
    }
}
