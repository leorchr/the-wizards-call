using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private TextMeshProUGUI infoText;

    private void Start()
    {
        slider.maxValue = playerHealth.maxHp;
        slider.value = playerHealth.maxHp;
        infoText.text = $"{playerHealth.maxHp}  /  {playerHealth.maxHp}";
    }

    public void SetHealth()
    {
        slider.value = playerHealth.currentHp;
        infoText.text = $"{playerHealth.currentHp}  /  {playerHealth.maxHp}";
    }

    public void SetMaxHealth()
    {
        slider.maxValue = playerHealth.maxHp;
        slider.value = playerHealth.currentHp;
        infoText.text = $"{playerHealth.currentHp}  /  {playerHealth.maxHp}";
    }
}
