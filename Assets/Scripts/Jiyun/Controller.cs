using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UIElements;

public class Controller : MonoBehaviour
{
    public GameObject arrow1, arrow2, arrow3, firefade1, firefade2, firefade3, greenwater;    // plock은 잠금화면
    public GameObject[] clock;  // 스탑워치
    public int num = 0; // 몇 분?
    public static bool remake = false;  // reset 버튼 판단
    public static bool isFadein = false;   // true면 점점 진해짐
    private Action onCompleteCallback;  // fadein이나 fadeout 다음에 진행할 함수

    void Start()
    {   // 디폴트를 화력 1, 3분으로 설정 
        arrow2.SetActive(false);
        arrow3.SetActive(false);

        time();

        Potion.fire = "1";
        Potion.time = "3";

        firefade1.SetActive(false);
        firefade2.SetActive(false);
        firefade3.SetActive(false);
        greenwater.SetActive(false);
    }

    void Update()
    {
        if(remake){
            num = 0;
            arrow1.SetActive(true);
            arrow2.SetActive(false);
            arrow3.SetActive(false);

            firefade1.SetActive(false);
            firefade2.SetActive(false);
            firefade3.SetActive(false);
            greenwater.SetActive(false);

            time();

            Potion.fire = "1";
            Potion.time = "3";

            remake = false;
        }

        if(isFadein){   // fadein 시작!
            if(Potion.fire == "1"){
                firefade1.SetActive(true);
            }
            
            if(Potion.fire == "2"){
                firefade2.SetActive(true);
            }

            if(Potion.fire == "3"){
                firefade3.SetActive(true);
            }

            greenwater.SetActive(true);

            StartCoroutine(CoFadeIn());
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

    IEnumerator CoFadeIn(){
        float elapsedTime = 0f; // 누적 시간
        float fadedTime = 2f; // 총 소요 시간

        while(elapsedTime <= fadedTime){
            if(Potion.fire == "1"){
                firefade1.GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(0f, 1f, elapsedTime));
            }
            
            if(Potion.fire == "2"){
                firefade2.GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(0f, 1f, elapsedTime));
            }

            if(Potion.fire == "3"){
                firefade3.GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(0f, 1f, elapsedTime));
            }

            greenwater.GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(0f, 1f, elapsedTime));

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        onCompleteCallback?.Invoke();   // 이후에 진행할 함수 있을 경우 진행
        yield break;
    }
}
