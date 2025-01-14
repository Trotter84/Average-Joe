using Unity.VisualScripting;
using UnityEngine;


public class MouseController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private PlayerController player;
    [SerializeField] public Weapons weapon;

    public bool isShooting = false;


    void Awake()
    {
        Cursor.visible = false;
    }

    void Start()
    {
        mainCamera = Camera.main;


        player = GameObject.Find("Player").GetComponent<PlayerController>();
        if (player == null)
        {
            Debug.LogError("The Player : PlayerController on Crosshair : MouseController is NULL.");
        }

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isShooting = true;
            weapon.muzzleFlash.SetBool("isShooting", isShooting);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isShooting = false;
            weapon.muzzleFlash.SetBool("isShooting", isShooting);
        }
    }

    public void ChangeGun()
    {
        weapon = GameObject.FindGameObjectWithTag("Gun").GetComponent<Weapons>();
        if (weapon == null)
        {
            Debug.LogError("The Weapon : Weapon on Crosshair : MouseController is NULL.");
        }
    }

    void FixedUpdate()
    {
        MouseMovement();
    }

    void MouseMovement()
    {
        Vector3 mouse = Input.mousePosition;

        Vector3 mousePoint = mainCamera.ScreenToWorldPoint(mouse);
        mousePoint.z = -3f;
        transform.position = mousePoint;

        player.FaceMouse(mouse);
    }

}
