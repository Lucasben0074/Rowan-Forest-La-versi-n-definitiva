using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image healthBar;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main; 
    }
    private void LateUpdate()
    {
        RotateHealthBar();
    }

    public void UpdateHealthBar(float health, float maxHealth)
    {
        healthBar.fillAmount = health / maxHealth;
    }

    private void RotateHealthBar()
    {
        Vector3 directionToCamera = transform.position - cam.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);

        transform.rotation = targetRotation;
    }

}
