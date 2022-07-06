using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isLocked = true;
    public Vector2 doorPosition;
    Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isLocked)
        {
            FindObjectOfType<DoorManager>().EnterDoor(this, collision.gameObject);
        } else
        {
            Debug.Log("Door is locked!");
        }
    }

    public void Lock()
    {
        isLocked = true;
        animator.Play("DoorClosing");
    }

    public void Unlock()
    {
        isLocked = false;
        animator.Play("DoorOpening");
    }
}
