using UnityEngine;

public class openSound : MonoBehaviour
{
    public AudioSource open;

    void Start()
    {
        open = GetComponent<AudioSource>();   
    }

    public void openclick(){
        open.Play();
    }
}
