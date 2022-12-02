using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Statistics")]
    [SerializeField] private int hp;
    public float speed;
    public int damage;
    public int xp;                                      // nombre d'xp que l'ennemi donne au joueur a sa mort

    [Header("Knockback")]
    [SerializeField] private int kbSpeed;               // vitesse a laquelle l'ennemi se deplace quand il est touche
    [SerializeField] private int kbPlayerSpeed;         // vitesse a laquelle le joueur se deplace quand il est touche
    [SerializeField] private float duration;
    private bool kb = false;

    [Header("Aggro")]
    [SerializeField] private int aggroDistance;
    private float distance;                             // pour stocker la distance entre le joueur et l'ennemi (aggro)
    
    private bool contact = false;                       // verifie s'il y a eu un contact avec le joueur
    private Rigidbody2D rb;
    private Spawner spawner;
    private Audio _audio;

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
        _audio = GameObject.Find("Audio Source").GetComponent<Audio>();
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
        if (kb == true)
        {
            rb.AddForce(transform.right * Time.deltaTime * -kbSpeed);
        }
    }

    void PlayerContact()     // deplace le personnage et l'ennemi quand ils se touchent
    {
        if (contact == true)
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
            _audio.DeathSound();
            Destroy(gameObject);
            spawner.currentEnemies--;
            playerXp.AddXp(xp);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stick")
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

    private IEnumerator KbDuration()
    {
        kb = true;
        yield return new WaitForSeconds(duration);
        rb.velocity = Vector3.zero;
        kb = false;
    }

    private IEnumerator ContactDuration()
    {
        contact = true;
        yield return new WaitForSeconds(duration);
        rb.velocity = Vector3.zero;
        playerRb.velocity = Vector3.zero;
        contact = false;
    }
}
