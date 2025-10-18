using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class MeleeEnemyAI : MonoBehaviour
{

    //agregado para el FEAR del amuleto
    private bool isFeared = false;

    [SerializeField]
    private bool isDestroyedOnFear;

    private float fearTimer = 0f;
    private Vector3 fearDirection;

    private NavMeshAgent agent;
    private Transform player;
    private HealthManager playerHealth;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private LayerMask groundLayer, playerLayer;

    private Vector3 walkPoint;
    private float walkPointRange = 15;
    private bool isWalkPointSet;

    private float timeBetweenAttacks = 1;
    private bool alreadyAttacked;

    [SerializeField]
    private float sightRange = 7, attackRange = 1;
    private bool isPlayerInSightRange, isPlayerInAttackRange;

    public bool IsPlayerInAttackRange => isPlayerInAttackRange;

    private void Awake()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<HealthManager>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;

        StartCoroutine(UpdateState());
    }

    private void Update()
    {
        if (isFeared) 
        {
            HandleFear(); 
        }
        else
        {
            CheckState();
        }
    }

    private void CheckState()
    {
        if (!isPlayerInSightRange && !isPlayerInAttackRange)
        {
            Patrolling();
        }
        if (isPlayerInSightRange && !isPlayerInAttackRange) Chasing();
        if (isPlayerInSightRange && isPlayerInAttackRange) Attacking();
    }

    private void Patrolling()
    {
        if (!isWalkPointSet) SearchWalkPoint();

        if (isWalkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 0.1f)
        {
            Invoke(nameof(ResetIsWalkPointSet), 3);
        }
    }

    private void ResetIsWalkPointSet()
    {
        isWalkPointSet = false;
    }

    private void Chasing()
    {
        agent.SetDestination(player.position); 
    }

    private void Attacking()
    {
        agent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            playerHealth.TakeDamage(10);
        
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer))
        {
            isWalkPointSet = true;
        }
    }

    private IEnumerator UpdateState()
    {
        while (true)
        {
            if (!isFeared)
            {
                isPlayerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
                isPlayerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
            }
            

            yield return new WaitForSeconds(0.3f);
        }
    }

    private void HandleFear()
    {
        if (isDestroyedOnFear)
        {
            Destroy(gameObject, 2);
        }

        fearTimer -= Time.deltaTime;     
        agent.SetDestination(transform.position + fearDirection * 10f);

        if (fearTimer <= 0f)
        {
            isFeared = false;
        }
    }

    public void ApplyFear(Vector3 sourcePosition, float duration)
    {
        isFeared = true;
        fearTimer = duration;
        fearDirection = (transform.position - sourcePosition).normalized;
    }
}