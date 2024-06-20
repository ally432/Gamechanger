using UnityEngine;

public class gageb : MonoBehaviour
{
    public AudioSource gagebtn;
    void Start()
    {
        gagebtn = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Dispenser.gagedone){
            gagebtn.Play();
            Dispenser.gagedone = false;
        }
    }
}
