using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUpStats : MonoBehaviour
{
    public Canvas statsCanvas;
    public Vector3 statsOffset;
    [SerializeField] private GameObject prefab;
    public GameObject stats;
    private TextMeshProUGUI levelUpInfo;
    [SerializeField] private PlayerXp playerXp;

    void Start()
    {
        stats = Instantiate(prefab, statsCanvas.transform);
        stats.SetActive(false);
        levelUpInfo = stats.GetComponent<TextMeshProUGUI>();
    }

    public void DisplayStats()     // affiche des statistiques au dessus du joueur
    {
        levelUpInfo.text = $"Lvl {playerXp.level} \n+{playerXp.addLevelSpeed} Speed \n+{playerXp.addLevelDamage} Damage";
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
