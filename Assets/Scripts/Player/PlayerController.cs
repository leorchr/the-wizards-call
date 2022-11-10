using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerController : MonoBehaviour
{
    [Header("Player Statistics")]
    public int damage;                                  // degats qu'inflige le joueur
    public float speed;                                 // vitesse du joueur

    [Header("SpeedBoost Competence")]
    [SerializeField] private int speedBoost;            // vitesse du joueur pendant son speedboost
    [SerializeField] private float duration;            // duree du speedboost
    [SerializeField] private float cooldown;            // duree du cooldown du speedboost
    [SerializeField] private bool isReady = true;

    [Header("Objects & Components")]
    public Rigidbody2D RB;
    public GameObject Pivot;                            // pour l'attaque
    private float angle;                                // variable pour definir l'angle de vue du joueur en fonction de la position de la souris ou du joystick

    private void Update()
    {
        CameraMovement();
    }

    public void LookMouse(InputAction.CallbackContext context)          // orientation de la vue du personnage avec la souris
    {

        Vector2 mouse = context.ReadValue<Vector2>();
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        Vector2 offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
        angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

    }

    public void LookJoystick(InputAction.CallbackContext context)       // orientation de la vue du personnage avec le joystick
    {

        Vector2 joystick = context.ReadValue<Vector2>();
        angle = Mathf.Atan2(joystick.y, joystick.x) * Mathf.Rad2Deg;
        if (angle != 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, angle + 90);
        }

    }

    public void Movement(InputAction.CallbackContext value)             // mouvements du personnage
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        inputMovement.Normalize();
        RB.velocity = new Vector2(speed * inputMovement.x, speed * inputMovement.y);
    }

    private void CameraMovement()                                       // camera suit le joueur
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y);
    }

    public void Attack(InputAction.CallbackContext value)               // attaque
    {
        if (value.started)
        {
            Pivot.GetComponent<Animator>().SetTrigger("Attaque");
        }
    }

    public void Boost(InputAction.CallbackContext value)                // competence Boost
    {
        if (value.started)
        {
            if (isReady == false)
            {
                return;
            }
            else
            {
                isReady = false;
                StartCoroutine(Cooldown());
            }
        }
    }

    IEnumerator Cooldown()        // cooldown pour Boost()
    {
        speed += speedBoost;
        yield return new WaitForSeconds(duration);
        speed -= speedBoost;
        yield return new WaitForSeconds(cooldown);
        isReady = true;
    }


}