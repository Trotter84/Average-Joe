using UnityEngine;


public class Enemy : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private GameObject player;

    [Header("Attributes")]
    [SerializeField] [Range(0.1f, 5f)] private float speed = 2.0f;
    [SerializeField] [Range(1, 10)] private int damage = 1;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        EnemyMovement();
    }

    void EnemyMovement()
    {
        Vector3 playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);

        Vector3 enemyPosition = transform.position;

        transform.position = Vector3.MoveTowards(enemyPosition, playerPosition, speed * Time.deltaTime);
    }
}