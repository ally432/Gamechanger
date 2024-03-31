using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public GameObject arrow1, arrow2, arrow3;    // plock은 잠금화면
    public GameObject[] clock;  // 스탑워치
    public int num = 0; // 몇 분?
    public static bool remake = false;  // reset 버튼 판단

    void Start()
    {   // 디폴트를 화력 1, 3분으로 설정 
        arrow2.SetActive(false);
        arrow3.SetActive(false);

        time();

        Potion.fire = "1";
        Potion.time = "3";
    }

    void Update()
    {
        if(remake){
            num = 0;
            arrow1.SetActive(true);
            arrow2.SetActive(false);
            arrow3.SetActive(false);

            time();

            Potion.fire = "1";
            Potion.time = "3";

            remake = false;
        }
    }

    // 화력 설정
    public void Set1(){
        arrow1.SetActive(true);
        arrow2.SetActive(false);
        arrow3.SetActive(false);
        Potion.fire = "1";
    }
    public void Set2(){
        arrow1.SetActive(false);
        arrow2.SetActive(true);
        arrow3.SetActive(false);
        Potion.fire = "2";
    }
    public void Set3(){
        arrow1.SetActive(false);
        arrow2.SetActive(false);
        arrow3.SetActive(true);
        Potion.fire = "3";
    }

    public void time(){
        foreach(GameObject t in clock){
            t.SetActive(false);
        }

        if(num == 3){   // 7분에서는 다시 3분 스탑워치로
            num = 0;   
        }

        clock[num].SetActive(true);

        switch(num){    // 각 이미지에 따른 변수값
            case 0:
                Potion.time = "3";
                break;
            case 1:
                Potion.time = "5";
                break;
            case 2:
                Potion.time = "7";
                break;
        }

        num++;
    }
}
