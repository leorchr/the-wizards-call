using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab1, enemyPrefab2, enemyPrefab3;
    [SerializeField] private GameObject player;
    [SerializeField] private int nbEnemies1, nbEnemies2, nbEnemies3;
    public int currentEnemies;
    [SerializeField] private Vector2 limitsX, limitsY;


    void Start()
    {
        currentEnemies = nbEnemies1+nbEnemies2+nbEnemies3;
        Spawn(enemyPrefab1, nbEnemies1);
        Spawn(enemyPrefab2, nbEnemies2);
        Spawn(enemyPrefab3, nbEnemies3);
    }

    private void Update()
    {
        Respawn();
    }

    private void Respawn()
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
            if (nbEnemies2 == 3)
            {
                nbEnemies2 = 0;
                nbEnemies3++;
            }
            currentEnemies = nbEnemies1 + nbEnemies2 + nbEnemies3;
            Spawn(enemyPrefab1, nbEnemies1);
            Spawn(enemyPrefab2, nbEnemies2);
            Spawn(enemyPrefab3, nbEnemies3);

        }
    }

    private void Spawn(GameObject enemy, int nbEnemies)
    {
        for (int i = 0; i < nbEnemies; i++)                                     // Spawn des ennemis pas directement sur le joueur
        {
            float x, y;
            if (player.transform.position.x - 2 < limitsX.x || player.transform.position.x + 2 > limitsX.y)
            {
                x = Random.Range(limitsX.x, limitsX.y);

            }
            else
            {
                x = Random.Range(limitsX.x, player.transform.position.x - 2);
                float x2 = Random.Range(limitsX.y, player.transform.position.x + 2);
                float x3 = Random.Range(0, 2);
                if (x3 == 1)
                {
                    x = x2;
                }
            }

            if (player.transform.position.y - 2 < limitsY.x || player.transform.position.y + 2 > limitsY.y)
            {
                y = Random.Range(limitsY.x, limitsY.y);
            }
            else
            {
                y = Random.Range(limitsY.x, player.transform.position.y - 2);
                float y2 = Random.Range(limitsY.y, player.transform.position.y + 2);
                float y3 = Random.Range(0, 2);
                if (y3 == 1)
                {
                    y = y2;
                }
            }
            Instantiate(enemy, new Vector2(x, y), Quaternion.identity);
        }
    }
}
