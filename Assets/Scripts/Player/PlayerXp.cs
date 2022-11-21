using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
public class PlayerXp : MonoBehaviour
{
    public int currentXp;
    public int maxXp;
    public int level;
    public float addLevelSpeed;
    public int addLevelDamage;
    [SerializeField] private XpBar xpBar;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerController player;

    public Canvas statsCanvas;
    public Vector3 statsOffset;
    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject stats;
    private TextMeshProUGUI levelUpInfo;

    private void Start()
    {
        stats = Instantiate(prefab, statsCanvas.transform);
        stats.SetActive(false);
        levelUpInfo = stats.GetComponent<TextMeshProUGUI>();
    }

    public void AddXp(int xp)       // ajoute de l'xp au joueur
    {
        currentXp += xp;
        LevelUp();
        xpBar.SetXp(currentXp);
    }
    private void LevelUp()          // augmente le niveau du joueur
    {
        if (currentXp >= maxXp)
        {
            level++;
            currentXp -= maxXp;
            maxXp += 20;
            playerHealth.AddHp();
            player.speed += addLevelSpeed;
            player.damage += addLevelDamage;
            xpBar.SetLevel();
            DisplayStats();
        }
    }

    private void DisplayStats()     // affiche des statistiques au dessus du joueur
    {
        levelUpInfo.text = $"Lvl {level} \n+{addLevelSpeed} Speed \n+{addLevelDamage} Damage";
        stats.SetActive(true);
        stats.transform.localPosition = statsOffset;
        StartCoroutine(Waiter());
    }
    private IEnumerator Waiter()
    {
        yield return new WaitForSeconds(3f);
        stats.SetActive(false);
    }
}
