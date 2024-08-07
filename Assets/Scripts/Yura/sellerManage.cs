using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;


public class sellerManage : MonoBehaviour
{
    int currentdate; // 현재 날짜. customerManage의 getDate 함수에서 얻어온다.

    public TypeEffect sellerText; // 약초꾼 텍스트 말풍선.
    public GameObject gamecursor;
    public TextMeshProUGUI dateText;

    public GameObject contractBtn, denyBtn, choice1,choice2,choice3,openBtn;
    public Button choiceBtn1, choiceBtn2, choiceBtn3;
    public TextMeshProUGUI choice1Text, choice2Text, choice3Text;

    public static List<string> herbList = new List<string>(); //현재 해금된 허브 리스트.

    List<bool> sellerList = new List<bool>(); // 진짜 약초꾼, 가짜 약초꾼 오는 순서 저장 리스트 (true,false로 구분)

    public Sprite[] sellerImgList = new Sprite[15]; // 약초꾼 이미지들
    public Sprite[] specialImgList = new Sprite[4]; // 특별손님 이미지들 
    public Image sellerImg;

    public Image day1HerbImg, day2HerbImg1, day2HerbImg2, day3HerbImg1, day3HerbImg2, day3HerbImg3, day4HerbImg1, day4HerbImg2, day4HerbImg3, day4HerbImg4,
        day5HerbImg1,day5HerbImg2,day5HerbImg3,day5HerbImg4,day5HerbImg5, day6HerbImg1, day6HerbImg2, day6HerbImg3, day6HerbImg4, day6HerbImg5, day6HerbImg6; //일차에 따른 약초들 배치 위치- 이미지들..
    public Sprite[] trueHerbImgList = new Sprite[12]; // 진짜 약초 이미지들
    public Sprite[] fakeHerbImgList = new Sprite[24]; // 가짜 약초 이미지들
    public Image herbimg;

    List<string> realHerbList = new List<string>(); //각 seller가 제시하는 진짜 허브들 저장되는 리스트.
    public static List<string> finalTrueHerbList = new List<string>(); // 제조 씬에 전달될, 최종 계약된 허브 리스트
    bool isContracted = false;//계약여부

    int reactionNum = 0; // 약초꾼 반응 계산 위한 변수.

    int sellerNum = 0; //지금까지 본 약초꾼 수

    bool nextperson = false;
    bool noContracted = false; //모든 약초꾼과 계약하지 않은 상황에 true.
    bool specialDone = false;
    bool specialSituation = false;
    public static int addMoney; //재계약시 추가금

    static public int excelnum;
    public int inum; // 특별손님수세기
    string kind;
    public string whatperson;
    bool isMouseClicked = false; //ㄱㅣㅅㅎㅓㅂㅡㅊㅔㅋㅡ

    bool choiceClicked = false;

    static public bool isChoiceDisplayed = false;
    /**
    public bool grandpa = false;

    public int herbGranpaNum = 0;
    **/

    public List<int> specialPersonList = new List<int>(); //특별손님 리스트

    static public int govermentLove = 0; // +++=저장필요 =+++
    static public int rebelLove = 0;
    static public bool rebelEvent = false;
    static public bool rebelEvent2 = false;

    public GameObject talkImage;

    public GameObject day1,day2,day3,day4,day5,day6;

    public GameObject upDesk;

    public GameObject herbImg;

    //엔딩 플래그드
    static public bool Bad1 = false;//bad1, 반동분자 플래그
    static public bool Bad2 = false;//bad2, 정부의 끄나풀 플래그

    public Image fadeInImg;

    public bool peopleOut = false; // 사람 들어가고 나오는거 판.

    public AudioSource click;   // 버튼 클릭

    public GameObject touchIcon1;
    public GameObject touchIcon2;
    public GameObject touchIcon3;


