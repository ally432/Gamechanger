using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SettleScript : MonoBehaviour
{
    public TextMeshProUGUI txtDate;
    public TextMeshProUGUI txtNowMoney;
    public TextMeshProUGUI txtInterest;
    public TextMeshProUGUI txtRent;
    public TextMeshProUGUI txtFinalMoney;

    public static int endingNum = 0;
    int date = customerManage.getDate();
    int nowMoney = customerManage.getMoney();
    int interest = 10;
    int rent = 10;

    public static bool trueEnding = false;

    // Start is called before the first frame update
    void Start()
    {
        txtDate.text = "DAY " + date;
        txtNowMoney.text = nowMoney + " 원";
        int plus = (date/5) * 200;
        rent += plus;
        Invoke("SettleMoney", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SettleMoney()
    {
        txtInterest.text = interest + " 원";
        txtRent.text = rent + " 원";
        nowMoney -= (interest + rent);
        Invoke("FinalMoney", 0.5f);
    }

    void FinalMoney()
    {
        txtFinalMoney.text = nowMoney + " 원";      
    }

    public void NextScene()
    {
        customerManage.money = nowMoney;

        if (sellerManage.Bad1) {
            SceneManager.LoadScene("endingScene");
            endingNum = 2;
        }

        if (sellerManage.Bad2) {
            SceneManager.LoadScene("endingScene");
            endingNum = 3;
        }

        if (customerManage.money >= 0)
        {
            customerManage.dateIncrese();
            SceneManager.LoadScene("SaveScene");
        }
        else        // 파산 엔딩
        {
            endingNum = 5;
            SceneManager.LoadScene("endingScene");
        }


        if (date == 17)
        {
            if(customerManage.money <= 5000) // 사채업자가 요구한 돈 갚지 못하면
            {
                endingNum = 4;
                SceneManager.LoadScene("endingScene"); //끝
            }
            else
            {
                customerManage.dateIncrese();
                SceneManager.LoadScene("SaveScene");
            }
        }

        else if (date == 20)
        {
            if (sellerManage.govermentLove == sellerManage.rebelLove)
            {
                endingNum = 1;
            }
            else if (sellerManage.govermentLove >= 2 && sellerManage.govermentLove > sellerManage.rebelLove) 
            {
                endingNum = 6;
            }
            else if (sellerManage.rebelLove>=2 && sellerManage.rebelLove > sellerManage.govermentLove)
            {
                endingNum = 7;
            }
            else if (sellerManage.rebelLove == 3 && sellerManage.govermentLove == 3)
            {
                endingNum = 8;
            }
            SceneManager.LoadScene("endingScene");
            
        }
    }
}
