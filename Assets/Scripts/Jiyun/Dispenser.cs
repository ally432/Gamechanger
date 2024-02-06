using UnityEngine;
using UnityEngine.UI;

public class Dispenser : MonoBehaviour
{
    public GameObject openbtn, closebtn, openimg, closeimg, notbottle, yesbottle;
    public static bool isDone = false;
    public static bool isDone2 = false;    // 완성된 병이 나왔는데 다시 닫으면.
    public static bool gage = false;
    public static bool isIn = false;   // 안에 있나?
    public Image loadingbar, loadingbar2;
    public float speed;
    float currentValue, currentValue2;

    void Start()
    {
        closebtn.SetActive(false);
        openimg.SetActive(false);    
        yesbottle.SetActive(false);
    }

    void Update()
    {
        if (gage){
            if(isDone){
                currentValue += speed * Time.deltaTime;
                loadingbar.fillAmount = currentValue / 100;

                if (currentValue >= 100f)
                {
                    gage = false;
                    Invoke("gagezero", 5f);
                }
            }
        }
        if (Potion.makeover){   // 흘러들어오는 부분
            currentValue2 += speed * Time.deltaTime;
            loadingbar2.fillAmount = currentValue2 / 100;

            if (currentValue2 >= 100f)
            {
                Potion.makeover = false;
            }
        }
    }

    void gagezero(){
        loadingbar.fillAmount = 0f;
        openimg.SetActive(false);
        closebtn.SetActive(false);
        closeimg.SetActive(true);
        openbtn.SetActive(true);
    }

    public void opening(){  // 열기
        closeimg.SetActive(false);
        openimg.SetActive(true);
        openbtn.SetActive(false);
        closebtn.SetActive(true);

        if(isDone){ // 완성된 병 나옴
            notbottle.SetActive(false); // 넣은 병 안 보이게
            yesbottle.SetActive(true);
            isDone = false;
            isDone2 = true;
        }
        else{
            notbottle.SetActive(true);
        }
    }

    public void closing(){  // 닫기
        closebtn.SetActive(false);
        openimg.SetActive(false);
        closeimg.SetActive(true);
        openbtn.SetActive(true);

        if(isIn){   // 병이 들어갔는가
            notbottle.SetActive(false);
        }

        if(isDone2){    // 완성되었는데 다시 닫으면!
            yesbottle.SetActive(false);
            isDone = true;
        }
    }

    public void gagestart(){    // 게이지 버튼
        currentValue = 0f;
        currentValue2 = 0f;
        loadingbar.fillAmount = 0f;
        loadingbar2.fillAmount = 0f;
        gage = true;
        isDone = true;
    }
}
