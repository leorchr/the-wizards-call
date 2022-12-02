using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab1, enemyPrefab2, enemyPrefab3;
    [SerializeField] private GameObject player;
    [SerializeField] private int nbEnemies1, nbEnemies2, nbEnemies3;
    public int currentEnemies;
    [SerializeField] private Vector2 limitsX, limitsY;
    [SerializeField] private bool isSpawning;
    private float x, y;
    [SerializeField] private GameObject particulePrefab1;
    [SerializeField] private GameObject particulePrefab2;
    [SerializeField] private GameObject particulePrefab3;
    private GameObject particule;
    [SerializeField] private GameObject timePrefab;

    void Start()
    {
        currentEnemies = nbEnemies1+nbEnemies2+nbEnemies3;
        Spawn(enemyPrefab1, nbEnemies1);
        Spawn(enemyPrefab2, nbEnemies2);
        Spawn(enemyPrefab3, nbEnemies3);
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ParticleSystem.MainModule psmain = ps.main;
    }

    private void Update()
    {
        Respawn();
    }

    private void Respawn()
    {
        if (currentEnemies == 0)
        {
            Instantiate(timePrefab);
            if (nbEnemies3 == 0)
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
            }
            else
            {
                if (nbEnemies1 <= 4)
                {
                    nbEnemies1 += 2;
                }
                else if (nbEnemies2 <= 4 && nbEnemies1 >= 6)
                {
                    nbEnemies1 = 1;
                    nbEnemies2 +=2;
                }
                if (nbEnemies2 >= 3)
                {
                    nbEnemies2 = 1;
                    nbEnemies3++;
                }
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
            ParticuleColor(enemy);
        }
    }

    private void ParticuleColor(GameObject enemy)
    {
        if (enemy == enemyPrefab1)
        {
            particule = Instantiate(particulePrefab1, new Vector3(x, y), particulePrefab1.transform.rotation);
            StartCoroutine(Waiter(enemy, particule, x, y));
        }
        else if (enemy == enemyPrefab2)
        {
            particule = Instantiate(particulePrefab2, new Vector3(x, y), particulePrefab2.transform.rotation);
            StartCoroutine(Waiter(enemy, particule, x, y));
        }
        else
        {
            particule = Instantiate(particulePrefab3, new Vector3(x, y), particulePrefab3.transform.rotation);
            StartCoroutine(Waiter(enemy, particule, x, y));
        }
    }

    IEnumerator Waiter(GameObject enemy,GameObject particule, float x, float y)        // pause entre les vagues
    {
        yield return new WaitForSeconds(1f);
        Instantiate(enemy, new Vector2(x, y), Quaternion.identity);
        Destroy(particule);
    }
}
