using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Swipe : MonoBehaviour
{
    public Canvas canva;
    public float speed = 5f;    // 스와이프 속도
    private Vector3 startpos;
    private Vector3 endpos;
    private Vector3 targetpos;  // 움직일 곳
    private bool isSwiping = false;

    void Update()
    {
        /*if(Input.touchCount > 0){   // 터치 감지
            Touch touch = Input.GetTouch(0);

            switch (touch.phase){
                case TouchPhase.Began:
                    startpos = touch.position;
                    break;
                case TouchPhase.Ended:
                    endpos = touch.position;
                    IsSwipe();
                    break;
            }
        }*/

        if (Input.GetMouseButtonDown(0)){
            startpos = Input.mousePosition;
            isSwiping = true;
        }

        if (isSwiping){
            if (Input.GetMouseButton(0)){
                endpos = Input.mousePosition;
                IsSwipe();
            }
            if(Input.GetMouseButtonUp(0)){
                isSwiping = false;
            }
        }
    }

    void IsSwipe(){ // 스와이프인지 판별
        float dir = endpos.x - startpos.x;

        if(Mathf.Abs(dir) > 50f){   // 50보다 클 경우 스와이프
            float swipe = Mathf.Sign(dir);
            if (swipe > 0){   // 오른쪽 스와이프
                //Move(1);
                canva.gameObject.SetActive(false);
            }
            else{
                //Move(-1);
            }
        }
    }

    /*void Move(int direction){
        targetpos = transform.position + new Vector3(direction * speed, 0f, 0f);
    }*/

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetpos, Time.deltaTime * speed);
    }
}
