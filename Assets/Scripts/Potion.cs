using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public static int fire; // 불의 강도
    public static int time; // 시간
    public static string potiongrade;   // 물약 등급
    public GameObject leaf1;

    void Start()
    {
        leaf1.SetActive(false);
        showleaf(1);    // 1일차라면?
    }

    public void showleaf(int day){
        //List<Dictionary<string, object>> leafinfo = CSVReader.Read("leaf");

        //if(leafinfo[day-1]["day"].ToString() == "1"){ // 엑셀의 day번째 줄의 day정보, 0번째 줄부터 가져와야 함
            //print(leafinfo[day - 1]["leafnum"].ToString());
            //leaf1.SetActive(true);
        //}
    }
    /*void Update()
    {
        // 화력, 시간 변경될 때마다 값 변하는 거 확인 완료
        print(time);    
    }*/
}
