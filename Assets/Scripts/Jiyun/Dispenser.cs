using UnityEngine;
using UnityEngine.UI;

public class Dispenser : MonoBehaviour
{
    public GameObject openbtn, closebtn, openimg, closeimg, notbottle, yesbottle, gagebtn;
    public static bool isDone = false;
    public static bool isDone2 = false;    // 완성된 병이 나왔는데 다시 닫으면.
    public static bool gage = false;
    public static bool isIn = false;   // 안에 있나?
    public static bool isOut = false;   // 완성된 물병이 테이블 위에 있나?
    public static bool clear = false;   // 최종 물약
    public static bool isFirst = true;  // 한 번에 하나의 물약만 제조 가능
    public Image loadingbar, loadingbar2;
    public float speed;
    float currentValue, currentValue2;

    public AudioSource sound;   // 디스펜서 소리

    void Start()
    {
        openbtn.SetActive(false);   // 제조실 안 갔다오면 안 열림
        closebtn.SetActive(false);
        openimg.SetActive(false);    
        yesbottle.SetActive(false);
        gagebtn.SetActive(false);

        sound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(gage){   // 게이지 버튼이 눌렸다면
            currentValue += speed * Time.deltaTime;
            loadingbar.fillAmount = currentValue / 100;
            currentValue2 -= speed * Time.deltaTime;    // 흘러들어온 액체는 나감
            loadingbar2.fillAmount = currentValue2 / 100;

            openbtn.SetActive(false);   // 게이지 다 안 채워졌으면 안 열림

            if (currentValue >= 100f)   // 로딩바를 다 채웠다면
            {
                gage = false;
                openbtn.SetActive(true);    // 게이지 다 채워졌으면 열림
                Invoke("gagezero", 4f);
                isDone = true;
            }
        }

        if (Potion.makeover){   // 흘러들어오는 부분
            currentValue2 += speed * Time.deltaTime;
            loadingbar2.fillAmount = currentValue2 / 100;

            if (currentValue2 >= 100f)
            {
                openbtn.SetActive(true);
                Potion.makeover = false;
            }
        }

        if(customerManage.crush){
            openbtn.SetActive(false);
            customerManage.crush = false;
        }

        if(Potionimg.trash){
            openbtn.SetActive(false);
            Potionimg.trash = false;
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
        sound.Play();
        closeimg.SetActive(false);
        openimg.SetActive(true);    // 열린 디스펜서
        openbtn.SetActive(false);
        closebtn.SetActive(true);   // 닫는 버튼

        if(isDone){ // 완성된 병 나옴
            if(!clear){ // 손님에게 줄 물약 완성 안되었다면
                notbottle.SetActive(false); // 넣은 병 안 보이게
                yesbottle.SetActive(true);  // 완성된 병 보이게
                isDone2 = true; // 완성!
                isDone = false;
            }
        }

        else{
            if(clear){  // 손님에게 줄 물약 완성
                notbottle.SetActive(false);
                yesbottle.SetActive(false);
            }

            else{ 
                if(isOut){  // 물약 따른 병이 테이블 위에 있을 때
                    notbottle.SetActive(false);
                }
                else{
                    notbottle.SetActive(true);
                }
            }
        }
        
        gagebtn.SetActive(false);   // 문 열려있을 시 비활성화
    }

    public void closing(){  // 닫기
        sound.Play();
        closebtn.SetActive(false);
        openimg.SetActive(false);
        closeimg.SetActive(true);
        openbtn.SetActive(true);

        if(isIn){   // 병이 들어갔는가
            notbottle.SetActive(false);
            gagebtn.SetActive(true);    // 문을 닫아야만 게이지 버튼 클릭 가능
        }

        if(isDone2){    // 완성되었는데 다시 닫으면!
            if(!isOut){  // 완성된게 테이블 위에 없으면
                yesbottle.SetActive(false);
            }       
            isDone = true;
            isDone2 = false;
        }
    }

    public void gagestart(){    // 게이지 버튼
        if(isFirst){
            sound.Play();
            if(isIn){
                currentValue = 0f;
                currentValue2 = 0f;
                loadingbar.fillAmount = 0f;
                loadingbar2.fillAmount = 0f;
                gage = true;
                isFirst = false;    // 한 번에 하나!
                isIn = false;
            }
        }
    }
}
