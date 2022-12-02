using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public void DeathSound()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
    }
}
