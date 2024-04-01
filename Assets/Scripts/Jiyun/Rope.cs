using UnityEngine;

public class Rope : MonoBehaviour
{
    public int day; // 날짜
    public GameObject r1, r2, r3, r4, r5, r6;   // 로프
    void Start()
    {
        r1.SetActive(false);
        r2.SetActive(false);
        r3.SetActive(false);
        r4.SetActive(false);
        r5.SetActive(false);
        r6.SetActive(false);

        day = customerManage.getDate();

        if(day > 0){    // 1일차부터
            r1.SetActive(true);
        }        
        if(day > 3){    // 4일차부터
            r1.SetActive(true);
            r2.SetActive(true);
        }
        if(day > 5){
            r1.SetActive(true);
            r2.SetActive(true);
            r3.SetActive(true);
        }
        if(day > 7){
            r1.SetActive(true);
            r2.SetActive(true);
            r3.SetActive(true);
            r4.SetActive(true);
        }
        if(day > 11){
            r1.SetActive(true);
            r2.SetActive(true);
            r3.SetActive(true);
            r4.SetActive(true);
            r5.SetActive(true);
        }
        if(day > 15){
            r1.SetActive(true);
            r2.SetActive(true);
            r3.SetActive(true);
            r4.SetActive(true);
            r5.SetActive(true);
            r6.SetActive(true);
        }
    }
}
