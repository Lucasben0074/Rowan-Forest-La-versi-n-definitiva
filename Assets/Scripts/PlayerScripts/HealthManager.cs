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
    private Animator animator;
    [SerializeField] private AudioSource HealthAudioSource;

    [SerializeField] AudioClip hurt, death, powerUp;



    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }
    void Death()
    {
        if(GameManager.Instance.PlayerLives > 0)
        {
            GameManager.Instance.LoseLife(1);
            Invoke(nameof(ReloadScene), 4f);
            isDead = true;
            
        }
        else
        {
            SceneManager.LoadScene("GameOver");
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
        animator = GetComponentInChildren<Animator>();    
    }

    void Update()
    {
        if (isDead) return;
        
        if(currentHealth <= 0 || gameObject.GetComponent<LabenrynthTimer>().TimeOver)
        {
            HealthAudioSource.PlayOneShot(death);
            animator.SetTrigger("death");
            Death();      
        }


        healthSlider.value = currentHealth / startHealth; 
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageMaker damageMaker = collision.gameObject.GetComponent<IDamageMaker>();

        if (damageMaker != null)
        {

            HealthAudioSource.PlayOneShot(hurt);
            float damage = damageMaker.MakeDamage();
            TakeDamage(damage);
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        IDamageMaker damageMaker = other.GetComponent<IDamageMaker>();

        if (damageMaker != null)
        {
            HealthAudioSource.PlayOneShot(hurt);
            float damage = damageMaker.MakeDamage();
            TakeDamage(damage);
              
        }

        if (other.gameObject.CompareTag("LifePowerUp"))
        {
            HealthAudioSource.PlayOneShot(powerUp);
            currentHealth = startHealth;
            Destroy(other.gameObject);
        }

    }
}
