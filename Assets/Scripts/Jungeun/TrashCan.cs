using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    public GameObject capPrefab; // Cap prefab
    public GameObject bottlePrefab; // Bottle prefab
    public Transform capSpawnLocation; // Cap spawn location
    public Transform bottleSpawnLocation; // Bottle spawn location

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D called with " + other.gameObject.tag);

        if (other.gameObject.CompareTag("cap"))
        {
            Destroy(other.gameObject);
            Instantiate(capPrefab, capSpawnLocation.position, Quaternion.identity);
        }
        if (other.gameObject.CompareTag("bottle"))
        {
            Destroy(other.gameObject);
            Instantiate(bottlePrefab, bottleSpawnLocation.position, Quaternion.identity);
        }
        if (other.gameObject.CompareTag("one"))
        {
            Destroy(other.gameObject);
            Debug.Log("Destroying 'one' and creating 'cap' and 'bottle'...");
            Instantiate(capPrefab, capSpawnLocation.position, Quaternion.identity);
            Instantiate(bottlePrefab, bottleSpawnLocation.position, Quaternion.identity);
            Debug.Log("'cap' and 'bottle' created.");
        }

    }

}
