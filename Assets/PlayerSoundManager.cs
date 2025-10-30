using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource _audioSource;

    [Header("Footstep Clips")]
    [SerializeField] private AudioClip[] walkingSteps;
    [SerializeField] private AudioClip[] runningSteps;
    [SerializeField] private AudioClip onAir;
    [SerializeField] private AudioClip onEarth;

    private CharacterMovement movement;
    private float stepTimer;
    private float stepInterval;

    // Control interno para evitar repetir sonidos en el aire
    private bool wasGrounded = true;

    void Start()
    {
        movement = GetComponent<CharacterMovement>();
        stepTimer = 0f;
    }

    void Update()
    {
        Vector3 move = movement.MoveInput;
        float speed = movement.CurrentSpeed;
        bool isGrounded = movement.IsGrounded;

        //  Pasos solo si está en el suelo
        if (move.magnitude > 0.1f && isGrounded)
        {
            stepInterval = Mathf.Lerp(0.4f, 0.18f, Mathf.InverseLerp(movement.Walk, movement.Sprint, speed));
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                PlayFootstep(speed);
                stepTimer = stepInterval + Random.Range(-0.03f, 0.03f); // leve variación natural
            }
        }
        else
        {
            stepTimer = 0f;
        }

        //  Sonidos de salto / aterrizaje
        HandleJumpAndLanding(isGrounded);
    }

    private void HandleJumpAndLanding(bool isGrounded)
    {
        // Si acaba de saltar (estaba en el suelo y ahora no lo está)
        if (wasGrounded && !isGrounded)
        {
            _audioSource.PlayOneShot(onAir);
        }

        // Si acaba de aterrizar (estaba en el aire y ahora toca el suelo)
        if (!wasGrounded && isGrounded)
        {
            _audioSource.PlayOneShot(onEarth);
        }

        wasGrounded = isGrounded; // Actualiza el estado
    }

    private void PlayFootstep(float speed)
    {
        AudioClip clip = (speed < movement.Sprint * 0.9f)
            ? walkingSteps[Random.Range(0, walkingSteps.Length)]
            : runningSteps[Random.Range(0, runningSteps.Length)];

        _audioSource.pitch = Random.Range(0.95f, 1.05f); // da naturalidad
        _audioSource.PlayOneShot(clip);
    }
}
