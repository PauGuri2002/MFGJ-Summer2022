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

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        fireAction = playerInput.actions["Fire"];
        interactAction = playerInput.actions["Interact"];
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)moveAction.ReadValue<Vector2>();
        
        if(fireAction.ReadValue<Vector2>() != Vector2.zero)
        {
            GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            newBullet.GetComponent<Bullet>().direction = fireAction.ReadValue<Vector2>();
        }

        if(interactAction.ReadValue<float>() > 0)
        {
            Debug.Log("Interacted");
            transform.localScale += new Vector3(5,5,5);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {

    }

    public void Fire(InputAction.CallbackContext context)
    {

    }

    public void Interact(InputAction.CallbackContext context)
    {

    }
}
