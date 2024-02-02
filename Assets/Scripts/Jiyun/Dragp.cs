using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

public class Dragp : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler     
{
    public static Vector2 DefaultPos;   // 처음 위치
    public static List<String> putgreds = new List<string>();    // 넣은 허브와 모든 부재료들 리스트
    public static List<String> putherb = new List<string>();    // 넣은 허브(진짜인지 판별용)
    public static List<String> specialherb = new List<String>();    // 넣은 약초와 스페셜 부재료 리스트
    public GameObject leaf;
    public List<String> getherb;   // 해금된 약초 리스트

    void Start()
    {
        leaf.SetActive(false);

        getherb = sellerManage.getHerbList();  // 해금된 약초들

        foreach(string herbtag in getherb){

            GameObject.Find("leaf").transform.Find(herbtag).gameObject.SetActive(true);    // 해금된 약초
        }
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)  // 드래그 시작
    {
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
            putgreds.Add(gameObject.name);
            if(gameObject.tag.Equals("herb")){  // 넣은게 허브라면..
                putherb.Add(gameObject.name);
                specialherb.Add(gameObject.name);   // catherb minaeri etc..
                if(gameObject.tag.Equals("special")){
                    specialherb.Add(gameObject.name);   // 스페셜 부재료(seasoning1, 2, 3, 4)
                }
            }
        }
    }
}
