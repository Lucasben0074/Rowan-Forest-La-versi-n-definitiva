using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Audio;

public class BossLifeControler : MonoBehaviour
{
    private GameObject eventActivator;

    [SerializeField] private GameObject levelKey;
    [SerializeField] private GameObject amuleto;
    [SerializeField] private float maxHealth = 300f;
    private float health;
    public float Health => health;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private AudioMixerSnapshot calabazamuerta;

    private GameObject bossSlider;
    private Animator animator;
    private Vector3 dropPosition;
    public bool isHit = false;
    private bool isDead = false;    
    void Start()
    {
        health = maxHealth;
        animator = GetComponentInChildren<Animator>();
        if (healthSlider == null)
        {
            healthSlider = GameObject.Find("BossHealthSlider").GetComponent<Slider>();
        }
        bossSlider = GameObject.Find("BossHealthSlider");
        eventActivator = GameObject.Find("BossEventActivation");
        dropPosition = transform.position;
    }

    public void BossDrop()
    {
        GameObject key = Instantiate(levelKey,dropPosition, Quaternion.identity);
        GameObject Newamuleto = Instantiate(amuleto,dropPosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0 && !isDead)
        {   
            isDead = true;
            calabazamuerta.TransitionTo(0.5f);
            bossSlider.SetActive(false);    
            Destroy(eventActivator);
            BossDrop();
            animator.SetTrigger("death");
            Destroy(gameObject,0.6f);
        
        
        }

        healthSlider.value = health/maxHealth;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            health -= other.GetComponent<ProjectileDamage>().StoneDamage;
            Debug.Log("Salud de la calabaza: " + health);
            isHit = true;
        }

    }

}

