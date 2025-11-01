using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AreaFear : MonoBehaviour
{
    [SerializeField] private TMP_Text timeVisionCooldown;
    [SerializeField] private GameObject amuletVisualCooldown;
    [SerializeField] private float fearCooldown = 10f;
    private float timer = 0f;
    private bool canFear = true;
    public bool Canfear => canFear;
    [SerializeField] private float manaCost = 20;
    [SerializeField] private float radius = 30f;       // Radio de la esfera de fear
    [SerializeField] private float fearDuration = 3f;  // Cuánto dura el efecto
    [SerializeField] private KeyCode fearKey = KeyCode.F; // Tecla para activar
    [SerializeField] private LayerMask enemyLayer;     // Solo enemigos
    private Animator animator;
    private ManaManager manaManager;
    private void Start()
    {
        amuletVisualCooldown.SetActive(false);
        manaManager = gameObject.GetComponent<ManaManager>();
        animator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(fearKey) && canFear)
        {
            manaManager.CurrentMana -= manaCost;
            animator.SetTrigger("fear");
            ApplyAreaFear();
            canFear = false;
            GetComponent<AttackSoundManager>()?.PlayMagicSound();
        }

        if (!canFear)
        {
            amuletVisualCooldown.SetActive(true);
            timer += Time.deltaTime;
            if (timer <= 10f)
            {
                timeVisionCooldown.text = Mathf.Ceil(fearCooldown - timer).ToString("0");
            }
            else
            {
                timer = 0;
                canFear = true;
                amuletVisualCooldown.SetActive(false);
            }
        }

    }

    private void ApplyAreaFear()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);
        Debug.Log("Total colliders detectados: " + hits.Length);

        foreach (Collider hit in hits)
        {
            Debug.Log(" Collider: " + hit.name + " | Layer: " + LayerMask.LayerToName(hit.gameObject.layer));

            BossFinalController boss = hit.GetComponent<BossFinalController>();
            if (boss != null)
            {
                Debug.Log(" Boss detectado, aplicando Fear");
                boss.ApplyFear(transform.position, fearDuration);
            }

            MeleeEnemyAI1 enemyAI = hit.GetComponent<MeleeEnemyAI1>();
            if (enemyAI != null)
            {
                Debug.Log(" Enemigo detectado, aplicando Fear");
                enemyAI.ApplyFear(transform.position, fearDuration);
            }
        }
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}




//using UnityEngine;

//public class RaycastFear : MonoBehaviour
//{
//    [SerializeField] private float range = 20f;       
//    [SerializeField] private float fearDuration = 3f; 
//    [SerializeField] private KeyCode fearKey = KeyCode.R; 

//    void Update()
//    {
//        if (Input.GetKeyDown(fearKey))
//        {
//            ShootFearRay();
//        }
//    }

//    private void ShootFearRay()
//    {
//        // Genera un rayo desde la posición del Player hacia adelante
//        Ray ray = new Ray(transform.position, transform.forward);
//        RaycastHit hit;

//        // Dibujo en escena para debug (se ve en SceneView)
//        Debug.DrawRay(transform.position, transform.forward * range, Color.cyan, 1f);

//        if (Physics.Raycast(ray, out hit, range))
//        {
//            // ¿El objeto golpeado tiene el script MeleeEnemyAI?
//            MeleeEnemyAI enemyAI = hit.collider.GetComponent<MeleeEnemyAI>();
//            if (enemyAI != null)
//            {
//                // Le aplico el fear
//                enemyAI.ApplyFear(transform.position, fearDuration);
//                Debug.Log("Fear aplicado a: " + hit.collider.name);
//            }
//        }
//    }
//}
