using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nightManage : MonoBehaviour
{
    public static int currentDate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextBtn()
    {
        if(customerManage.money < 0)        // 파산 엔딩
        {
            PlayerPrefs.DeleteAll();
            // SceneManager.LoadScene("BadEnding1");
        }
        else
        {
            customerManage.dateIncrese();
            SceneManager.LoadScene("SaveScene");
        }
    }
}
