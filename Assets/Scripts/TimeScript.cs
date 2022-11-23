using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeScript : MonoBehaviour
{
    public float currentTime, maxTime;
    [SerializeField] private TimeBar timeBar;
    [SerializeField] private EndGame endGame;

    void Start()
    {
        currentTime = maxTime;
    }

    private void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        timeBar.SetTime();
        GameOver();
    }
    private void GameOver()
    {
        if (currentTime <= 0f)
        {
            Debug.Log("Vous êtes mort !");
            endGame.GameOver();
        }
    }

    public void AddTime()
    {
        currentTime += 10;
        if (currentTime >= maxTime)
        {
            currentTime = maxTime;
        }
    }
}
