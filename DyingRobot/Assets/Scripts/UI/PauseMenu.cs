using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseMenu;

    public void TogglePause()
    {
        if (isPaused)
        {
            isPaused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        } else
        {
            isPaused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void QuitToMenu()
    {
        foreach (GameObject o in FindObjectsOfType<GameObject>())
        {
            Destroy(o);
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