    void Start()
    {
        
        click = GetComponent<AudioSource>();

        sellerText = GameObject.Find("Talk").GetComponent<TypeEffect>();
        dateText = GameObject.Find("date").GetComponent<TextMeshProUGUI>();


        Debug.Log(currentdate);
        finalTrueHerbList.Clear(); //최종 허브 리스트 초기화 
        specialPersonList.Clear();
        inum = 0;

        touchIcon1.SetActive(true);
        touchIcon2.SetActive(false);
        touchIcon3.SetActive(false);
        herbImg.SetActive(true);
        day1.SetActive(false);
        day2.SetActive(false);
        day3.SetActive(false);
        day4.SetActive(false);
        day5.SetActive(false);
        day6.SetActive(false);
        talkImage.SetActive(false); // 말풍선 초기 비활성화
        openBtn.SetActive(false);
        contractBtn.SetActive(false);
        denyBtn.SetActive(false);
        choice1.SetActive(false);
        choice2.SetActive(false);
        choice3.SetActive(false);
        upDesk.SetActive(true);
        fadeInImg.enabled = false;
        

        day1HerbImg.enabled = false;
        day2HerbImg1.enabled = false;
        day2HerbImg2.enabled = false;
        day3HerbImg1.enabled = false;
        day3HerbImg2.enabled = false;
        day3HerbImg3.enabled = false;
        day4HerbImg1.enabled = false;
        day4HerbImg2.enabled = false;
        day4HerbImg3.enabled = false;
        day4HerbImg4.enabled = false;
        day5HerbImg1.enabled = false;
        day5HerbImg2.enabled = false;
        day5HerbImg3.enabled = false;
        day5HerbImg4.enabled = false;
        day5HerbImg5.enabled = false;
        day6HerbImg1.enabled = false;
        day6HerbImg2.enabled = false;
        day6HerbImg3.enabled = false;
        day6HerbImg4.enabled = false;
        day6HerbImg5.enabled = false;
        day6HerbImg6.enabled = false;


        addMoney = 0;

        

        currentdate = customerManage.getDate();
        dateText.text = currentdate.ToString();

        specialPerson(); // 특별손님리스트 추{

        for (int i = 0; i < specialPersonList.Count; i++)
        {
            Debug.Log(specialPersonList[i]);
        }

        if (specialPersonList.Count > 0) specialSituation = true;

        addHerb(); //현재 일차수에 따라 herblist에 해금된 허브 추가하기.
        fillSellerList();


        if (specialSituation)
        {
            specialCustomer();
        }
        else
        {
            Debug.Log("else로 셀러 시작해야됨");
            talkImage.SetActive(false);
            showImg("seller");
            openBtn.SetActive(true);

            
        }

    }

    // Update is called once per frame
    void Update()
    {
        gamecursor.SetActive(false);

        if (specialDone)
        {
            openBtn.SetActive(true);
            talkImage.SetActive(false);
            
            sellerImg.transform.DOLocalMoveY(-500, 1.0f).OnComplete(() =>
            {
                showImg("seller");
                
            });
             
            specialDone = false;
            

        }

        

        if (nextperson)
        {
            nextperson = false;
            reactionNum = 0;
            
            StartCoroutine(seller());
            

        }
    }

    IEnumerator FadeCo()
    {
        Debug.Log("코루틴 진입");
        fadeInImg.enabled = true;

        float fadeCount = 0;
        while(fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            fadeInImg.color = new Color(0, 0, 0, fadeCount);
        }

        SceneManager.LoadScene("Potionmaking");
    }

    void specialCustomer()
    {
        List<Dictionary<string, object>> special_Dialog = CSVReader.Read("specialPerson");
        excelnum = specialPersonList[inum];
        kind = special_Dialog[specialPersonList[inum]]["kind"].ToString();
        whatperson = special_Dialog[specialPersonList[inum]]["person"].ToString();
        Debug.Log("손님 바뀜" + excelnum + whatperson);
        showImg(kind);
        showSpecialTalk(whatperson);

    }

    void showSpecialTalk(string person)
    {
        List<Dictionary<string, object>> special_Dialog = CSVReader.Read("specialPerson");
        //talkImage.SetActive(true);


        if (person != special_Dialog[excelnum]["person"].ToString() || person == "none")
        {
            inum++;
            if (inum < specialPersonList.Count)
            {
                specialCustomer();
                return;
            }
            else
            {
                specialDone = true;
                specialSituation = false;
            }

        }

        Debug.Log("showspecialTalk" + excelnum);
        string content = special_Dialog[excelnum]["content"].ToString();
        Debug.Log(content);
        talkImage.SetActive(true);
        sellerText.SetMsg(content);

        if(excelnum == 8 || excelnum == 13)
        {
            if (excelnum == 8)
                touchIcon2.SetActive(true);
            else
                touchIcon3.SetActive(true);
        }
        

        if (special_Dialog[excelnum]["choice"].ToString() == "y")
        {
            choiceDisplay(excelnum);
        }
        excelnum++;
        isMouseClicked = false;
    }

