using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BossLifeControler : MonoBehaviour
{
    private GameObject eventActivator;
    [SerializeField] private GameObject levelKey;
    [SerializeField] private GameObject amuleto;
    [SerializeField] private float maxHealth = 300f;
    private float health;
    [SerializeField] private Slider healthSlider;
    private GameObject bossSlider;
    private Animator animator;
    private Vector3 dropPosition;
    void Start()
    {
        health = maxHealth;
        animator = GetComponentInChildren<Animator>();
        if (healthSlider == null)
        {
            healthSlider = GameObject.Find("FirstBoss").GetComponent<Slider>();
        }
        bossSlider = GameObject.Find("FirstBoss");
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
        if(health <= 0)
        {   
            bossSlider.SetActive(false);    
            Destroy(eventActivator);
            BossDrop();
            animator.SetTrigger("death");
            Destroy(gameObject);
        }

        healthSlider.value = health/maxHealth;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            health -= other.GetComponent<ProjectileDamage>().StoneDamage;
            Debug.Log("Salud de la calabaza: " + health);
        }

    }

}

