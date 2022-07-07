using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandicaps : MonoBehaviour
{
    public HandicapList handicapList;
    private Handicap[] handicaps;
    private PlayerController pc;
    private DialogueManager dialogueManager;
    private TargetRobot collidedTarget;

    private Handicap chosenHandicap;

    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<PlayerController>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        handicaps = handicapList.Handicaps;
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
                    dialogueManager.ShowDialogue(chosenHandicap.dialogue);
                    return;
                }
                collidedTarget.healed = true;

                chosenHandicap = handicaps[Random.Range(0, handicaps.Length)];
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

                dialogueManager.ShowDialogue(chosenHandicap.dialogue);
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
