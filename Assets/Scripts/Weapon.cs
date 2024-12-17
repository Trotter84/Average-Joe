using System.Collections;
using System.Threading;
using UnityEditor;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private LineRenderer bulletLine;
    [SerializeField] private PlayerController player;
    [SerializeField] private Transform crosshair;
    [SerializeField] private UiManager uiManager;

    [Header("Attributes")]
    private bool canShoot;
    private int bulletDamage = 1;
    private float gunRange = 30f;
    [SerializeField] private float bulletDuration = 0.005f;
    [SerializeField] public int magazineSize = 9;
    [SerializeField] public int bulletsLeft;
    [SerializeField] public bool isReloading;
    [SerializeField] public float reloadTime = 3f;
    public float timer;


    void Start()
    {
        bulletLine = GetComponent<LineRenderer>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        if (player == null)
        {
            Debug.LogError("The Player:PlayerController on Weapon is NULL");
        }

        uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
        if (uiManager == null)
        {
            Debug.LogError("The Canvas:UiManager on Weapon is NULL.");
        }

        bulletsLeft = magazineSize;
        canShoot = true;
    }

    void Update()
    {
        if (bulletsLeft == 0)
        {
            canShoot = false;
        }

        timer += Time.deltaTime;
    }

    public void Fire()
    {
        if (canShoot)
        {
            bulletLine.SetPosition(0, bulletSpawnPoint.position);

            Vector3 rayOrigin = bulletSpawnPoint.position;
            Vector3 crosshairTarget = crosshair.position;
            
            Vector2 offset = new Vector2(rayOrigin.x - crosshairTarget.x, rayOrigin.y - crosshairTarget.y);

            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            
            transform.eulerAngles = new Vector3(angle, -90f, 90f);

            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, bulletSpawnPoint.transform.forward, out hit, gunRange))
            {
                bulletLine.SetPosition(1, hit.point);

                var hitEnemy = hit.transform.gameObject;

                var health = hitEnemy.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(bulletDamage);
                }
            }
            else
            {
                bulletLine.SetPosition(1, rayOrigin + (bulletSpawnPoint.transform.forward * gunRange));
            }
            StartCoroutine(ShootBulletRoutine());

            bulletsLeft --;
        }
    }

    public void Reload()
    {
        if (bulletsLeft != magazineSize)
        {
            timer = 0;
            isReloading = true;
            StartCoroutine(ReloadRoutine());
            StartCoroutine(uiManager.ReloadingUIRoutine());
        }
    }

    IEnumerator ShootBulletRoutine()
    {
        bulletLine.enabled = true;
        yield return new WaitForSeconds(bulletDuration);
        bulletLine.enabled = false;
    }

    IEnumerator ReloadRoutine()
    {
        yield return new WaitForSeconds(reloadTime);
        bulletsLeft = magazineSize;
        canShoot = true;
        isReloading = false;
    }
}
