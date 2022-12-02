using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerTextDisplay : MonoBehaviour
{
    public float timer;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI timerTextInGame;
    public string timePassed;
    private int minutes;
    private int seconds;


    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        timerText.text = "You survived : " + timer;
    }

    // Update is called once per frame
    void Update()
    {
        timer += 1 * Time.deltaTime;
        minutes = Mathf.FloorToInt(timer / 60f);
        seconds = Mathf.FloorToInt(timer-minutes * 60);
        timerTextInGame.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void TimeSurvivedText()
    {
        timePassed = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText.text = $"\nYou survived {timePassed}";
        if (timer < 60)
        {
            timerText.text += "\nBad performance";
        }
        else if (timer > 60 && timer < 120)
        {
            timerText.text += "\nInteresting !";
        }
        else if (timer >= 120)
        {
            timerText.text += "\nWell played";
        }
        //timerText.text = "You survived : " + timer + " seconds";
    }
}
