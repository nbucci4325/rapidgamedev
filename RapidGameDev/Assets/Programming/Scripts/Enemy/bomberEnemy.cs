using UnityEngine;

public class bomberEnemy : baseEnemy
{
    [SerializeField] GameObject[] waypoints;
    private int currentPatrolIndex = 0;
    private projectile projectile;

    private new void Start()
    {
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        playerRef = GameObject.Find("Player");
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private new void Update()
    {
        base.Update();
    }

    public override State Patrol()
    {
        Transform target = waypoints[currentPatrolIndex].transform;
        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
        if (Vector3.Distance(transform.position, target.position) < 1f) currentPatrolIndex = Random.Range(0, waypoints.Length);
        if (canSeePlayer()) return State.Chase;
        return State.Patrol;
    }

    public override State Chase()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
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
            Debug.Log("Attack!");
            //projectile.shoot();
        }
        if (distanceToPlayer > attackDistance && distanceToPlayer <= chaseDistance) return State.Chase;
        return State.Attack;
    }

}