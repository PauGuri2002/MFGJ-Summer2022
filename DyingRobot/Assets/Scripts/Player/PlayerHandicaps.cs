using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandicaps : MonoBehaviour
{
    private Handicap[] handicaps = new Handicap[]
    {
        new Handicap("speed",0.5f),
        new Handicap("fireCooldown",2f)
    };
    private PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
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

            Debug.Log("ROBOT SAVED");
            FindObjectOfType<DoorManager>().UnlockDoors();
        }
    }
}
