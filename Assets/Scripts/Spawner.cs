using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;
    public int nbEnemies1;
    public int nbEnemies2;
    public int nbEnemies3;
    public int currentEnemies;
    //public int[,] enemy = new int[3, 10];

    public Vector2 limitsX, limitsY;


    void Start()
    {
        currentEnemies = nbEnemies1+nbEnemies2+nbEnemies3;
        Spawn(enemyPrefab1, nbEnemies1);
        Spawn(enemyPrefab2, nbEnemies2);
        Spawn(enemyPrefab3, nbEnemies3);
    }

    private void Update()
    {
        if (currentEnemies == 0)
        {
            if (nbEnemies1 <= 2)
            {
                nbEnemies1++;
            }
            else if (nbEnemies2 <= 2 && nbEnemies1 == 3)
            {
                nbEnemies1 = 0;
                nbEnemies2++;
            }
            if(nbEnemies2==3)
            {
                nbEnemies2 = 0;
                nbEnemies3++;
            }
            currentEnemies = nbEnemies1+nbEnemies2+nbEnemies3;
            Spawn(enemyPrefab1, nbEnemies1);
            Spawn(enemyPrefab2, nbEnemies2);
            Spawn(enemyPrefab3, nbEnemies3);

        }
    }

    private void Spawn(GameObject enemy, int nbEnemies)
    {
        for (int i = 0; i < nbEnemies; i++)                                     // Spawn des ennemis
        {
            float x = Random.Range(limitsX.x, limitsX.y);
            float y = Random.Range(limitsY.x, limitsY.y);
            Instantiate(enemy, new Vector2(x, y), Quaternion.identity);
        }
    }
}
