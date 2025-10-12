using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    [Header("Velocidades")]
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float sprintSpeed = 14f;
    [SerializeField] private float crouchSpeed = 1f;
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private float jumpForce = 5f;

    [Header("Detección de suelo")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius = 0.1f;

    private Rigidbody rb;
    private Animator anim;
    private bool isGrounded;
    private bool isCrouched;
    private float startVelocity;
    private Vector3 moveInput;

    public bool IsCrouched => isCrouched;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        rb.freezeRotation = true;
        startVelocity = movementSpeed;
    }

    private void Update()
    {
        HandleInput();
        HandleJump();
        HandleCrouch();
        HandleSprint();
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
            anim.SetBool("running", true);
            movementSpeed = sprintSpeed;
        }
        else
        {
            anim.SetBool("running", false);
            movementSpeed = isCrouched ? crouchSpeed : startVelocity;
        }
    }

    private void MoveAndRotate()
    {
        if (moveInput.magnitude > 0.1f)
        {
            Vector3 move = moveInput * movementSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + move);

            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            Quaternion smoothRotation = Quaternion.RotateTowards(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(smoothRotation);
        }
    }
}
