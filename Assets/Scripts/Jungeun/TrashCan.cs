using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("cap")) 
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("bottle"))
        {
            Destroy(other.gameObject);
        }
    }
}
