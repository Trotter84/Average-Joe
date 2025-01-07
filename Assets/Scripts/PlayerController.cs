using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody rb;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private MouseController mouseController;
    [SerializeField] private Weapon weaponScript;

    [Header("Attributes")]
    [SerializeField] private float speed = 5.0f;
    private Vector2 moveDiagonal;
    public List<GameObject> inventory = new List<GameObject>();
    private int weaponChoice = 0;

    private float timer;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        mainCamera = Camera.main;

        InventoryController();
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
    }

    void Inputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDiagonal = new Vector2(moveX, moveY).normalized;

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (0 > timer)
            {
                weaponScript.Reload();

                timer = weaponScript.reloadTime;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponChoice = 0;
            InventoryController();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponChoice = 1;
            InventoryController();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weaponChoice = 2;
            InventoryController();
        }
    }

    void InventoryController()
    {
        foreach (var weapon in inventory)
        {
            weapon.SetActive(false);
        }
        
        inventory[weaponChoice].SetActive(true);

        weaponScript = GameObject.FindGameObjectWithTag("Gun").GetComponent<Weapon>();
        if (weaponScript == null)
        {
            Debug.LogError("Weapon : Weapon on Player is NULL.");
        }
        mouseController.ChangeGun();
        weaponScript.ChangeAudioSource();

    }

    void PlayerMovement()
    {
        rb.linearVelocity = new Vector2(moveDiagonal.x * speed, moveDiagonal.y * speed);
    }

    public void FaceMouse(Vector3 mouse)
    {

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
