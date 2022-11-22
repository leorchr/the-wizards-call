using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerController : MonoBehaviour
{
    [Header("Player Statistics")]
    public int damage;
    public float speed;

    [Header("SpeedBoost Competence")]
    [SerializeField] private int speedBoost;
    [SerializeField] private float duration;
    [SerializeField] private float cooldown;
    [SerializeField] private bool isReady = true;

    [Header("Objects & Components")]
    public Rigidbody2D RB;
    public GameObject Pivot;
    public GameObject Rotation;
    public GameObject Stick;
    private float angle;                                // variable pour definir l'angle de vue du joueur en fonction de la position de la souris ou du joystick


    public void LookMouse(InputAction.CallbackContext context)
    {
        Vector2 mouse = context.ReadValue<Vector2>();
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        Vector2 offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
        angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg + 90;
        Rotation.transform.rotation = Quaternion.Euler(0f, 0f, angle);

    }

    public void LookJoystick(InputAction.CallbackContext context)
    {
        Vector2 joystick = context.ReadValue<Vector2>();
        angle = Mathf.Atan2(joystick.y, joystick.x) * Mathf.Rad2Deg;
        if (angle != 0)
        {
            Rotation.transform.rotation = Quaternion.Euler(0f, 0f, angle + 90);
        }

    }

    public void Movement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        inputMovement.Normalize();
        RB.velocity = new Vector2(speed * inputMovement.x, speed * inputMovement.y);
    }

    public void Attack(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            if (Time.timeScale == 0) return;

            Pivot.GetComponent<Animator>().SetTrigger("Attaque");
        }
    }

    public void Boost(InputAction.CallbackContext value)
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