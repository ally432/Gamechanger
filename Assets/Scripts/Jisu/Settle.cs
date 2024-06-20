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
    public TextMeshProUGUI textDebt;


    
    public TextMeshProUGUI txtInterest2;
    public TextMeshProUGUI txtRent2;
    public TextMeshProUGUI txtFinalMoney2;
    public TextMeshProUGUI textDebt2;


    public static int endingNum = 0;
    int date = customerManage.getDate();
    int nowMoney = customerManage.getMoney();
    int interest = 10;
    int rent = 10;
    int debt = 5000;

    public static bool trueEnding = false;
    public AudioSource coin;

    // Start is called before the first frame update
    void Start()
    {
        
        txtInterest2.enabled = false;
        txtRent2.enabled = false;
        txtFinalMoney2.enabled = false;
        textDebt2.enabled = false;


        txtInterest.enabled = false;
        textDebt.enabled = false;
        txtRent.enabled = false;


        txtDate.text = "DAY " + date;
        txtNowMoney.text = nowMoney + " 원";
        int plus = (date/5) * 200;
        rent += plus;
        StartCoroutine(SettleMoney());
       
        coin = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SettleMoney()
    {
        yield return new WaitForSeconds(0.5f);

        txtInterest.enabled = true;
        txtInterest2.enabled = true;
        coin.Play();
        txtInterest.text = interest + " 원";
        StartCoroutine(RentMoney());

    }
    IEnumerator RentMoney()
    {
        yield return new WaitForSeconds(0.5f);
        txtRent2.enabled = true;
        txtRent.enabled = true;
        coin.Play();
        txtRent.text = rent + " 원";
        nowMoney -= (interest + rent);
        if (date == 17)
        {
            Invoke("DebtMoney", 1.0f);
        }
        Invoke("FinalMoney", 1.0f);

    }

    void FinalMoney()
    {
        txtFinalMoney2.enabled = true;
        coin.Play();
        txtFinalMoney.text = nowMoney + " 원";      
    }

    void DebtMoney()
    {
        textDebt2.enabled = true;
        textDebt.enabled = true;
        coin.Play();
        textDebt.text = debt + " 원";
        nowMoney -= 5000;
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
