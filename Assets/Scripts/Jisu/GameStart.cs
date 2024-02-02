using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameStart : MonoBehaviour
{
    public TextMeshProUGUI conStartResult;
    public GameObject title;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void NewStart()
    {
        PlayerPrefs.DeleteAll();
        customerManage.date = 1;
        customerManage.money = 1000;
        SceneManager.LoadScene("morningScene");
        // 튜토리얼 씬 만들면 그쪽으로 이동
    }

    public void ConStart()
    {
        int savedDate = 0;
        int savedMoney = 0;

        // 저장한 데이터 가져오기
        if(PlayerPrefs.HasKey("SavedDate")) // Date로 저장된 값이 있다면
        {
            savedDate = PlayerPrefs.GetInt("SavedDate");
            savedMoney = PlayerPrefs.GetInt("SavedMoney");
        }

        /*
        물약 해금 상황
        호감도
        플래그(분기점)
        */

        if(savedDate == 0){     // 저장된 기록이 없을 경우
            conStartResult.text = "저장 기록이 없습니다.";
        }
        else{                   // 저장된 기록이 있을 경우
            customerManage.date = savedDate;
            customerManage.money = savedMoney;
            SceneManager.LoadScene("morningScene");
        }
    }
}
