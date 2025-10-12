using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
     [SerializeField] private float stoneDamage = 10;
    public float StoneDamage => stoneDamage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Destroy(gameObject);
    //}
}
