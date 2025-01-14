using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Enemy : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private Waypoint waypoint;

    [Header("Attributes")]
    [SerializeField] [Range(0.1f, 5.0f)] private float speed = 2.0f;
    [SerializeField] [Range(1, 10)] private int damage = 1;

    private float collisionTimer = 0;
    private float timeBetweenTicks = 1.5f;


    void Start()
    {
        waypoint = GameObject.Find("Waypoint").GetComponent<Waypoint>();
        if (waypoint == null)
        {
            Debug.LogError("Waypoint : Transform on Canvas is NULL.");
        }
    }

    void Update()
    {
        EnemyMovement();
    }

    void EnemyMovement()
    {
        Vector3 enemyPosition = transform.position;

        Vector2 offset = new Vector2(waypoint.waypointPosition.x - enemyPosition.x, waypoint.waypointPosition.y - enemyPosition.y);

        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        
        transform.eulerAngles = new Vector3(angle, -90f, 90f);
        
        transform.position = Vector3.MoveTowards(enemyPosition, waypoint.waypointPosition, speed * Time.deltaTime);
    }

    void OnCollisionStay(Collision other)
    {
        if (other.transform.CompareTag("Player"))
        {
            collisionTimer += Time.deltaTime;

            if (collisionTimer >= timeBetweenTicks)
            {
                var health = other.transform.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                              
                    collisionTimer = 0;
                }
            }
        }
    }

}
