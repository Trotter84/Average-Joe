using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public abstract class Weapons : MonoBehaviour
{
    [Header("Component")]
    public Transform bulletSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;
    public List<GameObject> pooledBullets = new List<GameObject>();
    [SerializeField] private PlayerController player;
    [SerializeField] private MouseController mouse;
    [SerializeField] private Transform crosshair;
    [SerializeField] private UiManager uiManager;
    [SerializeField] protected GunItem gunItem;

    private float timeToFire = 0.0f;
    private float gunRange = 30f;
    private float bulletDuration = 0.05f;
    public AudioSource gunSound;
    public Animator muzzleFlash;
    public MeshRenderer muzzleFlashRender;
    private float muzzleFashDuration = 0.05f;
    private bool canShoot;
    public bool isReloading;

    [Header("Unity Events")]
    [SerializeField] private UnityEvent fireGun;
    

    public void Start()
    {
        InitiatePool();

        player = GameObject.Find("Player").GetComponent<PlayerController>();
        if (player == null)
        {
            Debug.LogError("The Player : PlayerController on Weapon is NULL");
        }

        mouse = GameObject.Find("Crosshair").GetComponent<MouseController>();
        if (mouse == null)
        {
            Debug.LogError("The Mouse : MouseController on Weapon is NULL");
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

        gunItem.bulletsLeft = gunItem.magazineSize;
        canShoot = true;
    }

    private void InitiatePool()
    {
        for (int i = 0; i < gunItem.bulletsPerShot; i++)
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

    bool CheckFireRate()
    {
        if (Time.time >= timeToFire)
        {
            
            timeToFire = Time.time + (1.0f / gunItem.fireSpeed);
            return true;
        }
        return false;
    }

    public void Update()
    {
        if (mouse.isShooting)
        {
            if (gunItem.fireRate == GunItem.FireRate.Auto)
            {
                if (CheckFireRate())
                {
                    Fire();
                    muzzleFlashRender.enabled = true;
                }
            }
            else if (gunItem.fireRate == GunItem.FireRate.Single)
            {
                Fire();
                mouse.isShooting = false;
            }
        
        }
    }

    public void Fire()
    {
        if (canShoot)
        {
            for (int i = 0; i < gunItem.bulletsPerShot; i++)
            {
                bulletPrefab = GetPooledBullets();

                if (bulletPrefab != null)
                {
                    Debug.Log(Time.time + (1.0f / gunItem.fireSpeed));
                    gunSound.Play();

                    var currentBullet = bulletPrefab.GetComponent<LineRenderer>();

                    Vector3 rayOrigin = bulletSpawnPoint.position;
                    Vector3 crosshairTarget = crosshair.position;
                    
                    currentBullet.SetPosition(0, rayOrigin);

                    Vector3 offset = new Vector3(rayOrigin.x - crosshairTarget.x, rayOrigin.y - crosshairTarget.y);

                    float x = Random.Range(-gunItem.bulletSpread, gunItem.bulletSpread);
                    float y = Random.Range(-gunItem.bulletSpread, gunItem.bulletSpread);

                    Vector3 offsetWithSpread = offset + new Vector3(x, y);

                    float angle = Mathf.Atan2(offsetWithSpread.y, offsetWithSpread.x) * Mathf.Rad2Deg;
                    
                    transform.eulerAngles = new Vector3(angle, -90f, 90f);

                    RaycastHit hit;
                    if (Physics.Raycast(rayOrigin, bulletSpawnPoint.transform.forward - offsetWithSpread, out hit, gunRange))
                    {
                        currentBullet.SetPosition(1, hit.point);

                        var hitEnemy = hit.transform.gameObject;

                        var health = hitEnemy.GetComponent<Health>();
                        if (health != null)
                        {
                            health.TakeDamage(gunItem.gunDamage);
                        }
                    }
                    else
                    {
                        currentBullet.SetPosition(1, rayOrigin + ((bulletSpawnPoint.transform.forward - offsetWithSpread) * gunRange));
                    }
                    StartCoroutine(ShootBulletRoutine(bulletPrefab));
                    StartCoroutine(MuzzleFlashRoutine());
                }
            }
            gunItem.bulletsLeft --;
        }
    }

    public void Reload()
    {
        if (gunItem.bulletsLeft != gunItem.magazineSize)
        {
            isReloading = true;
            canShoot = false;
            StartCoroutine(ReloadRoutine());
            StartCoroutine(uiManager.ReloadingUIRoutine());
        }
    }

    public abstract void Shoot();

    IEnumerator ShootBulletRoutine(GameObject bulletPrefab)
    {
        bulletPrefab.SetActive(true);
        yield return new WaitForSeconds(bulletDuration);
        bulletPrefab.SetActive(false);
    }

    IEnumerator MuzzleFlashRoutine()
    {
        while (mouse.isShooting)
        {
            muzzleFlashRender.enabled = true;
            yield return new WaitForSeconds(muzzleFashDuration);
            muzzleFlashRender.enabled = false;
        }
    }

    IEnumerator ReloadRoutine()
    {
        yield return new WaitForSeconds(gunItem.reloadTime);
        gunItem.bulletsLeft = gunItem.magazineSize;
        canShoot = true;
        isReloading = false;
    }

}
