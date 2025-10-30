using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;

public class BossFinalController : MonoBehaviour, IDamageMaker
{
    public UnityEvent OnBossDeath;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float detectionRange = 30f;

    [Header("Componentes")]
    private NavMeshAgent agent;
    private Transform player;

    [Header("Ataques")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shootRate = 0.15f;
    [SerializeField] private int projectilesPerBurst = 10;
    [SerializeField] private float projectileForce = 25f;
    [SerializeField] private float chaseSpeed = 15f;
    [SerializeField] private float attackRange = 2.5f;
    [SerializeField] private float shadowDamage = 15;

    [Header("Torches")]
    [SerializeField] private Light[] torchLights;
    [SerializeField] private ParticleSystem[] torchFlames;
    [SerializeField] private float damagePerCycle = 50f;
    private bool recentlyDamaged = false;

    [Header("Boss Stats")]
    [SerializeField] private float maxHealth = 200f;
    private float currentHealth;
    private bool isFeared = false;
    private bool isDead = false;

    [Header("Fear")]
    //[SerializeField] private float fearDuration = 3f;
    private Vector3 fearDirection;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        currentHealth = maxHealth;
    }

    private void Start()
    {

        currentHealth = maxHealth;

        if (projectilePrefab == null)
            Debug.LogError("projectilePrefab no asignado en el Inspector");
        if (shootPoint == null)
            Debug.LogError("shootPoint no asignado en el Inspector");
        if (torchLights == null || torchLights.Length == 0)
            Debug.LogError("No hay luces asignadas en torchLights");
        if (torchFlames == null || torchFlames.Length == 0)
            Debug.LogError("No hay particulas asignadas en torchFlames");

        if (projectilePrefab != null && shootPoint != null)
        {
            Debug.Log("Boss configurado correctamente. Iniciando comportamiento...");
            StartCoroutine(BossBehaviourLoop());
        }
    }

    private void Update()
    {
        if (!isDead)
            CheckTorchesState();

        healthSlider.value = currentHealth / maxHealth;

        if(currentHealth <= 0)
        {
            StopAllCoroutines();
            Destroy(gameObject);
            OnBossDeath.Invoke();
        }

    }

    private IEnumerator BossBehaviourLoop()
    {
        while (!isDead)
        {
            yield return StartCoroutine(ShootBurst());
            yield return new WaitForSeconds(0.8f);

            yield return StartCoroutine(ChasePlayer());
            yield return new WaitForSeconds(1.5f);
        }
    }

    private IEnumerator ShootBurst()
    {
        if (isFeared) yield break;
        if (shootPoint == null || projectilePrefab == null) yield break;
        if (Vector3.Distance(transform.position, player.position) > detectionRange) yield break;


        for (int i = 0; i < projectilesPerBurst; i++)
        {
            GameObject proj = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
            if (proj == null) yield break;

            Rigidbody rb = proj.GetComponent<Rigidbody>();
            if (rb == null)
            {
                Debug.LogError("El prefab del proyectil no tiene Rigidbody");
                yield break;
            }

            Vector3 randomDir = (player.position - shootPoint.position).normalized +
                                new Vector3(Random.Range(-0.4f, 0.4f), 0.1f, Random.Range(-0.4f, 0.4f));

            rb.AddForce(randomDir.normalized * projectileForce, ForceMode.Impulse);
            Destroy(proj, 5f);
            yield return new WaitForSeconds(shootRate);
        }
    }

    private IEnumerator ChasePlayer()
    {
        if (isFeared) yield break;
        if (agent == null || player == null) yield break;

        if(Vector3.Distance(transform.position, player.position) > detectionRange) yield break;


        float chaseTime = 8f;
        float elapsed = 0f;

        agent.isStopped = false;
        agent.speed = chaseSpeed;

        while (elapsed < chaseTime && !isFeared)
        {
            agent.SetDestination(player.position);
            elapsed += Time.deltaTime;

            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                Debug.Log("El boss golpeó al jugador");
            }

            yield return null;
        }

        agent.speed = 4f; // vuelve a velocidad base
    }

    private void CheckTorchesState()
    {
        if (recentlyDamaged || isDead) return;

        bool allOn = true;

        foreach (var light in torchLights)
        {
            if (light == null || !light.enabled)
            {
                allOn = false;
                break;
            }
        }

        if (allOn)
        {
            recentlyDamaged = true;
            Debug.Log("Las 4 antorchas están encendidas, el boss recibe 50 de daño");
            TakeDamage(damagePerCycle);
            StartCoroutine(ExtinguishTorches());
        }
    }

    private IEnumerator ExtinguishTorches()
    {
        yield return new WaitForSeconds(0.3f);

        foreach (var light in torchLights)
            if (light != null) light.enabled = false;

        foreach (var flame in torchFlames)
            if (flame != null) flame.Stop();

        Debug.Log("Antorchas apagadas tras hacer daño al boss");

        yield return new WaitForSeconds(1f);
        recentlyDamaged = false;
    }

    private void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        Debug.Log("Boss recibe daño: " + amount + " | Salud: " + currentHealth);

        if (currentHealth <= 0)
        {
            isDead = true;
            agent.isStopped = true;
            Debug.Log("Boss derrotado");
            
        }
    }

    public void ApplyFear(Vector3 fromPosition, float duration)
    {
        if (isDead) return;
        StartCoroutine(FearRoutine(fromPosition, duration));
    }

    private IEnumerator FearRoutine(Vector3 fromPosition, float duration)
    {
        isFeared = true;
        fearDirection = (transform.position - fromPosition).normalized;
        agent.isStopped = false;
        agent.speed = 10f;

        float timer = 0f;
        while (timer < duration)
        {
            agent.SetDestination(transform.position + fearDirection * 10f);
            timer += Time.deltaTime;
            yield return null;
        }

        isFeared = false;
    }

    public float MakeDamage()
    {
        return shadowDamage;
    }
}
