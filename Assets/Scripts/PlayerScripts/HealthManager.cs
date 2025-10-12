using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private float startHealth;
    private float currentHealth;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float healthValue;
    [SerializeField] private TMP_Text Lives;
    private bool isDead = false;
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }
    void Death()
    {
        if(GameManager.Instance.PlayerLives > 0)
        {
            GameManager.Instance.LoseLifes(1);
            Invoke(nameof(ReloadScene), 1.5f);
            isDead = true;
        }
        else
        {
            Debug.Log("Perdiste maldito perdedor.");
        }
       
    }
    private void ReloadScene()
    {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    void Start()
    {
        isDead = false;
        currentHealth = startHealth;
        Lives.text = GameManager.Instance.PlayerLives.ToString();
    }

    void Update()
    {
        if (isDead) return;
        
        if(currentHealth <= 0)
        {          
                Death();      
        }


        healthSlider.value = currentHealth / startHealth; 
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageMaker damageMaker = collision.gameObject.GetComponent<IDamageMaker>();

        if (damageMaker != null)
        {
            float damage = damageMaker.MakeDamage();
            TakeDamage(damage);
            Debug.Log(currentHealth);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        IDamageMaker damageMaker = other.GetComponent<IDamageMaker>();

        if (damageMaker != null)
        {
            float damage = damageMaker.MakeDamage();
            TakeDamage(damage);
            Debug.Log(currentHealth);    
        }
    }
}
