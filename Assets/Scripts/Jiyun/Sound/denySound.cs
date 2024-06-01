using UnityEngine;

public class denySound : MonoBehaviour
{
    public AudioSource deny;

    void Start()
    {
        deny = GetComponent<AudioSource>();   
    }

    public void denyclick(){
        deny.Play();
    }
}
