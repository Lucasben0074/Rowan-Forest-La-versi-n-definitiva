using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (mainCamera == null) return;

        // El cartel mira siempre hacia la c�mara
        transform.LookAt(transform.position + mainCamera.transform.forward);
    }
}
