using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TimeScript timeScript;

    private void Start()
    {
        slider.maxValue = timeScript.maxTime;
        slider.value = timeScript.maxTime;
    }

    public void SetTime()
    {
        slider.value = timeScript.currentTime;
    }

    public void SetMaxHealth()
    {
        slider.maxValue = timeScript.maxTime;
        slider.value = timeScript.currentTime;
    }
}
