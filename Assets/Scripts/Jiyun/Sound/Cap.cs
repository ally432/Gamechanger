using UnityEngine;

public class Cap : MonoBehaviour
{
    public AudioSource cap;
    void Start()
    {
        cap = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Potionimg.isCap){
            cap.Play();
            Potionimg.isCap = false;
        }
    }
}
