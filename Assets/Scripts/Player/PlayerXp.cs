using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private GameObject prefab;
    private TextMeshProUGUI levelUpInfo;
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
            Instantiate(prefab, transform.position + new Vector3(Screen.width * 0.5f, Screen.height * 0.5f + 95, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            levelUpInfo = prefab.GetComponent<TextMeshProUGUI>();
            levelUpInfo.text = $"Lvl {level} \n+{addLevelSpeed} Speed \n+{addLevelDamage} Damages";
            //Destroy(prefab);
        }
    }
}
