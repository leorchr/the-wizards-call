using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public GameObject stick;

    private void Start()
    {
        stick.SetActive(false);
    }

    public void BeginAnimation()
    {
        stick.SetActive(true);
    }

    public void EndAnimation()
    {
        stick.SetActive(false);
    }
}
