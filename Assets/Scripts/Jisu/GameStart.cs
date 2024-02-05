using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;
using GooglePlayGames.BasicApi;

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
        // 저장한 데이터 가져오기
        if(PlayerPrefs.HasKey("SavedDate")) // Date로 저장된 값이 있다면
        {
            // 날짜, 소지금 가져오기
            int savedDate = PlayerPrefs.GetInt("SavedDate");
            int savedMoney = PlayerPrefs.GetInt("SavedMoney");

            // 물약 해금 상황 가져오기
            string[] dataArr = PlayerPrefs.GetString("SavedPotionList").Split(',');
            List<int> savedPotionList = new List<int>();
            for(int i = 0; i < dataArr.Length; i++)
            {
                savedPotionList[i] = System.Convert.ToInt32(dataArr[i]);
            }

            // 호감도 가져오기
            // int gFavor = PlayerPrefs.GetInt("SavedGFavor");
            // int rFavor = PlayerPrefs.GetInt("SavedRFavor");

            // 플래그 가져오기
            // bool flag1 = System.Convert.ToBoolean(PlayerPrefs.GetInt("SavedFlag1"));
            // ...

            // 저장된 정보 불러오기
            customerManage.date = savedDate;
            customerManage.money = savedMoney;
            Potion.plist = savedPotionList.ToList();

            SceneManager.LoadScene("morningScene");
        }
        else    // 저장된 기록이 없을 경우
        {
            conStartResult.text = "저장 기록이 없습니다.";
        }
    }
}
