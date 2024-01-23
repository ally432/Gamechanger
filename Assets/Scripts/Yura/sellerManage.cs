using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class sellerManage : MonoBehaviour
{
    int currentdate; // 현재 날짜. customerManage의 getDate 함수에서 얻어온다.

    public TypeEffect sellerText; // 약초꾼 텍스트 말풍선.
    public GameObject gamecursor;

    public static List<string> herbList = new List<string>(); //현재 해금된 허브 리스트.

    List<bool> sellerList = new List<bool>(); // 진짜 약초꾼, 가짜 약초꾼 오는 순서 저장 리스트 (true,false로 구분)

    public Sprite[] sellerImgList = new Sprite[5]; // 약초꾼 이미지들 
    public Image sellerImg;

    public Image day1HerbImg, day2HerbImg1, day2HerbImg2, day3HerbImg1, day3HerbImg2, day3HerbImg3; //일차에 따른 약초들 배치 위치- 이미지들..
    public Sprite[] trueHerbImgList = new Sprite[6]; // 진짜 약초 이미지들
    public Sprite[] fakeHerbImgList = new Sprite[12]; // 가짜 약초 이미지들
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
    public int addMoney; //재계약시 추가금

    public int excelnum;
    public int inum; // 특별손님수세기
    string kind;
    string whatperson;
    bool isMouseClicked = false; //ㄱㅣㅅㅎㅓㅂㅡㅊㅔㅋㅡ
    /**
    public bool grandpa = false;

    public int herbGranpaNum = 0;
    **/

    public List<int> specialPersonList = new List<int>(); //특별손님 리스트

    void Start()
    {
        
        finalTrueHerbList.Clear(); //최종 허브 리스트 초기화 
        specialPersonList.Clear();
        inum = 0;


        day1HerbImg.enabled = false;
        day2HerbImg1.enabled = false;
        day2HerbImg2.enabled = false;
        day3HerbImg1.enabled = false;
        day3HerbImg2.enabled = false;
        day3HerbImg3.enabled = false;

        addMoney = 0;

        sellerText = GameObject.Find("Talk").GetComponent<TypeEffect>();

        currentdate = customerManage.getDate();

        specialPerson(); // 특별손님리스트 추{

        for (int i = 0; i < specialPersonList.Count; i++)
        {
            Debug.Log(specialPersonList[i]);
        }

        if (specialPersonList != null) specialSituation = true;

        if (specialSituation)
        {
            specialCustomer();
        }

        addHerb(); //현재 일차수에 따라 herblist에 해금된 허브 추가하기.
        fillSellerList();

    }

    // Update is called once per frame
    void Update()
    {
        gamecursor.SetActive(false);

        if (specialDone)
        {
            showImg("seller");
            showSellerTalk();
            sellerHerb();
            showSellerHerb();
            specialDone = false;
        }

        if (Input.GetMouseButtonDown(0) && specialSituation && !isMouseClicked)
        {
            isMouseClicked = true;
            showSpecialTalk(whatperson);
            Debug.Log("다음 대사");
        }

        if (nextperson)
        {
            nextperson = false;
            reactionNum = 0;
            StartCoroutine(seller());

        }
    }

    void specialCustomer()
    {
        List<Dictionary<string, object>> special_Dialog = CSVReader.Read("specialPerson");

        excelnum = specialPersonList[inum];
        kind = special_Dialog[specialPersonList[inum]]["kind"].ToString();
        whatperson = special_Dialog[specialPersonList[inum]]["person"].ToString();
        showImg(kind);
        showSpecialTalk(whatperson);
            
    }

    void showSpecialTalk(string person)
    {
        List<Dictionary<string, object>> special_Dialog = CSVReader.Read("specialPerson");
        
        if (person != special_Dialog[excelnum]["person"].ToString())
        {
            inum++;
            if (inum < specialPersonList.Count)
            {
                specialCustomer();
            }
            else
            {
                specialDone = true;
                specialSituation = false;
            }

        }
        //만약 person이 i번째랑 같지 않으면? 스페셜에 다음 사람 있는지 확인
        //다음 사람 있으면 코루틴 실행 or 다음스페셜손님함수 실행
        //ㅇ없으면 specialDone true, specialSituation false.

        string content = special_Dialog[excelnum]["content"].ToString();
        sellerText.SetMsg(content);
        excelnum++;
        Debug.Log("엑셀넘버"+excelnum);
        isMouseClicked = false;


    }

    public void contractBtnClicked()
        {
            finalTrueHerbList.Clear();

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
            nextperson = true;

        }

        public void denyBtnClicked()
        {
            if (sellerNum == 10 && !isContracted)
            {
                noContracted = true;
                Debug.Log("계약안함");
            }

            reactionNum = 10;
            showSellerTalk();
            nextperson = true;
        }

        IEnumerator seller()
        {
            yield return new WaitForSeconds(3.0f);

            showImg("seller");
            showSellerTalk();
            sellerHerb();
            showSellerHerb();
        }

        void showImg(string person)
        {
            if (person == "seller")
            {
                int randomIndex = Random.Range(0, 3);
                sellerImg.sprite = sellerImgList[randomIndex];
            }

            else if (person == "granpa")
            {
                sellerImg.sprite = sellerImgList[3];
            }
            else if (person == "army")
            {
                sellerImg.sprite = sellerImgList[4];
            }

        }

        void showSellerTalk()
        {
            List<Dictionary<string, object>> seller_Dialog = CSVReader.Read("seller");
            int ranNum = Random.Range(reactionNum, reactionNum + 5);
            string content = seller_Dialog[ranNum]["content"].ToString();
            sellerText.SetMsg(content);
        }

        void showSellerHerb()
        {
            if (currentdate == 1)
            {
                day1HerbImg.enabled = true;
                day1HerbImg.sprite = herbImgDecide("catherb");
            }
            else if (currentdate == 2)
            {
                day2HerbImg1.enabled = true;
                day2HerbImg2.enabled = true;
                day2HerbImg1.sprite = herbImgDecide("catherb");
                day2HerbImg2.sprite = herbImgDecide("minaeri");
            }
        }

        Sprite herbImgDecide(string herbName)
        {
            int ranNum;

            if (herbName == "catherb")
            {
                if (realHerbList.Contains("catherb"))
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
            else if (herbName == "minaeri")
            {
                if (realHerbList.Contains("minaeri"))
                    herbimg.sprite = trueHerbImgList[Random.Range(2, 4)];
                else
                    herbimg.sprite = fakeHerbImgList[Random.Range(4, 8)];
            }
            else if (herbName == "floating Mushroo")
            {
                if (realHerbList.Contains("floating Mushroo"))
                    herbimg.sprite = trueHerbImgList[Random.Range(4, 6)];
                else
                    herbimg.sprite = fakeHerbImgList[Random.Range(8, 12)];
            }

            return herbimg.sprite;

        }
        void addHerb()
        {
            if (currentdate == 1)
                herbList.Add("catherb"); // 나중에 그랜파가 와서 대사 말하고, 더하는걸로 변경.
            else if (currentdate == 2)
            {

                herbList.Add("minaeri");
            }

            else if (currentdate == 3)
            {
                herbList.Add("floating Mushroom");
            }

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
            else if (currentdate == 2) specialPersonList.Add(9);
            else if (currentdate == 3)
            {

                specialPersonList.Add(21); // 약초할아버지는 항상 맨 마지막에 넣도록 하기.
                specialPersonList.Add(15);
            }

        }

        /*

        void herbGrandpa(string herbname)
        {
            //약초할아버지 와서 말하고, 맞는 약초할아버지 이미지 나오고, 가고. endcursor를 부활해야 할듯 한데...
            List<Dictionary<string, object>> herbGranpa_Dialog = CSVReader.Read("herbGranpaTalk");
            herbGranpaNum = 0;
            if(herbname == "catherb")
            {
                herbGranpaNum = 1;
            }
            else if(herbname == "minaeri")
            {
                herbGranpaNum = 10;
            }
            else if(herbname == "floating mushroom")
            {
                herbGranpaNum = 16;
            }

            sellerImg.sprite = sellerImgList[4];
            while (herbGranpa_Dialog[herbGranpaNum]["stage"].ToString() != herbname)
            {
               string content = herbGranpa_Dialog[herbGranpaNum]["content"].ToString();
               sellerText.SetMsg(content);


            }
        }

        void herbGrandpaTalk()
        {
            List<Dictionary<string, object>> herbGranpa_Dialog = CSVReader.Read("herbGranpaTalk");
            string content = herbGranpa_Dialog[herbGranpaNum]["content"].ToString();
            sellerText.SetMsg(content);
        }
        */



        public static List<string> getTrueContractHerbList()
        {
            return finalTrueHerbList;
        }

        public static List<string> getHerbList()
        {
            return herbList;
        }

        public void openBtn()
        {
            SceneManager.LoadScene("customerScene");
        }

        public void gopotion() {
            SceneManager.LoadScene("Potionmaking");
        }
    }
