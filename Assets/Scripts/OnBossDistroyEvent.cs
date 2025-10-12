using UnityEngine;

public class OnBossDistroyEvent : MonoBehaviour
{
    [SerializeField] private GameObject levelKey;
    [SerializeField] private GameObject Amuleto;
        
    public void BossDrop()
    {
        Instantiate(levelKey, transform.position, Quaternion.identity);
        Instantiate(Amuleto , transform.position, Quaternion.identity);
    }

}
