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
        FindObjectOfType<AudioManager>().PlaySound("Sweep");
    }

    public void EnterDoor(Door door, GameObject player)
    {
        lastUsedDoor = Array.Find(doors, d => d.doorPosition == door.doorPosition * -1);

        foreach (Door d in doors)
        {
            d.Lock(d == lastUsedDoor);
        }
        FindObjectOfType<AudioManager>().PlaySound("Sweep");
        SceneManager.LoadScene("Level2");
        Vector3 newPlayerPosition = (player.transform.position * -1);
        GameObject.FindGameObjectWithTag("Player").transform.position = newPlayerPosition;
    }
}