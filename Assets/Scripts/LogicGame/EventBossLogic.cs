using UnityEngine;
using UnityEngine.Events;

public class EventBossLogic : MonoBehaviour
{
    public UnityEvent OnActivateBoss;

    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("entro a la esfera");
            OnActivateBoss.Invoke();
        }
    }
}
