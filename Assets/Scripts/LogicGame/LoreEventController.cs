using UnityEngine;

public class LoreEventController : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private KeyCode closeKey = KeyCode.E; // tecla para cerrar el panel

    private bool isActive = false;

    public void OnEventLore()
    {
        // Activar panel y pausar tiempo
        panel.SetActive(true);
        Time.timeScale = 0f;
        isActive = true;
        Debug.Log("Panel de lore activado. Tiempo detenido.");
    }

    private void Start()
    {
        panel.SetActive(false);
    }
    private void Update()
    {
        if (isActive && Input.GetKeyDown(closeKey))
        {
            ClosePanel();
        }
    }

    private void ClosePanel()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
        isActive = false;
        Debug.Log("Panel cerrado. Tiempo reanudado.");
    }
}

