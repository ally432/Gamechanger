using UnityEngine;

public class contractSound : MonoBehaviour
{
    public AudioSource contract;

    void Start()
    {
        contract = GetComponent<AudioSource>();   
    }

    public void contractclick(){
        contract.Play();
    }
}
