using UnityEngine;

public class redgage : MonoBehaviour
{
    public AudioSource redg;
    void Start()
    {
        redg = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Dispenser.gageing && !redg.isPlaying){
            redg.Play();
        }
    }
}
