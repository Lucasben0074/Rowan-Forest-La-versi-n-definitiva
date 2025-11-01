using UnityEngine;

public class HealthController : MonoBehaviour
{
    private PlayerData data;

    private HealthBar healthBar;

    private void Start()
    {
        data = GetComponent<PlayerData>();
        healthBar = GetComponentInChildren<HealthBar>();
    }

    public void TakeDamage(float damage)
    {
        data.Health -= damage;
        healthBar.UpdateHealthBar(data.Health, data.MaxHealth);

        if (data.Health <= 0)
        {
            Destroy(gameObject); 
        }
    }
}
