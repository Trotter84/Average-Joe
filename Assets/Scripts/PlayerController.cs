using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    private Camera mainCamera;
    [SerializeField] private Transform crosshair;
    [SerializeField] private Weapon weapon;

    [Header("Attributes")]
    [SerializeField] private float playerSpeed = 5.0f;
    private float verticalInput;
    private float horizontalInput;

    

    void Start()
    {
        mainCamera = Camera.main;
        Cursor.visible = false;
    }

    void Update()
    {
        PlayerMovement();
        MouseMovement();

        FireGun();
    }

    void PlayerMovement()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        transform.position += Vector3.up * playerSpeed * Time.deltaTime * verticalInput;
        transform.position += Vector3.right * playerSpeed * Time.deltaTime * horizontalInput;
    }

    void MouseMovement()
    {
        Vector3 mouse = Input.mousePosition;

        Vector3 mousePoint = mainCamera.ScreenToWorldPoint(mouse);
        mousePoint.z = -2f;
        crosshair.position = mousePoint;

        Vector3 screenPoint = mainCamera.WorldToScreenPoint(transform.localPosition);
        screenPoint.z = -0.1f;

        Vector2 offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);

        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        
        transform.eulerAngles = new Vector3(angle, -90f, 90f);
    }

    void FireGun()
    {
        if (Input.GetMouseButtonDown(0))
        {
            weapon.Fire();
        }
    }

    // void OnGUI()
    // {
    //     var mousePos = Event.current.mousePosition;

    //     mousePos.x = Mathf.Clamp(mousePos.x, 100, 1002);
    //     mousePos.y = Mathf.Clamp(mousePos.y, 100, 476);
    // }
}