    void choiceDisplay(int excelnum)
    {

        List<Dictionary<string, object>> special_Dialog = CSVReader.Read("specialPerson");

        Debug.Log(excelnum + "선택지 엑셀넘버");

        choice1.SetActive(true);
        choice1Text.text = special_Dialog[excelnum]["choice1"].ToString();

        choice2.SetActive(true);
        choice2Text.text = special_Dialog[excelnum]["choice2"].ToString();
        

        if(special_Dialog[excelnum]["choice3"].ToString() != "none")
        {
            choice3.SetActive(true);
            choice3Text.text = special_Dialog[excelnum]["choice3"].ToString();
            
        }

        isChoiceDisplayed = true;
       
    }

    public void talkNextBtn()
    {
        touchIcon1.SetActive(false);
        touchIcon3.SetActive(false);

        if ((specialSituation && !isMouseClicked) || choiceClicked)
        {
            if (!isChoiceDisplayed)
            {
                click.Play();
                Debug.Log("업데이트문" + whatperson + excelnum);
                isMouseClicked = true;
                choiceClicked = false;
                showSpecialTalk(whatperson);

                Debug.Log("다음 대사");

            }

        }
    }

    public void Btn1()
    {
        click.Play();

        if (excelnum == 40) //여긴 원래대로 
        {
            excelnum = 40; // 여긴 1 뺀 수로
            whatperson = "goArmy1-1";
            govermentLove -= 1;
        }
        else if (excelnum == 89)
        {
            excelnum = 89;
            whatperson = "rebel2-1-1";
            rebelLove += 1;
        }
        else if(excelnum == 99)
        {
            excelnum = 99;///
            whatperson = "rebel2-2-1";
            rebelLove += 1;
        }
        else if(excelnum == 120)
        {
            excelnum = 120;
            whatperson = "goArmy2-1";
            Bad1 = true;
        }
        
        BtnClear();
        talkNextBtn();

    }
    public void Btn2()
    {
        click.Play();
        if(excelnum == 3)
        {
            excelnum = 20;
            whatperson = "herbGranpaA-1";
        }

        if (excelnum == 40)
        {
            excelnum = 43;
            whatperson = "goArmy1-2";
            govermentLove += 1;
        }
        /*else if (excelnum == 67)
        {
            excelnum = 68;
            whatperson = "rebel1-2";
            rebelEvent = false;
            rebelLove -= 5;
        }*/
        else if (excelnum == 89)
        {
            excelnum = 91;
            whatperson = "rebel2-1-2";
            rebelLove -= 1;
        }
        else if(excelnum == 99)
        {
            excelnum = 100;
            whatperson = "rebel2-2-2";
            Bad2 = true;
        }
        else if(excelnum == 120)
        {
            excelnum = 123;
            whatperson = "goArmy2-2";
            govermentLove += 1;
        }

        
        BtnClear();
        talkNextBtn();


    }

    public void Btn3()
    {
        click.Play();
        
        if(excelnum == 120)
        {
            excelnum = 125;
            whatperson = "goArmy2-3";
        }
        
        BtnClear();
        talkNextBtn();

    }

    void BtnClear()
    {
        choice1.SetActive(false);
        choice2.SetActive(false);
        choice3.SetActive(false);
        isChoiceDisplayed=false;
        choiceClicked = true;
    }


    public void contractBtnClicked()
    {
        finalTrueHerbList.Clear();
        contractBtn.SetActive(false);
        denyBtn.SetActive(false);

        if (isContracted)
        {
            reactionNum = 15;
            showSellerTalk();
            addMoney += 5;
        }
        else
        {
            isContracted = true;

            reactionNum = 5;
            showSellerTalk();
        }

        if (realHerbList.Count > 0)
        {
            for (int i = 0; i < realHerbList.Count; i++) //realHerbList 내 finalTrueherblist에 복사.  
            {
                finalTrueHerbList.Add(realHerbList[i]);
            }
        }

        if(sellerNum == 10)
        {
            Debug.Log("손님 10");
            StartCoroutine(FadeCo());
        }

        nextperson = true;


    }


