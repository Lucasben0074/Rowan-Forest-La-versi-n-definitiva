using UnityEngine;

public class PumpkinsShoot : MonoBehaviour, IDamageMaker
{
    [SerializeField] private float pumpkinsVelocity = 10;
    [SerializeField] private float targetUpY = 10f;
    [SerializeField] private float pumpkinDamage = 135f;

    public float MakeDamage()
    {
        return pumpkinDamage;
    }

    public void MovePumpkins()
    {
        transform.Translate(Vector3.back * pumpkinsVelocity * Time.deltaTime);
    }

    void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
       if(transform.position.y <= targetUpY)
        {
            transform.Translate(Vector3.up * pumpkinsVelocity * Time.deltaTime);
        }
       else
        {
            MovePumpkins();
        }

    }
}
