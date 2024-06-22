using UnityEngine;

public class Dispenserin : MonoBehaviour
{
    public AudioSource disin;
    void Start()
    {
        disin = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Potion.gagestart){
            disin.Play();
            Potion.gagestart = false;
        }        
    }
}
