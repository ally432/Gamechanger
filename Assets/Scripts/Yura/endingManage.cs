using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class endingManage : MonoBehaviour
{
    public TypeEffect script1;
    public int excelNum = 0;
    string endingName;
    bool SceneEnd = false;
    int endingNum = 1;

    // Start is called before the first frame update
    void Start()
    {
        script1 = GameObject.Find("script").GetComponent<TypeEffect>();
        endingChoice();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !SceneEnd)
        {
            Debug.Log("click");
            printScript();
            
        }
        else
        {
            //버튼 활성화
        }
    }

    void printScript()
    {
        List<Dictionary<string, object>> Dialog = CSVReader.Read("endings");
        if (endingName != Dialog[excelNum]["ending"].ToString())
        {
            Debug.Log("different");
            SceneEnd = true;
            return;
        }
        else
        {
            string content = Dialog[excelNum]["content"].ToString();
            Debug.Log(content);
            script1.SetMsg(content);
            excelNum++;
        }
    }

    void endingChoice()
    {
        switch (endingNum) //SettleScript.endingNum
        {
            case 1:
                endingName = "True:극적인 균형";
                excelNum = findExcelNum(endingName);
                Debug.Log(excelNum);
                break;
            case 2:
                endingName = "반동분자";
                excelNum = findExcelNum(endingName);
                break;
            case 3:
                endingName = "정부의 끄나풀";
                excelNum = findExcelNum(endingName);
                break;
            case 4:
                endingName = "빚쟁이";
                excelNum = findExcelNum(endingName);
                break;
            case 5:
                endingName = "파산";
                excelNum = findExcelNum(endingName);
                break;
            case 6:
                endingName = "충실한 협력자";
                excelNum = findExcelNum(endingName);
                break;
            case 7:
                endingName = "반란 주도자";
                excelNum = findExcelNum(endingName);
                break;
            case 8:
                endingName = "이중 스파이";
                excelNum = findExcelNum(endingName);
                break;
            case 0:
                script1.SetMsg("엔딩 오류");
                break;

        }
            
      
    }

    int findExcelNum(string endingName)
    {
        List<Dictionary<string, object>> Dialog = CSVReader.Read("endings");

        for(int i = 0; i< 47; i++)
        {
            if (endingName == Dialog[i]["ending"].ToString())
            {
                return i;
            }             

        }

        return 0;
    }
}
