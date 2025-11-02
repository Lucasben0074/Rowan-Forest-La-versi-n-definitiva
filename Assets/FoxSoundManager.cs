using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FoxSoundManager : MonoBehaviour
{
    [Header("Fuentes de audio")]
    [SerializeField] private AudioSource mainSource;
    [SerializeField] private AudioSource footstepSource;

    [Header("Clips de sonido")]
    [SerializeField] private AudioClip[] footsteps;
    [SerializeField] private AudioClip attackClip;
    [SerializeField] private AudioClip hurtClip;
    [SerializeField] private AudioClip deathClip;

    [Header("Tiempos de paso")]
    [SerializeField] private float stepInterval = 0.5f; // cada cuánto suena un paso
    private float stepTimer;

    private Animator anim;
    private Rigidbody rb;

    private bool hasPlayedDeath;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();

        //if (mainSource == null)
        //    mainSource = GetComponent<AudioSource>();

        //if (footstepSource == null)
        //    footstepSource = mainSource;
    }

    private void Update()
    {
        bool isMoving = anim.GetBool("isMoving");

        if (isMoving && rb.linearVelocity.magnitude > 0f)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                PlayFootstep();
                stepTimer = stepInterval;
            }
        }

        if (GetComponent<EnemyHealtControler>().isHit && GetComponent<EnemyHealtControler>().Health >= 10f )
        {
            PlayHurt();
            GetComponent<EnemyHealtControler>().isHit = false;
        }

        if(GetComponent<EnemyHealtControler>().Health <= 0 && !hasPlayedDeath)
        {
            PlayDeath();
            hasPlayedDeath = true;
        }

        
    }

    // === MÉTODOS DE SONIDO ===
    private void PlayFootstep()
    {
        if (footsteps.Length == 0) return;
        int index = Random.Range(0, footsteps.Length);
        footstepSource.PlayOneShot(footsteps[index]);
    }

    public void PlayAttack()
    {
        Debug.Log(" Reproduciendo ataque");
        if (attackClip != null)
            mainSource.PlayOneShot(attackClip);
        else
            Debug.LogWarning(" No hay AttackClip asignado");
    }

    public void PlayHurt()
    {
        if (hurtClip != null)
            mainSource.PlayOneShot(hurtClip);
    }

    public void PlayDeath()
    {
        if (deathClip != null)
            mainSource.PlayOneShot(deathClip);
    }
}

