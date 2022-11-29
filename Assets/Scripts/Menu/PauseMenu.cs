using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public LevelUpStats levelUpStats;

    private void Start()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
    public void Pause(InputAction.CallbackContext context)
    {
        if (pauseMenu.activeSelf == true)
        {
            Resume();
        }
        else
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            levelUpStats.stats.SetActive(false);
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void Exit()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
}
