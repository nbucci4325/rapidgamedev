using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Android;

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
                isWaiting= false;
                moveToRandomPoint();
            }
        }
        return State.Patrol;
    }

    public override State Chase()
    {
        agent.ResetPath();
        agent.autoBraking = true;
        throw new System.NotImplementedException();
    }

    public override State Wait()
    {
        throw new System.NotImplementedException();
    }

    public override State Attack()
    {
        throw new System.NotImplementedException();
    }

    private
}
