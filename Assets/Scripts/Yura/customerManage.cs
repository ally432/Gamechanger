using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class customerManage : MonoBehaviour
{
    public static int date = 1;
    public static int maxindex = 0;
    public TypeEffect customerText;
    public GameObject gamecursor;

    public Image customerImg;
    public Sprite[] customerImgList = new Sprite[3];
    public static int customerNum = 0; // 손님 식별번(엑셀로 기타 요구조건들 확인가능)
    public static int money = 0;
    public TextMeshProUGUI moneyText;
    public List<string> herbList = new List<string>(); // 계약한 진짜 약초들 리스트 (sellerManage에서 전달받음 )
    public string potionGrade = "C"; //제조 완료 시 결정된 포션등급

    public List<int> customerList = new List<int>(); //손님들 중복방지 과거 손님 확인리스트

    int ranNum;

    int specialPeopleNum;

    private bool timeover = false;

    [SerializeField] private TMP_Text timeText;
    [SerializeField] private float time;
    [SerializeField] private float curTime;

    int minute;
    int second;

    public static bool isfirst = false; // 제조실에 갔다왔는지

    public Canvas customer, making; // 손님오는 캔버스, 만드는 캔버스

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("현재 날짜"+date);
        herbList = sellerManage.getTrueContractHerbList();
        if (herbList.Count > 0)
        {
            for (int i = 0; i < herbList.Count; i++)
                Debug.Log(herbList[i]);
        }
        customerText = GameObject.Find("Talk").GetComponent<TypeEffect>();
        moneyText = GameObject.Find("txt_Money").GetComponent<TextMeshProUGUI>();

        maxcheck(date);
        customerPick();
        showCustomerImg();
        printOrderScript();

        /*bottle.SetActive(false);
        cap.SetActive(false);
        if (isfirst){   // 제조실에 갔다왔다면
            bottle.SetActive(true);
            cap.SetActive(true);
        } */       

        making.gameObject.SetActive(false);
        customer.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        gamecursor.SetActive(false);
        moneyText.text = money.ToString();

        if (timeover)
        {
            Debug.Log("저녁씬 로드");
            //if special 손님 없으면
            SceneManager.LoadScene("nightScene");
        }


    }

    private void Awake()
    {
        time = 10;
        StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        curTime = time;
        while (curTime > 0)
        {
            curTime -= Time.deltaTime;
            minute = (int)curTime / 60;
            second = (int)curTime % 60;
            timeText.text = minute.ToString("00") + ":" + second.ToString("00");
            yield return null;

            if(curTime <= 0)
            {
                timeover = true;
                curTime = 0;
                yield break;
            }
        }
    }


    void showCustomerImg()
    {
        int randomIndex = Random.Range(0, 3);
        customerImg.sprite = customerImgList[randomIndex];
    }


    void maxcheck(int date)
    {
        if (date == 1)
            maxindex += 10;
        else if (date == 2)
            maxindex += 10;
        else if (date == 4)
            maxindex += 10;
        else if (date == 6)
            maxindex += 20;
        else if (date == 8)
            maxindex += 20;
        else if (date == 10)
            maxindex += 10;
        else if (date == 12)
            maxindex += 20;
        else if (date == 14)
            maxindex += 10;
        else if (date == 16)
            maxindex += 50;
        
        Debug.Log(maxindex);
    }

    public void customerBtn() //물약을 주면 등급에 맞는 반응, 손님 가고 다음손님옴. 나중에 물약 드래그되면 실행되는 함수로 변경하기
    {
        
        potionGradeCheck();
        StartCoroutine(nextCustomer());
        
    }

    IEnumerator nextCustomer()
    {
            yield return new WaitForSeconds(2.0f);
        showCustomerImg();
        customerPick();
        printOrderScript();
    }

    public void customerPick()
    {
        ranNum = Random.Range(0, maxindex);
       
        for(int i = 0; i< customerList.Count; i++)
        {
            if (customerList[i] == ranNum)
            {
                customerPick();
                return;
            }                
        }

        customerList.Add(ranNum);
        customerNum = ranNum;

    }

    public void printOrderScript()
    {
        List<Dictionary<string, object>> customerOrder_Dialog = CSVReader.Read("customerOrder");
        string content = customerOrder_Dialog[customerNum]["content"].ToString();
        customerText.SetMsg(content);

    }

    public void potionGradeCheck()
    {
        List<Dictionary<string, object>> customerOrder_Dialog = CSVReader.Read("customerOrder");
        List<Dictionary<string, object>> customerReaction_Dialog = CSVReader.Read("customerReaction");
        string reaction;
        string strMoney;
        int ranNumRe = Random.Range(0, 5);

        switch (potionGrade)
        {
            case "A":
                strMoney = customerOrder_Dialog[customerNum]["a"].ToString();
                money += int.Parse(strMoney);
                break;
            case "B":
                strMoney = customerOrder_Dialog[customerNum]["b"].ToString();
                money += int.Parse(strMoney);
                ranNumRe += 5;
                break;
            case "C":
                strMoney = customerOrder_Dialog[customerNum]["c"].ToString();
                money += int.Parse(strMoney);
                ranNumRe += 10;
                break;
            case "D":
                strMoney = customerOrder_Dialog[customerNum]["d"].ToString();
                money += int.Parse(strMoney);
                ranNumRe += 15;
                break;
            case "F":
                strMoney = customerOrder_Dialog[customerNum]["f"].ToString();
                money += int.Parse(strMoney);
                ranNumRe += 20;
                break;
        }

        reaction = customerReaction_Dialog[ranNumRe]["content"].ToString();
        customerText.SetMsg(reaction);

    }

    void specialPeople()
    {
        if (date == 2) specialPeopleNum =10 ; } //

    public static int getDate()
    {
        return date;
    }

    public static void dateIncrese()
    {
        date++;
    }

    public static int getMoney()
    {
        return money;
    }
    public void potionmaking(){
        making.gameObject.SetActive(!making.gameObject.activeSelf);
        customer.gameObject.SetActive(!customer.gameObject.activeSelf);
    }
}


