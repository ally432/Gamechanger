using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Potion : MonoBehaviour
{
    public static string fire; // 불의 강도
    public static string time; // 시간
    public static string potiongrade;   // 물약 등급
    public static int potionnum = 0;   // 물약 점수
    public Image loadingbar;    // 제조 완료 시 넘어가는 물약
    float currentValue; // 로딩바와 관련
    public float speed; // 로딩바와 관련
    bool isFilling = false; // 로딩바와 관련
    public List<String> getherb;   // 해금된 약초 리스트
    public List<String> ingredients;    // 손님이 요구한 조미료 리스트

    void Start(){
        getherb = sellerManage.getHerbList();  // 계약한 진짜 약초들

        foreach(string herbtag in getherb){
            GameObject leaf = GameObject.FindGameObjectWithTag(herbtag);
            leaf.SetActive(true);
        }
        if (getherb.Count > 0)
        {
            for (int i = 0; i < getherb.Count; i++)
                Debug.Log(getherb[i]);
        }
    }

    void Update()
    {
        if (isFilling){
            currentValue += speed * Time.deltaTime;
            loadingbar.fillAmount = currentValue / 100;

            if (currentValue >= 100f)
            {
                isFilling = false;
            }
        }
    }

    public void End(){  // 제조 버튼 클릭
        custominfo(5);
        currentValue = 0f;
        loadingbar.fillAmount = 0f;
        //isFilling = true;   // 나중에 등급 판별 완성된 후에 작동되도록 옮기기
    }

    public void custominfo(int cusnum){ // 손님 번호로 엑셀 파일 읽고 내용과 일치하는지 확인하여 점수 매기기
        List<Dictionary<string, object>> data = CSVReader.Read("customerOrder");    // 손님번호-1 해야 알맞은 라인 읽어옴!

        if(data[cusnum-1]["fire"].ToString() == fire){    // 화력을 맞게 설정했는가
            potionnum += 1;
        }
        
        if(data[cusnum-1]["time"].ToString() == time){   // 시간을 맞게 설정했는가
            potionnum += 1;
        }
        
        ingredients = new List<string>();   // 리스트 초기화
        ingredlist(data, cusnum);   // 손님이 원한 조미료 리스트 가져오기

        if((Dragp.putgreds != null) && (Dragp.putgreds.Count == ingredients.Count)){    // 두 리스트의 원소 개수가 같을 때
            for (int i = 0; i < Dragp.putgreds.Count; i++){
                if(ingredients.Contains(Dragp.putgreds[i])){
                    if(i == Dragp.putgreds.Count - 1){
                        potionnum += 1;
                        break;
                    }
                }
                else{
                    break;
                }
            }
        }



        Debug.Log(potionnum);
    }

    public List<String> ingredlist(List<Dictionary<string, object>> data, int cusnum){
        if(data[cusnum-1]["ingredient1"] != null && !string.IsNullOrEmpty(data[cusnum-1]["ingredient1"].ToString())){  // 손님이 원한 조미료 리스트
            ingredients.Add(data[cusnum-1]["ingredient1"].ToString());
            if(data[cusnum-1]["ingredient2"] != null && !string.IsNullOrEmpty(data[cusnum-1]["ingredient2"].ToString())){
                ingredients.Add(data[cusnum-1]["ingredient2"].ToString());
                if(data[cusnum-1]["ingredient3"] != null && !string.IsNullOrEmpty(data[cusnum-1]["ingredient3"].ToString())){
                    ingredients.Add(data[cusnum-1]["ingredient3"].ToString());
                    if(data[cusnum-1]["ingredient4"] != null && !string.IsNullOrEmpty(data[cusnum-1]["ingredient4"].ToString())){
                        ingredients.Add(data[cusnum-1]["ingredient4"].ToString());
                    }
                }
            }
        }
        return ingredients;
    }

    public static String Getpotiongrade(){ // 물약 등급 얻는 함수
        if(potionnum == 0){
            potiongrade = "F";
        }
        else if(potionnum == 1){
            potiongrade = "D";
        }
        else if(potionnum == 2){
            potiongrade = "C";
        }
        else if(potionnum == 3){
            potiongrade = "B";
        }
        else if(potionnum == 4){
            potiongrade = "A";
        }

        return potiongrade;
    }
}
