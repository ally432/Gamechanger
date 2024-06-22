using UnityEngine;

public class trash : MonoBehaviour
{
    public AudioSource trashsound;
    void Start()
    {
        trashsound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Potionimg.trash){
            trashsound.Play();
            Potionimg.trash = false;
        }
    }
}
