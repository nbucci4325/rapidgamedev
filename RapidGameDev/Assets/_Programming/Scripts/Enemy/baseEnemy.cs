using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.AI;

public abstract class baseEnemy : MonoBehaviour
{
    private float health = 100.0f;
    private string colourName;
    _zoneManager zone;

    #region Setters
    /// <summary>
    /// Subtracts incoming attack damage from current health
    /// </summary>
    /// <param name="amount">The amount to be subtracted</param>
    public void takeDamage(float amount)
    {
        health -= amount;
        if (health <= 0.0f)
        {
            gameObject.SetActive(false);
            zone.decrementQuota();
            Debug.Log("Enemy Destroyed! Quota Decremented!");
        }
    }

    /// <summary>
    /// Sets the colour of the enemy as a string
    /// </summary>
    /// <param name="colour">The colour, as a string, to be set</param>
    public void SetColour(string colour)
    {
        colourName = colour;
    }
    #endregion

    #region States
    public enum State
    {
        Patrol,
        Chase,
        Wait,
        Attack,
    }

    public State currentState = State.Patrol;
    public Transform player;
    public float moveSpeed;
    public NavMeshAgent agent;
    public float chaseDistance = 10f;
    public float patrolRadius = 20f;
    public float attackDistance = 5f;
    public float waitTime = 2f;
    public float waitTimer;
    public bool isWaiting;
    public bool isAttacking = false;

    public abstract State Patrol();
    public abstract State Chase();
    public abstract State Wait();
    public abstract State Attack();
    protected void moveToRandomPoint()
    {
        Vector3 randomDir = Random.insideUnitSphere * patrolRadius;
        randomDir += agent.transform.position;

        if (NavMesh.SamplePosition(randomDir, out NavMeshHit hit, patrolRadius, 1))
            agent.SetDestination(hit.position);
    }
    #endregion

    #region Collision Logic

    /// <summary>
    /// Describes how to be behave based on incoming attack, taking into account colour
    /// </summary>
    /// <param name="other">The attacking collider</param>
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger fired! Collided with: " + other.name);

        Transform weaponsParent = other.transform.root.Find("PlayerObj").Find("WeaponHolder");
        if (weaponsParent == null)
        {
            Debug.Log("WeaponsHolder not found!");
            return;
        }

        swordController sword = weaponsParent.GetComponentInChildren<swordController>(true);
        hammerController hammer = weaponsParent.GetComponentInChildren<hammerController>(true);
        wbxBowController bow = weaponsParent.GetComponentInChildren<wbxBowController>(true);

        MonoBehaviour activeWeapon = null;

        if (sword != null && sword.gameObject.activeInHierarchy)
            activeWeapon = sword;
        else if (hammer != null && hammer.gameObject.activeInHierarchy)
            activeWeapon = hammer;
        else if (bow != null && bow.gameObject.activeInHierarchy)
            activeWeapon = bow;

        if (activeWeapon == null)
        {
            Debug.Log("No active weapon detected!");
            return;
        }

        Debug.Log("Active weapon detected: " + activeWeapon.name);

        string weaponTag = activeWeapon.gameObject.tag;
        Transform tagChild = transform.GetChild(0);
        string enemyTag = tagChild.tag;

        Debug.Log("Enemy tag: " + enemyTag);
        Debug.Log("Weapon tag: " + weaponTag);

        if (weaponTag == enemyTag)
        {
            Debug.Log("HIT!");

            float weaponDamage = 0f;
            if (activeWeapon is swordController s) weaponDamage = s.damage;
            else if (activeWeapon is hammerController h) weaponDamage = h.damage;
            else if (activeWeapon is wbxBowController b) weaponDamage = b.damage;

            takeDamage(weaponDamage);

        }
    }
    #endregion

    #region FOV
    public GameObject playerRef;
    public LayerMask obstructionMask;

    public bool canSeePlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, distanceToPlayer, obstructionMask))
        {
            Debug.DrawRay(transform.position, directionToPlayer * hit.distance, Color.blue);
            return false;
        }
        else
        {
            Debug.DrawRay(transform.position, directionToPlayer * distanceToPlayer, Color.green);
            return true;
        }
    }
    #endregion

    #region Runtime
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerRef = GameObject.Find("Player");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        zone = GameObject.FindGameObjectWithTag("Zone").GetComponent<_zoneManager>();
    }

    protected virtual void Update()
    {
        State nextState = currentState;

        switch (currentState)
        {
            case State.Patrol:
                nextState = Patrol();
                break;
            case State.Chase:
                nextState = Chase();
                break;
            case State.Wait:
                nextState = Wait();
                break;
            case State.Attack:
                nextState = Attack();
                break;
        }
        if (nextState != currentState) currentState = nextState;
    }
    #endregion
}