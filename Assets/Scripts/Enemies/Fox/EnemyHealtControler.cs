using UnityEngine;

public class EnemyHealtControler : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float health;
    private HealthBar healthBar;
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
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag("PlayerBullet")) && (!gameObject.CompareTag("NoDamageable")))
        {              
            health -= other.GetComponent<ProjectileDamage>().StoneDamage;
            healthBar.UpdateHealthBar(health,maxHealth);
             
        }
        if (other.gameObject.CompareTag("Sword") && (!gameObject.CompareTag("NoDamageable")))
        {
            Debug.Log("Ataque con espada");
            health -= other.GetComponent<SwordAttack>().SwordDamage;
            healthBar.UpdateHealthBar(health, maxHealth);
        }
    }
        

    }

 

