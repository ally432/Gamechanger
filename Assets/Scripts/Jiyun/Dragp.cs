using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

public class Dragp : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler     
{
    public static Vector2 DefaultPos;   // 처음 위치
    public static List<String> putgreds = new List<string>();    // 넣은 약초들 리스트
    public GameObject leaf1, leaf2, leaf3;
    public List<String> getherb;   // 해금된 약초 리스트

    void Start()
    {
        leaf1.SetActive(false);
        leaf2.SetActive(false);
        leaf3.SetActive(false);

        getherb = sellerManage.getHerbList();  // 계약한 진짜 약초들

        foreach(string herbtag in getherb){
            GameObject leaf = GameObject.FindGameObjectWithTag(herbtag);
            leaf.SetActive(true);
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
            putgreds.Add(gameObject.tag.ToString());
        }
    }
}
