using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isLocked = true;
    public Vector2 doorPosition;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
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
        GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
    }

    public void Unlock()
    {
        isLocked = false;
        GetComponent<SpriteRenderer>().color = new Color32(0, 255, 0, 255);
    }
}
