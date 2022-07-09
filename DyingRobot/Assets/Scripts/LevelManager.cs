using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour
{
    public Door[] doors;
    public Animator transitionAnimator;
    Door lastUsedDoor;
    GameObject player;
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

    public void EnterDoor(Door door, GameObject _player)
    {
        lastUsedDoor = Array.Find(doors, d => d.doorPosition == door.doorPosition * -1);
        player = _player;

        FindObjectOfType<AudioManager>().PlaySound("Sweep");
        StartCoroutine(LoadNextLevel());
    }

    public IEnumerator LoadNextLevel()
    {
        player.GetComponent<PlayerInput>().enabled = false;
        transitionAnimator.Play(directionToAnimationName(lastUsedDoor.doorPosition));

        yield return new WaitForSeconds(transitionAnimator.GetCurrentAnimatorStateInfo(0).length);

        var asyncSceneLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        while (!asyncSceneLoad.isDone)
        {
            Debug.Log("Scene loading...");
            yield return null;
        }

        Vector3 newPlayerPosition = (player.transform.position * -1);
        player.transform.position = newPlayerPosition;
        foreach (Door d in doors)
        {
            d.Lock(d == lastUsedDoor);
        }
        player.GetComponent<PlayerInput>().enabled = true;
        
        transitionAnimator.SetTrigger("SceneLoaded");
    }

    private string directionToAnimationName(Vector2 direction)
    {
        switch (direction.x)
        {
            case -1:
                return "LevelTransitionInWest";
            case 1:
                return "LevelTransitionInEast";
        }
        switch (direction.y)
        {
            case -1:
                return "LevelTransitionInSouth";
            case 1:
                return "LevelTransitionInNorth";
        }
        return "LevelTransitionInWest";
    }
}