    public void denyBtnClicked()
    {
        if (sellerNum == 10)
        {
            if (!isContracted)
            { 
                noContracted = true;
            }
            Debug.Log("계약안함");
            StartCoroutine(FadeCo());   
        }
        contractBtn.SetActive(false);
        denyBtn.SetActive(false);
        reactionNum = 10;
        showSellerTalk();
        nextperson = true;
    }

    
    

    IEnumerator seller()
    {
        
        yield return new WaitForSeconds(3.0f);


        herbImg.transform.DOLocalMoveY(300, 1.0f).OnComplete(() =>
        {
            upDesk.SetActive(true);

            talkImage.SetActive(false);

            sellerImg.transform.DOLocalMoveY(-500, 1.0f).OnComplete(() =>
            {
                // 말풍선 unvisible


                Debug.Log("next people");
                showImg("seller");
                
                    
                    
                
            });
        });
    }

    void showImg(string person)
    {
        

        if (person == "seller")
        {
            int randomIndex = Random.Range(0, 15);
            sellerImg.sprite = sellerImgList[randomIndex];
            
        }


        else if (person == "granpa")
        {
            sellerImg.sprite = specialImgList[0];
        }
        else if (person == "army")
        {
            sellerImg.sprite = specialImgList[1];
        }
        else if (person == "rebel2")
        {
            sellerImg.sprite = specialImgList[2];
        }
        else if (person == "boy")
        {
            sellerImg.sprite = specialImgList[3];
        }

        sellerImg.transform.DOLocalMoveY(200, 2.0f).OnComplete(() => {
            talkImage.SetActive(true);
            if(person == "seller")
            {
                contractBtn.SetActive(true);
                denyBtn.SetActive(true);
                showSellerTalk();
                sellerHerb();
                showSellerHerb();
                Debug.Log("이제 데스크 투명된다");
                upDesk.SetActive(false);//////////
                herbImg.transform.DOLocalMoveY(-370, 0.5f);
            }
           
            

        });
    }



    void showSellerTalk()
    {
        talkImage.SetActive(true);
        List<Dictionary<string, object>> seller_Dialog = CSVReader.Read("seller");
        int ranNum = Random.Range(reactionNum, reactionNum + 5);
        string content = seller_Dialog[ranNum]["content"].ToString();
        sellerText.SetMsg(content);

    }

    void showSellerHerb()
    {
        if (currentdate <4)
        {
            day1.SetActive(true);
            day1HerbImg.enabled = true;
            day1HerbImg.sprite = herbImgDecide("waterBerry");
        }
        else if (currentdate <6)
        {
            day2.SetActive(true);
            day2HerbImg1.enabled = true;
            day2HerbImg2.enabled = true;
            day2HerbImg1.sprite = herbImgDecide("waterBerry");
            day2HerbImg2.sprite = herbImgDecide("silverFlame");
        }

        else if (currentdate < 8)
        {
            day3.SetActive(true);
            day3HerbImg1.enabled = true;
            day3HerbImg2.enabled = true;
            day3HerbImg3.enabled = true;
            day3HerbImg1.sprite = herbImgDecide("waterBerry");
            day3HerbImg2.sprite = herbImgDecide("silverFlame");
            day3HerbImg3.sprite = herbImgDecide("wadadakPepper");
        }

        else if (currentdate < 12)
        {
            day4.SetActive(true);
            day4HerbImg1.enabled = true;
            day4HerbImg2.enabled = true;
            day4HerbImg3.enabled = true;
            day4HerbImg4.enabled = true;
            day4HerbImg1.sprite = herbImgDecide("waterBerry");
            day4HerbImg2.sprite = herbImgDecide("silverFlame");
            day4HerbImg3.sprite = herbImgDecide("wadadakPepper");
            day4HerbImg4.sprite = herbImgDecide("shadowHerb");
        }
        else if (currentdate < 16)
        {
            day5.SetActive(true);
            day5HerbImg1.enabled = true;
            day5HerbImg2.enabled = true;
            day5HerbImg3.enabled = true;
            day5HerbImg4.enabled = true;
            day5HerbImg5.enabled = true;
            day5HerbImg1.sprite = herbImgDecide("waterBerry");
            day5HerbImg2.sprite = herbImgDecide("silverFlame");
            day5HerbImg3.sprite = herbImgDecide("wadadakPepper");
            day5HerbImg4.sprite = herbImgDecide("shadowHerb");
            day5HerbImg5.sprite = herbImgDecide("lightMush");
        }
        else
        {
            day6.SetActive(true);
            day6HerbImg1.enabled = true;
            day6HerbImg2.enabled = true;
            day6HerbImg3.enabled = true;
            day6HerbImg4.enabled = true;
            day6HerbImg5.enabled = true;
            day6HerbImg6.enabled = true;
            day6HerbImg1.sprite = herbImgDecide("waterBerry");
            day6HerbImg2.sprite = herbImgDecide("silverFlame");
            day6HerbImg3.sprite = herbImgDecide("wadadakPepper");
            day6HerbImg4.sprite = herbImgDecide("shadowHerb");
            day6HerbImg5.sprite = herbImgDecide("lightMush");
            day6HerbImg6.sprite = herbImgDecide("caveLotus");
        }

    }

