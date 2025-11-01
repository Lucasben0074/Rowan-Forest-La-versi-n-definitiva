using UnityEngine;
using UnityEngine.Events;

public class EventBossLogic : MonoBehaviour
{
    public UnityEvent OnActivateBoss;
    private bool hasActivated = false; 

    private void OnTriggerEnter(Collider other)
    {
        if (!hasActivated && other.CompareTag("Player"))
        {
            Debug.Log("Entró a la esfera del evento del jefe");

            hasActivated = true; 
            OnActivateBoss.Invoke();
            

            
            Collider col = GetComponent<Collider>();
            if (col != null)
                col.enabled = false;
        }
    }
}
