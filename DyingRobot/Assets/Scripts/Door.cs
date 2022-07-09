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
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isLocked)
        {
            FindObjectOfType<LevelManager>().EnterDoor(this, collision.gameObject);
        }
    }

    public void Lock(bool playAnimation)
    {
        isLocked = true;
        if (playAnimation)
        {
            animator.Play("DoorClosing");
        } else
        {
            animator.Play("DoorClosedInstant");
        }
    }

    public void Unlock()
    {
        isLocked = false;
        animator.Play("DoorOpening");
    }
}
