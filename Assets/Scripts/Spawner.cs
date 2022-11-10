using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int nbEnemies;
    public int currentEnemies;
    public Vector2 limitsX, limitsY;


    void Start()
    {
        currentEnemies = nbEnemies;
        Spawn();
    }

    private void Update()
    {
        if (currentEnemies == 0)
        {
            nbEnemies++;
            currentEnemies = nbEnemies;
            Spawn();
        }
    }

    private void Spawn()
    {
        for (int i = 0; i < nbEnemies; i++)                                     // Spawn des ennemis
        {
            float x = Random.Range(limitsX.x, limitsX.y);
            float y = Random.Range(limitsY.x, limitsY.y);
            Instantiate(enemyPrefab, new Vector2(x, y), Quaternion.identity);
        }
    }
}
