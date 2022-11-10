using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class XpBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private PlayerXp playerXp;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private TextMeshProUGUI lvlText;

    private void Start()
    {
        slider.maxValue = playerXp.maxXp;
        slider.value = playerXp.currentXp;
        infoText.text = $"{playerXp.currentXp}  /  {playerXp.maxXp}";
        lvlText.text = "LVL. " + playerXp.level;
    }

    public void SetXp(int xp)       // augmente la barre d'xp et change son texte
    {
        slider.value = xp;
        infoText.text = $"{xp}  /  {playerXp.maxXp}";
    }

    public void SetLevel()          // augmente la taille de la barre d'xp et l'affichage du niveau
    {
        slider.maxValue = playerXp.maxXp;
        lvlText.text = "LVL. " + playerXp.level;
    }
}