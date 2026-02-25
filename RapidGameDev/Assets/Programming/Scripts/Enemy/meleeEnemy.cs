using UnityEngine;

public class meleeEnemy : baseEnemy
{
    private void Start()
    {
        base.Start();
    }

    private void Update()
    {
        base.Update();
    }

    public override State Patrol()
    {
        agent.autoBraking = false;
        isWaiting = false;
        waitTime = 0;
        moveToRandomPoint();
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            isWaiting = true;
        if (isWaiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                waitTimer = 0;
                isWaiting = false;
                moveToRandomPoint();
            }
        }
        if (canSeePlayer()) return State.Chase;
        return State.Patrol;
    }

    public override State Chase()
    {
        agent.ResetPath();
        agent.autoBraking = true;
        agent.SetDestination(player.position);
        if (Vector3.Distance(transform.position, player.position) <= chaseDistance) return State.Attack; //change back to WAIT later
        if (Vector3.Distance(transform.position, player.position) > chaseDistance) return State.Patrol;
        return State.Chase;
    }

    public override State Wait()
    {
        throw new System.NotImplementedException();
        //enemy manager stufff
    }

    public override State Attack()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackDistance)
        {
            isAttacking = true;
            Debug.Log(transform.name + "ATTACK!");
        }

        if (distanceToPlayer > attackDistance && distanceToPlayer <= chaseDistance) return State.Chase;
        return State.Attack;
    }
}
