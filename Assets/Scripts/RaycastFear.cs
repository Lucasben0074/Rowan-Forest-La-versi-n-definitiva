using UnityEngine;

public class AreaFear : MonoBehaviour
{
    [SerializeField] private float radius = 10f;       // Radio de la esfera de fear
    [SerializeField] private float fearDuration = 3f;  // Cuánto dura el efecto
    [SerializeField] private KeyCode fearKey = KeyCode.F; // Tecla para activar
    [SerializeField] private LayerMask enemyLayer;     // Solo enemigos

    void Update()
    {
        if (Input.GetKeyDown(fearKey))
        {
            ApplyAreaFear();
        }
    }

    private void ApplyAreaFear()
    {

        Collider[] hits = Physics.OverlapSphere(transform.position, radius, enemyLayer);

        foreach (Collider hit in hits)
        {
            MeleeEnemyAI enemyAI = hit.GetComponent<MeleeEnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.ApplyFear(transform.position, fearDuration);
                Debug.Log("Fear aplicado a: " + hit.name);
            }
        }

        Debug.DrawLine(transform.position, transform.position + Vector3.up * 2, Color.cyan, 1f);
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
