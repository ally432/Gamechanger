using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Save : MonoBehaviour
{
    public TextMeshProUGUI txtDate;
    public TextMeshProUGUI txtSaveResult;

    // Start is called before the first frame update
    void Start()
    {
        int date = customerManage.date - 1;
        txtDate.text = "DAY " + date;
        txtSaveResult.text = "";
        Invoke("Nextday", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Nextday()
    {
        txtDate.text = "DAY " + customerManage.date;
        Invoke("Saving", 0.5f);
    }

    void Saving()
    {
        txtSaveResult.text = "저장 중 ...";
        Invoke("Saved", 1.0f);
    }

    void Saved()
    {
        txtSaveResult.text = "저장 완료!";
        Invoke("Next", 0.3f);
    }

    void Next()
    {
        SceneManager.LoadScene("morningScene");
    }
}
