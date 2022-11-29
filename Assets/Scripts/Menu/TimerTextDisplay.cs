using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerTextDisplay : MonoBehaviour
{
    public float timer;
    public TextMeshProUGUI timerText;
    public string timePassed;


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
    }

    public void TimeSurvivedText()
    {
        Debug.Log("bonjour");
        timePassed = string.Format("{0:0}m : {1:00}s", Time.timeSinceLevelLoad / 60, Time.timeSinceLevelLoad % 60);
        timerText.text = $"\nYou survived {timePassed}";
        if (timer < 60)
        {
            timerText.text += "\nBad performance";
        }
        else if (timer > 60)
        {
            timerText.text += "\nInteresting !";
        }
        else if (timer > 60)
        {
            timerText.text += "\nWell played";
        }
        //timerText.text = "You survived : " + timer + " seconds";
    }
}
