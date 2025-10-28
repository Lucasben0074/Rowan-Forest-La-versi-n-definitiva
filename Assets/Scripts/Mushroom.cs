using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyBounceChase_Refined : MonoBehaviour,IDamageMaker
{
    [Header("Velocidades")]
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float runSpeed = 7f;
    [SerializeField] private float turnLerp = 10f;

    [Header("Detección de paredes")]
    [SerializeField] private float probeDistance = 4f;      // distancia máxima del raycast (más largo)
    [SerializeField] private float avoidDistance = 1.5f;    // si la pared está más cerca que esto, gira ya
    [SerializeField] private float rechooseCooldown = 0.15f;

    [Header("Detección del jugador")]
    [SerializeField] private float detectRange = 10f;
    [SerializeField] private float loseRange = 14f;

    [Header("Animación")]
    [SerializeField] private string runBoolName = "run";

    [Header("Debug (opcional)")]
    [SerializeField] private bool debugRays = false;

    private Rigidbody rb;
    private Animator anim;
    private Transform player;

    private bool chasing = false;
    private Vector3 desiredDir;
    private float lastRechooseTime = 0f;
    private float repathTimer = 0f;
    [SerializeField] private float ratDamage = 30f;

    // direcciones para chequear obstáculos
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

        // distancia frente al enemigo
        float frontClearance = CastDistance(transform.forward);

        // Si hay algo muy cerca o cada ciertos segundos, recalcula dirección
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
        {
            if (debugRays) Debug.DrawLine(start, hit.point, Color.red);
            return hit.distance;
        }
        else
        {
            if (debugRays) Debug.DrawLine(start, start + worldDir * probeDistance, Color.green);
            return probeDistance;
        }
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position + Vector3.up * 0.6f, transform.position + transform.forward * probeDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, loseRange);
    }

    public float MakeDamage()
    {
        return ratDamage;
    }
}
