using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using static UnityEditor.Timeline.TimelinePlaybackControls;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    [Header("Statistics")]
    [SerializeField] private int hp;                    // nombre de pv de l'ennemi
    [SerializeField] private int speed;                 // vitesse de l'ennemi
    public int damage;                                  // nombre de degats infliges par l'epee
    public int xp;                                      // nombre d'xp que l'ennemi donne au joueur a sa mort

    [Header("Knockback")]
    [SerializeField] private int kbSpeed;               // vitesse a laquelle l'ennemi se deplace quand il est touche
    [SerializeField] private int kbPlayerSpeed;         // vitesse de l'ennemis, du kb de l'ennemi et du joueur
    [SerializeField] private float duration;            // vitesse et duree du kb
    private bool kb = false;

    [Header("Aggro")]
    [SerializeField] private int aggroDistance;
    private float distance;                             // pour stocker la distance entre le joueur et l'ennemi (aggro)
    
    private bool contact = false;                       // verifie s'il y a eu un contact avec le joueur
    private Rigidbody2D rb;
    private Spawner spawner;

    // Player Components

    private GameObject player;
    private Rigidbody2D playerRb;
    private PlayerXp playerXp;
    private PlayerController playerController;

    private void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        playerRb = player.GetComponent<Rigidbody2D>();
        playerXp = player.GetComponent<PlayerXp>();
        playerController = player.GetComponent<PlayerController>();
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
    }

    void FixedUpdate()
    {
        Aggro();
        Knockback();
        PlayerContact();
    }

    void Aggro()                                        // fonction pour que l'ennemi regarde le joueur des qu'il rentre dans perimetre
    {
        distance = Vector3.Distance(player.transform.position, transform.position);       // donne la distance entre l'ennemi et le joueur
        if (distance <= aggroDistance)
        {
            transform.right = player.transform.position - transform.position;
            rb.velocity = new Vector2(speed * Time.deltaTime, speed * Time.deltaTime) * transform.right;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    void Knockback()                                    // fonction qui fait reculer l'ennemi quand il est touché
    {
        if (kb == true)                                 // deplace l'ennemi quand il est touche
        {
            rb.AddForce(transform.right * Time.deltaTime * -kbSpeed);
        }
    }

    void PlayerContact()
    {
        if (contact == true)     // deplace le personnage et l'ennemi quand ils se touchent
        {
            rb.AddForce(transform.right * Time.deltaTime * -kbSpeed);
            playerRb.AddForce(transform.right * Time.deltaTime * kbPlayerSpeed);
        }
    }

    void TakeDamage(int damage)                         // fonction qui inflige des degats a l'ennemi et qui le detruit quand il n'a plus de pv
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
            spawner.currentEnemies--;
            playerXp.AddXp(xp);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Stick")
        {
            TakeDamage(playerController.damage);
            StartCoroutine(KbDuration());
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.name == "Player")
        {
            StartCoroutine(ContactDuration());
        }
    }

    private IEnumerator KbDuration()                    // pour la duree du kb
    {
        kb = true;
        yield return new WaitForSeconds(duration);
        rb.velocity = Vector3.zero;
        kb = false;
    }

    private IEnumerator ContactDuration()               // pour la duree du contact
    {
        contact = true;
        yield return new WaitForSeconds(duration);
        rb.velocity = Vector3.zero;
        playerRb.velocity = Vector3.zero;
        contact = false;
    }
}
