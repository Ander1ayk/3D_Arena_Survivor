using UnityEngine;

public class AutoShoot : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float fireRate = 1f;
    [Header("References")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    private PlayerStats playerStats;
    private float nextFireTime;
    private Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
        playerStats = GetComponent<PlayerStats>();
    }
    private void Update()
    {
        if (Time.time >= nextFireTime)
        {
            HandleShooting();
        }
    }
    private void HandleShooting()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            Vector3 targetPoint = hit.point;

            targetPoint.y = firePoint.position.y;

            Vector3 direction = (targetPoint - firePoint.position).normalized;
            Shoot(direction);
            int fireRateFull = Mathf.RoundToInt(fireRate * playerStats.GetFireRateMultiplier());
            nextFireTime = Time.time + 1f / fireRateFull;
        }
    }
    private void Shoot(Vector3 shootDirection)
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        bulletGO.transform.forward = shootDirection;
    }
}