    Sprite herbImgDecide(string herbName)
    {
        int ranNum;

        if (herbName == "waterBerry")
        {
            if (realHerbList.Contains("waterBerry"))
            {
                ranNum = Random.Range(0, 2);
                herbimg.sprite = trueHerbImgList[ranNum];
            }
            else
            {
                ranNum = Random.Range(0, 4);
                herbimg.sprite = fakeHerbImgList[ranNum];
            }
        }
        else if (herbName == "silverFlame")
        {
            if (realHerbList.Contains("silverFlame"))
                herbimg.sprite = trueHerbImgList[Random.Range(2, 4)];
            else
                herbimg.sprite = fakeHerbImgList[Random.Range(4, 8)];
        }
        else if (herbName == "wadadakPepper")
        {
            if (realHerbList.Contains("wadadakPepper"))
                herbimg.sprite = trueHerbImgList[Random.Range(4, 6)];
            else
                herbimg.sprite = fakeHerbImgList[Random.Range(8, 12)];
        }
        else if (herbName == "shadowHerb")
        {
            if (realHerbList.Contains("shadowHerb"))
                herbimg.sprite = trueHerbImgList[Random.Range(6, 8)];
            else
                herbimg.sprite = fakeHerbImgList[Random.Range(12, 16)];
        }
        else if (herbName == "lightMush")
        {
            if (realHerbList.Contains("lightMush"))
                herbimg.sprite = trueHerbImgList[Random.Range(8, 10)];
            else
                herbimg.sprite = fakeHerbImgList[Random.Range(16, 20)];
        }
        else if (herbName == "caveLotus")
        {
            if (realHerbList.Contains("caveLotus"))
                herbimg.sprite = trueHerbImgList[Random.Range(10, 12)];
            else
                herbimg.sprite = fakeHerbImgList[Random.Range(20, 24)];
        }

        return herbimg.sprite;

    }


    void addHerb()
    {
        if (currentdate == 1) herbList.Add("waterBerry"); // 나중에 그랜파가 와서 대사 말하고, 더하는걸로 변경.

        else if (currentdate == 4) herbList.Add("silverFlame");
            
        else if (currentdate == 6) herbList.Add("wadadakPepper");

        else if (currentdate == 8) herbList.Add("shadowHerb");

        else if (currentdate == 12) herbList.Add("lightMush");

        else if (currentdate == 16) herbList.Add("caveLotus");
    }

    void fillSellerList()
    {
        int sellerNum = 10;

        for (int i = 0; i < sellerNum; i++)
        {
            if (i < 2)
                sellerList.Add(true);
            else
                sellerList.Add(false);
        }
    }


