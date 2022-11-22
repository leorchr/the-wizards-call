using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public GameObject gameOverMenu;
    public GameObject player;
    public PlayerHealth playerHealth;
    public HealthBar healthBar;

    private void Start()
    {
        gameOverMenu.SetActive(false);
    }

    public void GameOver()
    {
            gameOverMenu.SetActive(true);
            Time.timeScale = 0;
    }

    public void Retry()
    {
        player.transform.position = Vector3.zero;
        Time.timeScale = 1;
        playerHealth.invincible = true;
        playerHealth.currentHp = playerHealth.maxHp;
        healthBar.SetHealth();
        gameOverMenu.SetActive(false);
    }
}
