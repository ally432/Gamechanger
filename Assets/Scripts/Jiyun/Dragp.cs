using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;
using TMPro;

public class Dragp : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler     
{
    public Vector2 DefaultPos;   // 처음 위치
    public static List<String> putgreds = new List<string>();    // 넣은 허브와 모든 부재료들 리스트
    public static List<String> putherb = new List<string>();    // 넣은 허브(진짜인지 판별용)
    public static List<String> specialherb = new List<String>();    // 넣은 약초와 스페셜 부재료 리스트
    public GameObject leaf, spec1, spec2, spec3, spec4, bowl1, bowl2, bowl3, bowl4;
    public List<String> getherb = new List<string>();   // 해금된 약초 리스트
    public TextMeshProUGUI ctext, popup, change;   // 소지금 텍스트
    public int day; // 날짜
    public AudioSource drag;    // 약초 뜯는 소리
    public static bool putsound = false;    // 재료 넣는 소리 플래그

    void Start()
    {
        leaf.SetActive(false);
        spec1.SetActive(false);
        spec2.SetActive(false);
        spec3.SetActive(false);
        spec4.SetActive(false);
        bowl1.SetActive(false);
        bowl2.SetActive(false);
        bowl3.SetActive(false);
        bowl4.SetActive(false);
        
        day = customerManage.getDate();
        
        if(day > 1){
            spec1.SetActive(true);
            bowl1.SetActive(true);
        }
        if(day > 5){
            spec1.SetActive(true);
            spec2.SetActive(true);
            bowl1.SetActive(true);
            bowl2.SetActive(true);
        }
        if(day > 9){
            spec1.SetActive(true);
            spec2.SetActive(true);
            spec3.SetActive(true);
            bowl1.SetActive(true);
            bowl2.SetActive(true);
            bowl3.SetActive(true);
        }
        if(day > 13){
            spec1.SetActive(true);
            spec2.SetActive(true);
            spec3.SetActive(true);
            spec4.SetActive(true);
            bowl1.SetActive(true);
            bowl2.SetActive(true);
            bowl3.SetActive(true);
            bowl4.SetActive(true);
        }

        getherb = sellerManage.getHerbList();  // 해금된 약초들

        foreach(string herbtag in getherb){
            GameObject.Find("leaf").transform.Find(herbtag).gameObject.SetActive(true);    // 해금된 약초
        }

        ctext.text = customerManage.money.ToString();   // 초기금 보여주기
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)  // 드래그 시작
    {
        drag = GetComponent<AudioSource>();

        drag.Play();
        DefaultPos = this.transform.position;
    }
    void IDragHandler.OnDrag(PointerEventData eventData)    // 드래그 중
    {
        // 스크린 좌표를 캔버스 좌표로 변환
        // eventData.position은 출력하고 싶은 스크린 좌표
        // Camera.main은 스크린 좌표와 연관된 카메라
        // localPos는 변환된 좌표를 저장한 변수
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform.parent, eventData.position, Camera.main, out Vector2 localPos);
        transform.localPosition = localPos;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData) // 드래그 끝
    {
        Invoke("put", .5f);
    }

    void put(){ // 원위치로
        this.transform.position = DefaultPos;
    }

    void OnTriggerEnter2D(Collider2D other) // 충돌 발생
    {
        if(other.gameObject.tag.Equals("putzone")){
            if(putgreds != null){   // 이미 리스트에 들어있는지 확인
                for(int i = 0; i < putgreds.Count; i++){
                    if(putgreds[i].Contains(gameObject.name)){
                        popup.text = "이미 들어간 재료입니다!";
                        Invoke("None1", 1f);
                        return;
                    }
                }
            }
            
            Effect.wav = true;  // 파동 효과
            putsound = true;
            putgreds.Add(gameObject.name);
            if(gameObject.tag.Equals("herb")){  // 넣은게 허브라면..
                List<Dictionary<string, object>> data = CSVReader.Read("herbcost");
                for (int i = 0; i < 6; i++){
                    if(data[i]["name"].ToString() == gameObject.name){  // 부딪힌 약초 이름을 엑셀에서 찾기
                            customerManage.money -= int.Parse(data[i]["cost"].ToString()) + sellerManage.addMoney;  // 잔액에서 약초값 빼기
                            change.text = "-" + (int.Parse(data[i]["cost"].ToString()) + sellerManage.addMoney);    // 빠지는 돈 안내
                            Invoke("None2", .5f);
                    }
                } 
                ctext.text = customerManage.money.ToString();   // 바뀐 금액 갱신

                putherb.Add(gameObject.name);
                specialherb.Add(gameObject.name);   // 허브
                if(gameObject.tag.Equals("special")){
                    specialherb.Add(gameObject.name);   // 스페셜 부재료(seasoning1, 2, 3, 4)
                }
            }
        }
    }
    void None1(){   // 이미 들어간 재료 안내
        popup.text = "";
    }
    void None2(){   // 빠지는 돈 안내
        change.text = "";
    }
}
