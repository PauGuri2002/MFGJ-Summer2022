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
    public GameObject bulletPrefab;
    private Rigidbody2D rb;
    private Vector3 moveDirection;
    public float speed = 0.1f;
    
    public float fireCooldown = 0.2f;
    private float fireTimer;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        fireAction = playerInput.actions["Fire"];
        interactAction = playerInput.actions["Interact"];
        fireTimer = fireCooldown;

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = (Vector3)moveAction.ReadValue<Vector2>();

        if (fireAction.ReadValue<Vector2>() != Vector2.zero && fireTimer <= 0)
        {
            fireTimer = fireCooldown;
            GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            newBullet.GetComponent<Bullet>().direction = fireAction.ReadValue<Vector2>();
        }

        if(interactAction.ReadValue<float>() > 0)
        {
            Debug.Log("Interacted");
            transform.localScale += new Vector3(5,5,5);
        }

        fireTimer -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        rb.MovePosition(transform.position + moveDirection*speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger Enter");
    }
}
