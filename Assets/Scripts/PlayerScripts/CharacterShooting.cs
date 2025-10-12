using UnityEngine;

public class CharacterShooting : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject crosshairUI;

    [Header("Ajustes de Apuntado")]
    [SerializeField] private float aimFov = 45f;
    [SerializeField] private float normalFov = 60f;
    [SerializeField] private float aimSmooth = 10f;

    private bool isAiming;
    private ThirdPersonCamera thirdPersonCam;
    private bool canShoot = true;
    [SerializeField] private float shootCooldown = 1.8f;
    private float timer = 0f;
    public bool IsAiming => isAiming;

    private void Start()
    {
        thirdPersonCam = playerCamera.GetComponent<ThirdPersonCamera>();
        if (crosshairUI) crosshairUI.SetActive(false);
    }

    private void Update()
    {
        HandleAim();
        HandleShoot();
        UpdateCameraFov();

        if (!canShoot)
        {
            timer += Time.deltaTime;
            if(timer >= shootCooldown)
            {
                canShoot = true;
                timer = 0f;
            }
        }
    }

    private void HandleAim()
    {
        if (Input.GetMouseButton(1))
        {
            isAiming = true;
            crosshairUI?.SetActive(true);
        }
        else
        {
            isAiming = false;
            crosshairUI?.SetActive(false);
        }

        thirdPersonCam?.SetAiming(isAiming);
    }

    private void HandleShoot()
    {
        if (Input.GetMouseButtonDown(0) && isAiming && canShoot)
        {
            Shoot();
        }
    }

    private void UpdateCameraFov()
    {
        float targetFov = isAiming ? aimFov : normalFov;
        playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, targetFov, Time.deltaTime * aimSmooth);
    }

    private void Shoot()
    {
        canShoot = false;
        

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(100f);

        Vector3 direction = (targetPoint - shootPoint.position).normalized;

        Instantiate(projectilePrefab, shootPoint.position, Quaternion.LookRotation(direction));

        
    }
}
