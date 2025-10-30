using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    [Header("Velocidades")]
    [SerializeField] private float walkSpeed = 10f;
    public float Walk => walkSpeed;
    [SerializeField] private float sprintSpeed = 14f;
    public float Sprint => sprintSpeed;
    [SerializeField] private float crouchSpeed = 1f;
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private float jumpForce = 5f;
    

    [Header("Detección de suelo")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius = 0.1f;

    [Header("Detección de obstáculos frontales")]
    [SerializeField] private float obstacleDetectDistance = 1f; // distancia del raycast
    [SerializeField] private LayerMask obstacleLayer; // capa de colisión de obstáculos

    private Rigidbody rb;
    private Animator anim;
    private bool isGrounded;
    public bool IsGrounded => isGrounded;
    private bool isCrouched;
    private bool isSprinting;
    private float currentSpeed;
    public float CurrentSpeed => currentSpeed;
    private Vector3 moveInput;
    public Vector3 MoveInput => moveInput;

    public bool IsCrouched => isCrouched;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        rb.freezeRotation = true;
        currentSpeed = walkSpeed;
    }

    private void Update()
    {
        HandleInput();
        HandleJump();
        HandleCrouch();
        HandleSprint();
        DetectObstacle(); // detecta obstáculos cada frame
    }

    private void FixedUpdate()
    {
        MoveAndRotate();
    }

    private void HandleInput()
    {
        float moveH = Input.GetAxisRaw("Horizontal");
        float moveV = Input.GetAxisRaw("Vertical");

        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        moveInput = (camForward * moveV + camRight * moveH).normalized;
        anim.SetBool("walking", moveInput.magnitude > 0.1f);
    }

    private void HandleJump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            anim.SetTrigger("jumping");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouched = !isCrouched;
            anim.SetBool("sneaking", isCrouched);
        }
    }

    private void HandleSprint()
    {
        if (!isCrouched && Input.GetKey(KeyCode.LeftShift) && moveInput.magnitude > 0.1f)
        {
            isSprinting = true;
            anim.SetBool("running", true);
        }
        else
        {
            isSprinting = false;
            anim.SetBool("running", false);
        }

        // velocidad base (sin obstáculo)
        currentSpeed = isCrouched ? crouchSpeed : (isSprinting ? sprintSpeed : walkSpeed);
    }

    private void DetectObstacle()
    {
        // Si no te estás moviendo, no hace falta detectar
        if (moveInput.magnitude < 0.1f) return;

        // Raycast al frente del jugador
        Vector3 rayOrigin = transform.position + Vector3.up * 0.5f;
        if (Physics.Raycast(rayOrigin, transform.forward, out RaycastHit hit, obstacleDetectDistance, obstacleLayer))
        {
            //  Hay obstáculo delante reducir velocidad a caminar
            currentSpeed = walkSpeed;
            anim.SetBool("running", false);
        }

        // (Opcional dibujar el rayo en la vista de escena)
        Debug.DrawRay(rayOrigin, transform.forward * obstacleDetectDistance, Color.red);
    }

    private void MoveAndRotate()
    {
        if (moveInput.magnitude > 0.1f)
        {
            Vector3 move = moveInput * currentSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + move);

            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            Quaternion smoothRotation = Quaternion.RotateTowards(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(smoothRotation);
        }
    }
}
