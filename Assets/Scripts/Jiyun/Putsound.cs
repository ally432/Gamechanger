using UnityEngine;

public class Putsound : MonoBehaviour
{
    public AudioSource sound;   // 재료 넣는 소리

    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Dragp.putsound){
            sound.Play();
        }
        Dragp.putsound = false;
    }
}
