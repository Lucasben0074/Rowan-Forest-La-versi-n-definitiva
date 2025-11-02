using System.Threading;
using UnityEngine;

public class EnemyHealtControler : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float health;
    private HealthBar healthBar;
    public float Health => health;
    //float timer;
    public bool isHit = false;
    void Start()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        health = maxHealth;    
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0f)
        {
            Destroy(gameObject,0.3f);
        }

        //if (isHit)
        //{
        //    timer += Time.deltaTime;
        //    if(timer > 0.5f) isHit = false;
        //}
        


    }
    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag("PlayerBullet")) && (!gameObject.CompareTag("NoDamageable")))
        {              
            health -= other.GetComponent<ProjectileDamage>().StoneDamage;
            healthBar.UpdateHealthBar(health,maxHealth);
            isHit = true;
            //timer = 0;
            
        }
        if (other.gameObject.CompareTag("Sword") && (!gameObject.CompareTag("NoDamageable")))
        {
            Debug.Log("Ataque con espada");
            health -= other.GetComponent<SwordAttack>().SwordDamage;
            healthBar.UpdateHealthBar(health, maxHealth);
        }
    }
        

    }

 

