using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class EnemyBounceChase_Refined : MonoBehaviour, IDamageMaker
{
    [Header("Velocidades")]
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float runSpeed = 7f;
    [SerializeField] private float turnLerp = 10f;

    [Header("Detección de paredes")]
    [SerializeField] private float probeDistance = 4f;
    [SerializeField] private float avoidDistance = 1.5f;
    [SerializeField] private float rechooseCooldown = 0.15f;

    [Header("Detección del jugador")]
    [SerializeField] private float detectRange = 10f;
    [SerializeField] private float loseRange = 14f;

    [Header("Animación")]
    [SerializeField] private string runBoolName = "run";

    [Header("Sonido")]
    [SerializeField] private AudioClip[] walkClips;
    [SerializeField] private AudioClip[] runClips;
    [SerializeField] private AudioClip attackClip;
    [SerializeField] private float stepIntervalWalk = 0.6f;
    [SerializeField] private float stepIntervalRun = 0.35f;

    private float stepTimer = 0f;
    private AudioSource audioSource;
    private Rigidbody rb;
    private Animator anim;
    private Transform player;

    private bool chasing = false;
    private Vector3 desiredDir;
    private float lastRechooseTime = 0f;
    private float repathTimer = 0f;
    [SerializeField] private float ratDamage = 30f;

    private static readonly Vector3[] dirs =
    {
        Vector3.forward,
        (Vector3.forward + Vector3.right).normalized,
        (Vector3.forward - Vector3.right).normalized,
        Vector3.right,
        -Vector3.right,
        -(Vector3.forward + Vector3.right).normalized,
        -(Vector3.forward - Vector3.right).normalized,
        -Vector3.forward
    };

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;

        anim = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();

        var p = GameObject.FindGameObjectWithTag("Player");
        if (p) player = p.transform;

        ChooseRandomDir();
        SetRun(false);
    }

    void Update()
    {
        if (!player) return;

        float d = Vector3.Distance(transform.position, player.position);

        if (!chasing && d <= detectRange)
        {
            chasing = true;
            SetRun(true);
        }
        else if (chasing && d > loseRange)
        {
            chasing = false;
            SetRun(false);
            ChooseRandomDir();
        }

        HandleFootsteps();
    }

    void FixedUpdate()
    {
        if (chasing && player)
        {
            Vector3 toPlayer = (player.position - transform.position);
            toPlayer.y = 0f;
            MoveAndRotate(toPlayer.normalized, runSpeed);
            return;
        }

        repathTimer += Time.fixedDeltaTime;

        float frontClearance = CastDistance(transform.forward);

        if (frontClearance < avoidDistance || repathTimer > 2f)
        {
            Vector3 best = transform.forward;
            float bestDist = -1f;

            foreach (Vector3 d in dirs)
            {
                Vector3 worldDir = transform.rotation * d;
                worldDir.y = 0;
                float dist = CastDistance(worldDir);
                if (dist > bestDist)
                {
                    bestDist = dist;
                    best = worldDir;
                }
            }

            if (Time.time - lastRechooseTime > rechooseCooldown)
            {
                desiredDir = best;
                lastRechooseTime = Time.time;
                repathTimer = 0f;
            }
        }

        MoveAndRotate(desiredDir, walkSpeed);
    }

    private void MoveAndRotate(Vector3 dir, float speed)
    {
        if (dir == Vector3.zero) return;
        Vector3 delta = dir.normalized * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + delta);

        Quaternion target = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, turnLerp * Time.fixedDeltaTime);
    }

    private float CastDistance(Vector3 worldDir)
    {
        Vector3 start = transform.position + Vector3.up * 0.6f;
        if (Physics.Raycast(start, worldDir, out RaycastHit hit, probeDistance, ~0, QueryTriggerInteraction.Ignore))
            return hit.distance;
        else
            return probeDistance;
    }

    private void ChooseRandomDir()
    {
        Vector2 r = Random.insideUnitCircle.normalized;
        desiredDir = new Vector3(r.x, 0f, r.y);
    }

    private void SetRun(bool v)
    {
        if (anim) anim.SetBool(runBoolName, v);
    }

    private void HandleFootsteps()
    {
        stepTimer -= Time.deltaTime;
        if (stepTimer <= 0f)
        {
            AudioClip clip = null;

            if (chasing && runClips.Length > 0)
            {
                clip = runClips[Random.Range(0, runClips.Length)];
                stepTimer = stepIntervalRun;
            }
            else if (!chasing && walkClips.Length > 0)
            {
                clip = walkClips[Random.Range(0, walkClips.Length)];
                stepTimer = stepIntervalWalk;
            }

            if (clip != null)
                audioSource.PlayOneShot(clip);
        }
    }

    public void PlayAttackSound()
    {
        if (attackClip != null)
            audioSource.PlayOneShot(attackClip);
    }

    public float MakeDamage()
    {
        // Lógica de daño y sonido de ataque
        PlayAttackSound();
        return ratDamage;
    }
}
