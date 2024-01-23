using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettleScript : MonoBehaviour
{
    public TextMeshProUGUI txtDate;
    public TextMeshProUGUI txtNowMoney;
    public TextMeshProUGUI txtInterest;
    public TextMeshProUGUI txtRent;
    public TextMeshProUGUI txtFinalMoney;

    int date = customerManage.getDate();
    int nowMoney = customerManage.getMoney();
    int interest = 500;
    int rent = 200;

    // Start is called before the first frame update
    void Start()
    {
        txtDate.text = "DAY " + date;
        txtNowMoney.text = nowMoney + " 원";
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
    }
}
