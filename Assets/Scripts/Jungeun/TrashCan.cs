using System.Collections;
using System.Collections.Generic;
using System.IO;
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
            other.transform.position = capSpawnLocation.position;
        }
        if (other.gameObject.CompareTag("bottle"))
        {
            other.transform.position = bottleSpawnLocation.position;
        }

        if (other.gameObject.CompareTag("one"))
        {
            Destroy(other.gameObject);
            Debug.Log("Destroying 'one' and creating 'cap' and 'bottle'...");
            GameObject newCap = Instantiate(capPrefab, capSpawnLocation.position, Quaternion.identity);
            GameObject newBottle = Instantiate(bottlePrefab, bottleSpawnLocation.position, Quaternion.identity);
            Debug.Log("'cap' and 'bottle' created.");

            // Make sure newCap and newBottle are visible
            newCap.SetActive(true);
            newBottle.SetActive(true);
        }
    }
/*    public string csvFileName;  // CSV 파일 이름
    void Start()
    {
        ResetCSVFile(csvFileName);
    }

    void ResetCSVFile(string fileName)
    {
        string filePath = Path.Combine(Application.dataPath, fileName);

        using (var writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Drug grade,Drug type");  // CSV 파일의 헤더
            // 이 시점에서 CSV 파일은 헤더만 남고 모든 데이터가 초기화됩니다.
        }
    }
*/
}
