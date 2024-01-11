using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Drag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler     
{
    public static Vector2 DefaultPos;   // 처음 위치

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
            if(gameObject.tag.Equals("leaf1")){
                print("leaf1");
            }

            if(gameObject.tag.Equals("leaf2")){
                print("leaf2");
            }
            
            if(gameObject.tag.Equals("leaf3")){
                print("leaf3");
            }
        }
    }
}
