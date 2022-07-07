using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandicaps : MonoBehaviour
{
    private Handicap[] handicaps = new Handicap[]
    {
        new Handicap("speed",0.5f),
        new Handicap("fireCooldown",2f)
    };
    private PlayerController pc;
    private DialogueManager dialogueManager;
    private TargetRobot collidedTarget;

    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<PlayerController>();
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    // INTERACTION
    public void Interact(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            FindObjectOfType<Jail>().Unlock();

            if(collidedTarget != null && !dialogueManager.dialogueBox.activeSelf)
            {
                if (collidedTarget.healed)
                {
                    dialogueManager.ShowDialogue(new string[] { "You saved me!", "Thanks! :)" });
                    return;
                }
                collidedTarget.healed = true;

                Handicap chosenHandicap = handicaps[Random.Range(0, handicaps.Length)];
                Debug.Log("Lost " + chosenHandicap.property);
                switch (chosenHandicap.property)
                {
                    case "speed":
                        pc.speed *= chosenHandicap.percentage;
                        break;
                    case "fireCooldown":
                        pc.fireCooldown *= chosenHandicap.percentage;
                        break;
                    default:
                        break;
                }

                dialogueManager.ShowDialogue(new string[] { "You saved me!", "Thanks! :)", "(you have lost " + chosenHandicap.property + ")" });
                FindObjectOfType<DoorManager>().UnlockDoors();
            } else
            {
                dialogueManager.Interact();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            Debug.Log("Entered target");
            collidedTarget = collision.gameObject.GetComponent<TargetRobot>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            Debug.Log("Left target");
            collidedTarget = null;
        }
    }
}
