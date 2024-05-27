using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boilsound : MonoBehaviour
{
    public AudioSource sound;

    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Potion.boil){
            sound.Play();
        }
        Potion.boil = false;
    }
}
