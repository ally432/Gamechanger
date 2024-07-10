using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using DG.Tweening;

public class customerManage : MonoBehaviour
{
    public static int date = 1;
    public static int maxindex = 0;
    public TypeEffect customerText;
    public GameObject gamecursor;

    public Image customerImg;
    public Sprite[] customerImgList = new Sprite[25];
    public static int customerNum = 0; // 손님 식별번(엑셀로 기타 요구조건들 확인가능)
    public static int money = 0;
    public TextMeshProUGUI moneyText;
    public List<string> herbList = new List<string>(); // 계약한 진짜 약초들 리스트 (sellerManage에서 전달받음 )
    public string potionGrade; //제조 완료 시 결정된 포션등급

    public List<int> customerList = new List<int>(); //손님들 중복방지 과거 손님 확인리스트

    int ranNum;

    public int specialPeopleNum;
    public Sprite[] specialImgList = new Sprite[4];

    private bool timeover = false;

    [SerializeField] private TMP_Text timeText;
    [SerializeField] private float time;
    [SerializeField] private float curTime;

    int minute;
    int second;

    public static bool isfirst = false; // 제조실에 갔다왔는지

    public Canvas customer, making; // 손님오는 캔버스, 만드는 캔버스

    public string person;

    public bool loadScene = false;
    bool isMouseClicked = false;
    bool choiceClicked = false;
    public GameObject choice1, choice2, choice3,moveBtn,nextBtn;
    public TextMeshProUGUI choice1Text, choice2Text, choice3Text;

    bool isChoiceDisplayed = false;
    bool specialcheck = true;
    bool special = false;
    bool moving = false;
    public static bool crush = false;   // 손님과 부딪쳤을때

    public GameObject talkImage;

    public AudioSource click;

    // Start is called before the first frame update
    void Start()
    {
        click = GetComponent<AudioSource>();

        customerText = GameObject.Find("Talk").GetComponent<TypeEffect>();
        moneyText = GameObject.Find("txt_Money").GetComponent<TextMeshProUGUI>();

        choice1.SetActive(false);
        choice2.SetActive(false);
        choice3.SetActive(false);
        moveBtn.SetActive(true);
        nextBtn.SetActive(false);
        talkImage.SetActive(false);

        Debug.Log("현재 날짜"+date);
        herbList = sellerManage.getTrueContractHerbList();
        if (herbList.Count > 0)
        {
            for (int i = 0; i < herbList.Count; i++)
                Debug.Log(herbList[i]);
        }
        

        
        maxcheck(date);
        customerPick();
        showCustomerImg(Random.Range(0, 3));
        

          

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
            if (specialcheck)
            {
                specialPeople();
                specialcheck = false;
            }
    
            

            if ((Input.GetMouseButtonDown(0) && !isMouseClicked) || choiceClicked)
            {
                if (!isChoiceDisplayed)
                {
                    
                    isMouseClicked = true;
                    choiceClicked = false;
                    Debug.Log("다음 대사 진입");
                    printSpecialScript(person);
                }
                
            }

            if (loadScene) StartCoroutine(MoveScene());


        }

