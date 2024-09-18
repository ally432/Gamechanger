using UnityEngine;
using DG.Tweening;
using TMPro;

public class Herbbook : MonoBehaviour
{
    public RectTransform panel;
    public GameObject hlock, prev, next;    // 잠금 이미지, 이전&다음 버튼
    public GameObject[] herbs;  // 약초 노트 각 페이지 이미지
    public int currentpage = 0; // 현재 페이지
    public int day;
    public TextMeshProUGUI htext;   // 페이지
    public AudioSource paper;

    public GameObject touchIcon2;

    void Start(){
        paper = GetComponent<AudioSource>();

        day = customerManage.getDate();    // 날짜 가져오기
        ShowPage(currentpage);
    }

    public void Open(){ // 노트 펼치기
        panel.DOLocalMoveY(0, 1f).SetEase(Ease.OutBack);
        touchIcon2.SetActive(false);
    }
    public void Close(){    // 노트 닫기
        panel.DOLocalMoveY(-5000, 1f).SetEase(Ease.InBack);
    }

    void ShowPage(int page){
        prev.SetActive(true);
        next.SetActive(true);
        hlock.SetActive(false);
        
        foreach(GameObject herb in herbs){
            herb.SetActive(false);
        }

        if(page == 0){  // 맨 첫장은 이전 버튼 안 보임  -> 속성 파트
            prev.SetActive(false);
            herbs[page].SetActive(true);
        }
        // 약초 파트
        if(page == 1){  // 이슬산딸기
            herbs[page].SetActive(true);
        }
        if(page == 2){   // 은빛불꽃
            if(day > 3){
                herbs[page].SetActive(true);
            }
            else{
                hlock.SetActive(true);
            }
        }
        if(page == 3){   // 와다닥고추
            if(day > 5){
                herbs[page].SetActive(true);
            }
            else{
                hlock.SetActive(true);
            }
        }
        if(page == 4){   // 그림자엉겅퀴
            if(day > 7){
                herbs[page].SetActive(true);
            }
            else{
                hlock.SetActive(true);
            }
        }
        if(page == 5){   // 불빛버섯
            if(day > 11){
                herbs[page].SetActive(true);
            }
            else{
                hlock.SetActive(true);
            }
        }
        if(page == 6){   // 동굴연근
            if(day > 15){
                herbs[page].SetActive(true);
            }
            else{
                hlock.SetActive(true);
            }
        }
        // 부재료 파트
        if(page == 7){ 
            next.SetActive(false);
            herbs[page].SetActive(true);
        }
          
        htext.text = (page+1).ToString();
    }

    public void Prev(){ // 이전 페이지
        paper.Play();
        currentpage--;
        
        if (currentpage >= 0){
            ShowPage(currentpage);
        }     
    }
    public void Next(){ // 다음 페이지
        paper.Play();
        currentpage++;

        if(currentpage < herbs.Length){
            ShowPage(currentpage);
        }
    }   

    public void attr(){ // 속성으로 가기
        paper.Play();
        currentpage = 0;
        ShowPage(currentpage);
    }
    public void herb(){
        paper.Play();
        currentpage = 1;
        ShowPage(currentpage);
    }
    public void spec(){
        paper.Play();
        currentpage = 7;
        ShowPage(currentpage);
    }
}
