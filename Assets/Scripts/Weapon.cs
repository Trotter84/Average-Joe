using System.Collections;
using UnityEditor;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private LineRenderer bulletLine;
    // [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private PlayerController player;

    [Header("Attributes")]
    private int bulletDamage = 1;
    private float gunRange = 30f;
    [SerializeField] private float bulletDuration = 0.005f;


    void Start()
    {
        bulletLine = GetComponent<LineRenderer>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void Fire()
    {
        bulletLine.SetPosition(0, bulletSpawnPoint.position);

        Vector3 rayOrigin = bulletSpawnPoint.transform.position;
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, -bulletSpawnPoint.transform.forward, out hit, gunRange))
        {
            bulletLine.SetPosition(1, hit.point);

            var hitEnemy = hit.transform.gameObject;
            Debug.Log(hitEnemy);
            var health = hitEnemy.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(bulletDamage);
            }
        }
        else
        {
            bulletLine.SetPosition(1, rayOrigin + (-bulletSpawnPoint.transform.forward * gunRange));
        }
        StartCoroutine(ShootBulletRoutine());
    }

    IEnumerator ShootBulletRoutine()
    {
        bulletLine.enabled = true;
        yield return new WaitForSeconds(bulletDuration);
        bulletLine.enabled = false;
    }
}
