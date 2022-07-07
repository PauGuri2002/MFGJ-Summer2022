using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction fireAction;
    private InputAction interactAction;

    private Rigidbody2D rb;
    private Vector3 moveDirection;
    private float acceleration = 0f;
    public float speed = 0.1f;
    public float accelerationTime = 0.5f;
    public float decelerationTime = 0.5f;

    public GameObject bulletPrefab;
    public float fireCooldown = 0.2f;
    private float fireTimer;
    public float bulletMomentum = 0.2f;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        fireAction = playerInput.actions["Fire"];
        interactAction = playerInput.actions["Interact"];
        fireTimer = fireCooldown;

        rb = GetComponent<Rigidbody2D>();

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        // MOVEMENT
        Vector3 currentMoveDirection = (Vector3)moveAction.ReadValue<Vector2>();
        if(currentMoveDirection != Vector3.zero)
        {
            moveDirection = currentMoveDirection;
            if (acceleration < 1f) {
                acceleration += 1 / (accelerationTime / Time.deltaTime);
            } else
            {
                acceleration = 1f;
            }
        } else
        {
            if (acceleration > 0f)
            {
                acceleration -= 1 / (decelerationTime / Time.deltaTime);
            } else
            {
                acceleration = 0f;
                moveDirection = currentMoveDirection;
            }
        }

        // FIRING
        if (fireAction.ReadValue<Vector2>() != Vector2.zero && fireTimer <= 0)
        {
            fireTimer = fireCooldown;
            GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            newBullet.GetComponent<Bullet>().direction = fireAction.ReadValue<Vector2>() + ((Vector2)moveDirection * acceleration * bulletMomentum);
            FindObjectOfType<AudioManager>().PlaySound("Fire");
        }

        fireTimer -= Time.deltaTime;
    }

    // INTERACTION
    void Interact(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Interacted");
            FindObjectOfType<Jail>().Unlock();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(transform.position + moveDirection * speed * acceleration);
    }
}
