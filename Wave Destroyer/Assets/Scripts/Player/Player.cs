using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float bulletSpeed = 20f;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Camera cam;
   // [SerializeField] private ObjectPooler pooler;
    PlayerController controller;

    [Header("Inputs")]
    [SerializeField] private Vector2 moveInput;
    [SerializeField] private Vector2 mousePos;

    private void Awake()
    {
        controller = new PlayerController();
        Movement();
    }

    void Movement()
    {
        controller.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controller.Player.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnEnable()
    {
        controller.Player.Enable();
    }

    private void OnDisable()
    {
        controller.Player.Disable();
    }

    private void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        /*
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
        */
    }

    private void FixedUpdate()
    {
        Vector2 move = rb.position + moveInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(move);

        MouseLook();
    }

    void MouseLook()
    {
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
    /*
    void Shoot()
    {
        GameObject bullet = pooler.SpawnFromPools("Bullet", firePoint.position, firePoint.rotation);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
    }\*/
}