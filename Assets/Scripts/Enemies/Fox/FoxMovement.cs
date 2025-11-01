using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FoxMovement : MonoBehaviour
{
    
    private Transform playerPosition;

    [Header("Ajustes de movimiento")]
    [SerializeField] private float foxSpeed = 5f;
    [SerializeField] private float rotationSpeed = 8f;
    [SerializeField] private float maxDistance = 10f;

    [Header("Ataque")]
    [SerializeField] private float attackRange = 2f;


    private Vector3 foxStartPosition;
    private Rigidbody rb;
    private Animator anim;
    private bool canAttack;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; 
    }

    private void Start()
    {
        playerPosition = FindAnyObjectByType<CharacterMovement>().transform;
        anim = GetComponentInChildren<Animator>();    
        foxStartPosition = transform.position;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(playerPosition.position, transform.position);
        float distanceToStart = Vector3.Distance(transform.position, foxStartPosition);

        
        if (canAttack)
        {
            anim.SetBool("isMoving", false);
            return;
        }

        
        if (distanceToPlayer <= maxDistance || distanceToStart > 0.1f)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
           
            anim.SetBool("isMoving", false);
        }
    }

    private void FixedUpdate()
    {

        float distanceToPlayer = Vector3.Distance(playerPosition.position, transform.position);
        float distanceToStart = Vector3.Distance(transform.position, foxStartPosition);

        Vector3 targetPosition;

        if (distanceToPlayer <= maxDistance)
        {
            targetPosition = playerPosition.position;
        }
        else
        {
            if (distanceToStart > 0.1f)
                targetPosition = foxStartPosition;
            else
                return; 
        }


        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0f; 

        Vector3 newPosition = rb.position + direction * foxSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);


        if (direction.magnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            Quaternion smoothedRotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(smoothedRotation);
        }

        canAttack = distanceToPlayer <= attackRange;
        gameObject.GetComponent<FoxAttack>().CanAttack = canAttack;
    }
}