    void sellerHerb() //한 약초꾼이 파는 각 약초들에 대한 진위 설정 함수.
    {
        List<string> sellerherbList = new List<string>(); //약초꾼의 허브 목록.    
        List<string> fakeHerbList = new List<string>();

        realHerbList.Clear();

        for (int i = 0; i < herbList.Count; i++) //herblist 내용 sellerherblist에 복사. 
        {
            sellerherbList.Add(herbList[i]);
        }

        int randSellerNum = Random.Range(0, sellerList.Count); //랜덤으로 진짜가짜상인 뽑기 

        bool sellerState = sellerList[randSellerNum];
        sellerList.RemoveAt(randSellerNum);

        if (sellerState) // 진짜 파는 상인일경우
        {
            for (int i = 0; i < sellerherbList.Count; i++) //진짜 허브 리스트에 현재 해금한 모든 허브 추가
            {
                realHerbList.Add(sellerherbList[i]);
            }
        }
        else if (!sellerState) // 가짜 파는 상인일경우 
        {
            int fakeHerbNum = Random.Range(1, herbList.Count + 1);

            for (int i = 0; i < fakeHerbNum; i++) // 랜덤으로 정한 가짜 허브 수만큼 반복   3
            {
                int randHerb = Random.Range(0, sellerherbList.Count); //랜덤으로 가짜허브종류 선택 
                fakeHerbList.Add(sellerherbList[randHerb]); //해당 허브 가짜허브리스트에 추가 
                sellerherbList.RemoveAt(randHerb); // 해당 허브 삭제 
            }

            if (sellerherbList != null) // 남은 허브종류 있을 경우 
            {
                for (int i = sellerherbList.Count - 1; i >= 0; i--) //진짜 허브 리스트에 추가. !!
                {
                    realHerbList.Add(sellerherbList[i]);
                    sellerherbList.RemoveAt(i);
                }

            }

        }

        sellerNum++;

        Debug.Log(sellerState);
        for (int i = 0; i < realHerbList.Count; i++)
            Debug.Log("진짜허브" + realHerbList[i]);
        for (int i = 0; i < fakeHerbList.Count; i++)
            Debug.Log("가짜허브리스트" + fakeHerbList[i]);


    }

    void specialPerson() //start때.
    {
        // 특정 상황에 따라 특별손님 추가 . 엑셀 번호로..? 
        if (currentdate == 1)
        {
            specialPersonList.Add(0);
        }
        else if (currentdate == 2) specialPersonList.Add(22);
        else if (currentdate == 3) specialPersonList.Add(32); // 약초할아버지는 항상 맨 마지막에 넣도록 하기.
        else if (currentdate == 4) specialPersonList.Add(46); // 뭔가 잘못. 
        else if (currentdate == 6) specialPersonList.Add(54);
        else if (currentdate == 8) specialPersonList.Add(71);
        else if (currentdate == 9)
        {
            if (rebelEvent) specialPersonList.Add(81);
            else specialPersonList.Add(93);
        }
        else if (currentdate == 10) specialPersonList.Add(102);
        else if (currentdate == 12)
        {
            specialPersonList.Add(111);
            specialPersonList.Add(117);
        }
        else if (currentdate == 14) specialPersonList.Add(126);
        
        else if (currentdate == 16) specialPersonList.Add(136);
        else if (currentdate == 17)
        {
            if (rebelEvent2) specialPersonList.Add(150);
            else specialPersonList.Add(152);
        }
        else if (currentdate == 19) specialPersonList.Add(160);




        else return;

    }



    public static List<string> getTrueContractHerbList()
    {
            return finalTrueHerbList;
    }

    public static List<string> getHerbList()
    {
        for(int i=0; i < herbList.Count; i++){
                Debug.Log("Yura"+herbList[i]);
        }
        
        return herbList;
    }

    public void openBtnFn()
    {
        StartCoroutine(FadeCo());
    }
    

    public void gopotion()
    {   // 다시 제조실로 갈 때 리스트 비우기
        if(Dragp.putgreds.Count > 0){
            Dragp.putgreds.Clear();
        }
        if(Dragp.putherb.Count > 0){
            Dragp.putherb.Clear();
        }
        if(Dragp.specialherb.Count > 0){
            Dragp.specialherb.Clear();
        }
        if(Potion.potionnum > -1){
            Potion.potionnum = 0;
        }

        SceneManager.LoadScene("Potionmaking");
    }


}
