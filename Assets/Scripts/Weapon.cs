using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Burst.Intrinsics;
using UnityEditor;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    [Header("Components")]
    public Transform bulletSpawnPoint;
    [SerializeField] private Bullet bulletScript;
    [SerializeField] private GameObject bulletPrefab;
    public List<GameObject> pooledBullets = new List<GameObject>();
    [SerializeField] private PlayerController player;
    [SerializeField] private Transform crosshair;
    [SerializeField] private UiManager uiManager;

    [Header("Attributes")]
    [SerializeField] private int gunID; // 0: Pistol; 1: Assualt; 2: Shotgun;
    [SerializeField] private bool canShoot;
    public float bulletDamage;
    [SerializeField] private int bulletsPerFire;
    private float gunRange = 30f;
    private float bulletDuration = 0.05f;
    private float muzzleFashDuration = 0.1f;
    public int magazineSize;
    public GameObject muzzleFlash;
    public AudioSource gunSound;
    public int bulletsLeft;
    public bool isReloading;
    public float reloadTime;
    public float spread;


    void Start()
    {
        InitiatePool();

        player = GameObject.Find("Player").GetComponent<PlayerController>();
        if (player == null)
        {
            Debug.LogError("The Player : PlayerController on Weapon is NULL");
        }

        crosshair = GameObject.Find("Crosshair").GetComponent<Transform>();
        if (crosshair == null)
        {
            Debug.LogError("Crosshair : Transform on Weapon is NULL.");
        }

        uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
        if (uiManager == null)
        {
            Debug.LogError("The Canvas : UiManager on Weapon is NULL.");
        }

        bulletsLeft = magazineSize;
        canShoot = true;
    }

    private void InitiatePool()
    {
        for (int i = 0; i < bulletsPerFire; i++)
        {
            GameObject bullets = Instantiate(bulletPrefab);
            bullets.transform.parent = bulletSpawnPoint.transform;
            bullets.SetActive(false);
            pooledBullets.Add(bullets);
        }
    }

    public GameObject GetPooledBullets()
    {
        for (int i = 0; i < pooledBullets.Count; i++)
        {
            if (!pooledBullets[i].activeInHierarchy)
            {
                return pooledBullets[i];
            }
        }
        return null;
    }

    void Update()
    {
        if (bulletsLeft == 0)
        {
            canShoot = false;
        }
    }

    public void ChangeAudioSource()
    {
        gunSound = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
    }

    public void Fire()
    {
        if (canShoot)
        {
            for (int i = 0; i < bulletsPerFire; i++)
            {
                bulletPrefab = GetPooledBullets();

                if (bulletPrefab != null)
                {
                    var currentBullet = bulletPrefab.GetComponent<LineRenderer>();

                    Vector3 rayOrigin = bulletSpawnPoint.position;
                    Vector3 crosshairTarget = crosshair.position;
                    
                    currentBullet.SetPosition(0, rayOrigin);

                    Vector3 offset = new Vector3(rayOrigin.x - crosshairTarget.x, rayOrigin.y - crosshairTarget.y);

                    float x = Random.Range(-spread, spread);
                    float y = Random.Range(-spread, spread);

                    Vector3 offsetWithSpread = offset + new Vector3(x, y);

                    float angle = Mathf.Atan2(offsetWithSpread.y, offsetWithSpread.x) * Mathf.Rad2Deg;
                    
                    transform.eulerAngles = new Vector3(angle, -90f, 90f);

                    // Debug.LogError($"RayOrigin: {i} {rayOrigin}");
                    // Debug.LogError($"CrosshairTarget: {i} {crosshairTarget}");
                    // Debug.LogError($"Offset: {i} {offset}");
                    // Debug.LogError($"OffsetWithSpread: {i} {offsetWithSpread}");

                    RaycastHit hit;
                    if (Physics.Raycast(rayOrigin, bulletSpawnPoint.transform.forward - offsetWithSpread, out hit, gunRange))
                    {
                        currentBullet.SetPosition(1, hit.point);

                        var hitEnemy = hit.transform.gameObject;

                        var health = hitEnemy.GetComponent<Health>();
                        if (health != null)
                        {
                            health.TakeDamage(bulletDamage);
                        }
                    }
                    else
                    {
                        currentBullet.SetPosition(1, rayOrigin + ((bulletSpawnPoint.transform.forward - offsetWithSpread) * gunRange));
                    }

                    StartCoroutine(MuzzleFlashRoutine());
                    StartCoroutine(ShootBulletRoutine(bulletPrefab));

                    gunSound.Play();
                }
            }

            bulletsLeft --;

        }
    }

    public void Reload()
    {
        if (bulletsLeft != magazineSize)
        {
            isReloading = true;
            canShoot = false;
            StartCoroutine(ReloadRoutine());
            StartCoroutine(uiManager.ReloadingUIRoutine());
        }
    }

    IEnumerator ShootBulletRoutine(GameObject bulletPrefab)
    {
        bulletPrefab.SetActive(true);
        yield return new WaitForSeconds(bulletDuration);
        bulletPrefab.SetActive(false);
    }

    IEnumerator MuzzleFlashRoutine()
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(muzzleFashDuration);
        muzzleFlash.SetActive(false);
    }

    IEnumerator ReloadRoutine()
    {
        yield return new WaitForSeconds(reloadTime);
        bulletsLeft = magazineSize;
        canShoot = true;
        isReloading = false;
    }
}