        if(crush){
            customerBtn();
        }


    }

    public void next()
    {
        SceneManager.LoadScene("nightScene");
    }
    private void Awake()
    {
        time = 60;  // 잠깐 바꿈
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


    IEnumerator MoveScene()
    {
        yield return new WaitForSeconds(3.0f);

        SceneManager.LoadScene("nightScene");
    }

    void showCustomerImg(int num)
    {
        if (special)
        {
            customerImg.sprite = specialImgList[num];
        }
        else
            customerImg.sprite = customerImgList[num];
        customerImg.transform.DOLocalMoveY(80, 0.5f).OnComplete(() => {
            talkImage.SetActive(true);
            printOrderScript();
        });


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
        //crush = false;
    }

    IEnumerator nextCustomer()
    {
        yield return new WaitForSeconds(2.0f);

        talkImage.SetActive(false);
        customerImg.transform.DOLocalMoveY(-500, 0.5f).OnComplete(() =>
        {

        showCustomerImg(Random.Range(0, 3));
        customerPick();
        printOrderScript();
        });
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

    public void printSpecialScript(string person) // 스페셜손님 말씀 하십니다.
    {
        Debug.Log(person + " 스페셜 스크립트 함수 진입");
        List<Dictionary<string, object>> special_Dialog = CSVReader.Read("specialPerson");
        if (person != special_Dialog[specialPeopleNum]["person"].ToString() || person == "none")
        {
            Debug.Log(special_Dialog[specialPeopleNum]["person"].ToString());

            Debug.Log("사람과 스페셜넘 다름");
            
            return;
        }
        string content = special_Dialog[specialPeopleNum]["content"].ToString();
        customerText.SetMsg(content);

        if (special_Dialog[specialPeopleNum]["choice"].ToString() == "y")
        {
            choiceDisplay(specialPeopleNum);
        }

        isMouseClicked = false;
        specialPeopleNum++;
    }

    void choiceDisplay(int specialPeopleNum)
    {

        List<Dictionary<string, object>> special_Dialog = CSVReader.Read("specialPerson");

        Debug.Log(specialPeopleNum + "선택지 엑셀넘버");

        choice1Text.text = special_Dialog[specialPeopleNum]["choice1"].ToString();
        choice1.SetActive(true);

        choice2Text.text = special_Dialog[specialPeopleNum]["choice2"].ToString();
        choice2.SetActive(true);

        if (special_Dialog[specialPeopleNum]["choice3"].ToString() != "none")
        {
            choice3Text.text = special_Dialog[specialPeopleNum]["choice3"].ToString();
            choice3.SetActive(true);
        }

        isChoiceDisplayed = true;

    }

    public void Btn1()
    {
        click.Play();
        if (specialPeopleNum == 67)
        {
            
            //제조실 이동 버튼 넣어야됨.
            specialPeopleNum = 67;
            person = "rebel1-1";
            moving = true;
            moveBtn.SetActive(true);
        }
        else if (specialPeopleNum == 135)
        {
            
            
            moving = true;
            moveBtn.SetActive(true);
            //제조실 이동
        }
        else if (specialPeopleNum == 165)
        {
            
            moving = true;
            moveBtn.SetActive(true);

        }
        BtnClear();
        
    }
    public void Btn2()
    {
        click.Play();
        if (specialPeopleNum == 67)
        {
            specialPeopleNum = 68;
            person = "rebel1-2";
            sellerManage.rebelEvent = false;
            sellerManage.rebelLove -= 1;
        }
        else if (specialPeopleNum == 135)
        {
            specialPeopleNum = 135;
            person = "rebel3-2";
            sellerManage.rebelEvent2 = true;
            sellerManage.rebelLove -= 1;
        }
        else if (specialPeopleNum == 165)
        {
            specialPeopleNum = 166;
            person = "goArmy3-2";
            sellerManage.govermentLove -= 1;
            //
        }
        BtnClear();
        loadScene = true;

    }
    public void Btn3()
    {
        click.Play();
        if (specialPeopleNum == 135)
        {
            specialPeopleNum = 135;
            person = "rebel3-2";
            sellerManage.rebelEvent2 = true;
            sellerManage.rebelLove -= 1;
        }
        BtnClear();
        loadScene = true;
    }

    void BtnClear()
    {
        choice1.SetActive(false);
        choice2.SetActive(false);
        choice3.SetActive(false);
        isChoiceDisplayed = false;
        choiceClicked = true;
    }

    public void potionGradeCheck()
    {
        List<Dictionary<string, object>> customerOrder_Dialog = CSVReader.Read("customerOrder");
        List<Dictionary<string, object>> customerReaction_Dialog = CSVReader.Read("customerReaction");
        string reaction;
        string strMoney;
        int ranNumRe = Random.Range(0, 5);

        potionGrade = Potionimg.finalPotionGrade();

        if (special)
        {
            if(potionGrade == "A" || potionGrade == "B" || potionGrade == "C")
            {
                if (customerNum == 159)
                {
                    customerText.SetMsg("정말 고마워요!");
                    sellerManage.rebelLove += 1;
                    sellerManage.rebelEvent = true;
                    
                }
                else if (customerNum == 160)
                {
                    customerText.SetMsg("아주 좋아!");
                    sellerManage.rebelLove += 1;
                    sellerManage.rebelEvent2 = false;

                }
                else if (customerNum == 161)
                {
                    customerText.SetMsg("협조에 감사드립니다.");
                    sellerManage.govermentLove += 1;
                }

            }
            else
            {
                if (customerNum == 159)
                {
                    customerText.SetMsg("이거 치유물약 맞아?!");
                    sellerManage.rebelLove -= 1;
                    sellerManage.rebelEvent = false;
                }
                else if (customerNum == 160)
                {
                    customerText.SetMsg("날 놀리는건가?");
                    sellerManage.rebelLove -= 1;
                    sellerManage.rebelEvent2 = true;
                }
                else if (customerNum == 161)
                {
                    customerText.SetMsg("이런 엉터리 물약을 주다니.");
                    sellerManage.govermentLove -= 1;
                }

            }

            Potion.differentPotion = false;
            Potion.failPotion = false;
            special = false;
            moving = false;
            StartCoroutine(MoveScene());
            return;
        }

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
        if (Potion.differentPotion) { reaction = "내가 원한 물약은 아니야..."; }
        if (Potion.failPotion) { reaction = "내가 주문한 대로는 만들어 주지 않았네."; }
        
        customerText.SetMsg(reaction);
        Potion.differentPotion = false;
        Potion.failPotion = false;

    }

    void specialPeople()
    {
        List<Dictionary<string, object>> special_Dialog = CSVReader.Read("specialPerson");

        if (date == 6)//6
        {
            specialPeopleNum = 63;
            person = special_Dialog[specialPeopleNum]["person"].ToString();
            customerNum = 1;
            special = true;
            showCustomerImg(0);
            printSpecialScript(person);
        }
        else if (date == 15)//15
        {
            specialPeopleNum = 131;
            person = special_Dialog[specialPeopleNum]["person"].ToString();
            customerNum = 160;
            special = true;
            showCustomerImg(1);
            printSpecialScript(person);
        }
        else if (date == 17)//17
        {
            specialPeopleNum = 154;
            person = special_Dialog[specialPeopleNum]["person"].ToString();
            showCustomerImg(2);
            printSpecialScript(person);
        }
        else if (date == 19)//19
        {
            special = true;
            specialPeopleNum = 161;
            person = special_Dialog[specialPeopleNum]["person"].ToString();
            customerNum = 161;
            showCustomerImg(3);
            printSpecialScript(person);
        }
        else loadScene = true;

    } //

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
    public void potionmaking(){ // 제조실로 가는 버튼
        click.Play();

        making.gameObject.SetActive(!making.gameObject.activeSelf);
        customer.gameObject.SetActive(!customer.gameObject.activeSelf);

        // 리스트 초기화
        Dragp.putgreds.Clear(); // 넣은 모든 재료들
        Dragp.putherb.Clear();  // 넣은 허브
        Dragp.specialherb.Clear();  // 넣은 허브와 특별 부재료
        Potion.potionnum = 0;   // 만든 포션의 점수
        Potion.potionname = null;   // 만든 포션의 이름
        Controller.remake = true;   // 화력, 시간 원위치로

        Potion.push = false;
    }
}


