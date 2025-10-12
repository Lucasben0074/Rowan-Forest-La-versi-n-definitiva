using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform target;

    [Header("Ajustes Normales")]
    [SerializeField] private float distance = 5f;
    [SerializeField] private float height = 2f;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float rotationSmoothTime = 0.12f;

    [Header("Ajustes de Apuntado")]
    [SerializeField] private float aimDistance = 2.5f;
    [SerializeField] private float aimHeight = 1.8f;
    [SerializeField] private float aimSideOffset = 1.2f; 

    private float yaw;
    private float pitch;
    private Vector3 currentRotation;
    private Vector3 smoothVelocity;
    private bool isAiming;

    public Transform Target
    {
        get => target;
        set => target = value;
    }

    public void SetAiming(bool aiming)
    {
        isAiming = aiming;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        if (!target) return;

        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, -20f, 60f);

        Vector3 targetRotation = new Vector3(pitch, yaw);
        currentRotation = Vector3.SmoothDamp(currentRotation, targetRotation, ref smoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;

        // Ajustes de distancia y altura
        float currentDistance = isAiming ? aimDistance : distance;
        float currentHeight = isAiming ? aimHeight : height;

        // offset 
        Vector3 offset = transform.forward * -currentDistance + Vector3.up * currentHeight;

     
        if (isAiming)
            offset += transform.right * aimSideOffset;

        transform.position = target.position + offset;
    }
}

