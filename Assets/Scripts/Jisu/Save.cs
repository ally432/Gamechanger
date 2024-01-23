using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        txtSaveResult.text = "저장 중 ...";
        Saving();
    }

    public void Saving()        // 날짜, 소지금, 물약 도감, 호감도, 플래그(분기점)
    {
        PlayerPrefs.SetInt("SavedDate", customerManage.date);
        PlayerPrefs.SetInt("SavedMoney", customerManage.money);
        Invoke("Saved", 1.0f);
    }

    void Saved()
    {
        txtSaveResult.text = "저장 완료!";
        Invoke("Next", 0.5f);
    }

    void Next()
    {
        SceneManager.LoadScene("morningScene");
    }
}
