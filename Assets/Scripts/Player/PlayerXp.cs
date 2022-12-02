using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
public class PlayerXp : MonoBehaviour
{
    [Header("Experience Statistics")]
    public int currentXp;
    public int maxXp;
    public int level;
    [Header("Level Up Bonus")]
    public float addLevelSpeed;
    public int addLevelDamage;
    [Header("Components")]
    [SerializeField] private XpBar xpBar;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerController player;
    [SerializeField] private LevelUpStats levelUp;


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
            if (level < 10)
            {
                level++;
                currentXp -= maxXp;
                maxXp += 10;
                playerHealth.AddHp();
                player.speed += addLevelSpeed;
                player.damage += addLevelDamage;
                xpBar.SetLevel();
                levelUp.DisplayStats();
            }
            else
            {
                level++;
                currentXp -= maxXp;
                maxXp += 100;
                playerHealth.AddHp();
                player.speed += addLevelSpeed;
                player.damage += addLevelDamage;
                xpBar.SetLevel();
                levelUp.DisplayStats();
            }
        }
    }
}
