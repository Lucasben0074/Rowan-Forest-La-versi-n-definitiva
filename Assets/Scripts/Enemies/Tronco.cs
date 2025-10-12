using UnityEngine;

public class Tronco : MonoBehaviour,IDamageMaker
{

    [SerializeField] private float troncoDamage = 80f;
    public float MakeDamage()
    {
        return troncoDamage;
    }

    void Start()
    {
        Destroy(gameObject,7f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
