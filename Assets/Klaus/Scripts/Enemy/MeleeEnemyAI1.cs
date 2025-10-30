using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class MeleeEnemyAI1 : MonoBehaviour
{


    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip chaseClip;
    [SerializeField] private AudioClip fearClip;


    private bool hasPlayedChaseSound = false; 

    [SerializeField] private LayerMask enviroment;

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

    protected virtual void Update()
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
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 0.1f)
            Invoke(nameof(ResetIsWalkPointSet), 3);


        hasPlayedChaseSound = false; 
    }


    private void ResetIsWalkPointSet()
    {
        isWalkPointSet = false;
    }

    protected virtual void Chasing()
    {
        agent.SetDestination(player.position);

        if (!hasPlayedChaseSound)
        {
            if (chaseClip != null)
                audioSource.PlayOneShot(chaseClip);
            hasPlayedChaseSound = true;
        }
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
                // Detecta si el jugador está en el rango de visión
                bool playerInRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);

                if (playerInRange)
                {
                    // Verifica si hay línea de visión directa
                    Vector3 directionToPlayer = (player.position - transform.position).normalized;
                    float distanceToPlayer = Vector3.Distance(transform.position, player.position);

                    // Si el raycast NO choca con un obstáculo, el jugador está visible
                    if (!Physics.Raycast(transform.position + Vector3.up, directionToPlayer, distanceToPlayer, enviroment))
                    {
                        isPlayerInSightRange = true;
                    }
                    else
                    {
                        isPlayerInSightRange = false; // Hay algo bloqueando la vista
                    }

                    // (Opcional) Dibuja el raycast para debug visual
                    Debug.DrawRay(transform.position + Vector3.up, directionToPlayer * distanceToPlayer, isPlayerInSightRange ? Color.green : Color.red, 0.3f);
                }
                else
                {
                    isPlayerInSightRange = false;
                }

                // El rango de ataque sigue igual
                isPlayerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
            }

            yield return new WaitForSeconds(0.3f); // Actualiza el estado cada 0.3 segundos
        }
    }



    private void HandleFear()
    {
        fearTimer -= Time.deltaTime;
        agent.SetDestination(transform.position + fearDirection * 10f);

        if (fearTimer > 0.1f && !audioSource.isPlaying)
        {
            if (fearClip != null)
                audioSource.PlayOneShot(fearClip);
        }

        if (fearTimer <= 0f)
            isFeared = false;
    }


    public void ApplyFear(Vector3 sourcePosition, float duration)
    {
        isFeared = true;
        fearTimer = duration;
        fearDirection = (transform.position - sourcePosition).normalized;

        if (fearClip != null)
            audioSource.PlayOneShot(fearClip);
    }

}