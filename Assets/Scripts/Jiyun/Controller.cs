using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public GameObject arrow1, arrow2, arrow3;
    public RectTransform panel;
    public Image potion;
    public Sprite[] potions = new Sprite[3];    // 추후 16개
    int index = 0;

    void Start()
    {   // 디폴트를 화력 1, 3분으로 설정
        arrow2.SetActive(false);
        arrow3.SetActive(false);

        Potion.fire = "1";
        Potion.time = "3";

        potion = GetComponent<Image>();
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

    // 시간 설정
    public void Set0(){
        Set1();
        Potion.time = "3";
    }
    public void Set5(){
        Set2();
        Potion.time = "5";
    }
    public void Set10(){
        Set3();
        Potion.time = "7";
    }

    // 노트 펼치기
    public void Open(){
        panel.DOLocalMoveY(0, 1f).SetEase(Ease.OutBack);
        if(Potion.plist != null){
            potion.sprite = potions[0];
            index++;
        }
    }
    public void Close(){
        panel.DOLocalMoveY(-554, 1f).SetEase(Ease.InBack);
    }
    void Show(){
        if(Potion.plist != null){   // 물약이 해금되었다면!
            potion.sprite = potions[index];
            index++;
        }
    }
}
