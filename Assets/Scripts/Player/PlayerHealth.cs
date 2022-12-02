using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int currentHp, maxHp;
    public bool invincible = false;                                                    // si le joueur est en état d'invincibilité
    [SerializeField] private float invincibleDuration;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private EndGame endGame;

    void Start()
    {
        currentHp = maxHp;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!invincible)
            {
                currentHp -= collision.gameObject.GetComponent<Enemy>().damage;        // enlève le nombre de degats de l'ennemi aux pv du joueur
                healthBar.SetHealth();                                                 // ajuste la barre de vie
                StartCoroutine(Invulnerability());
                GameOver();
            }
        }
    }

    private void GameOver()
    {
        if (currentHp <= 0)
        {
            currentHp = 0;
            healthBar.SetHealth();
            Debug.Log("Vous êtes mort !");
            endGame.GameOver();
        }
    }

    public void AddHp()         // pour augmenter la taille maximale de la barre d'HP
    {
        maxHp += 10;
        currentHp = maxHp;
        healthBar.SetMaxHealth();
    }

    IEnumerator Invulnerability()
    {
        invincible = true;
        yield return new WaitForSeconds(invincibleDuration);
        invincible = false;
    }
}
