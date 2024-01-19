using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
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
        customerManage.date = 1;
        SceneManager.LoadScene("morningScene");
        // 튜토리얼 씬 만들면 그쪽으로 이동
    }

    public void ConStart()
    {
        // 저장한 데이터 가져오기
        /*
        customerManage.date = savedDate + 1;
        customerManage.money = savedMoney;
        물약 해금 상황
        호감도
        플래그(분기점)
        */
        SceneManager.LoadScene("morningScene");
    }
}
