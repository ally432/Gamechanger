using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potiond : MonoBehaviour
{
    public AudioSource potiondone;
    void Start()
    {
        potiondone = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Potionimg.potiond){
            potiondone.Play();
            Potionimg.potiond = false;
        }   
    }
}
