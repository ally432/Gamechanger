using UnityEngine;

public class Clicksound : MonoBehaviour
{
    public AudioSource click;

    void Start()
    {
        click = GetComponent<AudioSource>();   
    }

    public void btnclick(){
        click.Play();
    }
}
