using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorManager : MonoBehaviour
{
    public Door[] doors;
    Door lastUsedDoor;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        lastUsedDoor = null;
    }

    public void UnlockDoors()
    {
        foreach(Door d in doors)
        {
            if(d != lastUsedDoor)
            {
                d.Unlock();
            }
        }
    }

    public void EnterDoor(Door door, GameObject player)
    {
        foreach(Door d in doors)
        {
            d.Lock();
        }

        FindObjectOfType<AudioManager>().PlaySound("Sweep");

        lastUsedDoor = Array.Find(doors, d => d.doorPosition == door.doorPosition * -1);
        SceneManager.LoadScene("Level2");
        Vector3 newPlayerPosition = (player.transform.position * -1);
        GameObject.FindGameObjectWithTag("Player").transform.position = newPlayerPosition;
    }
}
