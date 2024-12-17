using System.Linq;
using System.Threading;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody rb;
    private Camera mainCamera;
    [SerializeField] public Transform crosshair;
    [SerializeField] private Weapon weapon;

    [Header("Attributes")]
    [SerializeField] private float speed = 5.0f;
    private Vector2 moveDiagonal;

    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        Cursor.visible = false;
    }

    void Update()
    {
        ScreenRestraints();

        Inputs();

        timer -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        PlayerMovement();
        MouseMovement();
    }

    void Inputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDiagonal = new Vector2(moveX, moveY).normalized;

        if (Input.GetMouseButtonDown(0))
        {
            weapon.Fire();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (0 > timer)
            {
                Debug.LogWarning(timer);

                weapon.Reload();

                timer = weapon.reloadTime;
            }
        }
    }

    void PlayerMovement()
    {
        rb.linearVelocity = new Vector2(moveDiagonal.x * speed, moveDiagonal.y * speed);
    }

    void MouseMovement()
    {
        Vector3 mouse = Input.mousePosition;

        Vector3 mousePoint = mainCamera.ScreenToWorldPoint(mouse);
        mousePoint.z = -2f;
        crosshair.position = mousePoint;

        Vector3 playerPosition = mainCamera.WorldToScreenPoint(transform.localPosition);
        playerPosition.z = -0.1f;
        

        Vector2 offset = new Vector2(playerPosition.x - mouse.x, playerPosition.y - mouse.y);

        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        
        transform.eulerAngles = new Vector3(angle, -90f, 90f);
    }

    void ScreenRestraints()
    {
        Vector3 pos = mainCamera.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = mainCamera.ViewportToWorldPoint(pos);
    }
}
