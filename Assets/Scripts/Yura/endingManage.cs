using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class endingManage : MonoBehaviour
{
    public TypeEffect script1;
    public int excelNum = 0;
    string endingName;
    bool SceneEnd = false;

    public GameObject endBut;
    public Sprite[] endingImgList = new Sprite[40];
    public Image endingImg;

    //1,2,3,4,5....
    // Start is called before the first frame update
    void Start()
    {
        endingImg.enabled = false;
        endBut.SetActive(false);
        script1 = GameObject.Find("script").GetComponent<TypeEffect>();
        endingChoice();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !SceneEnd)
        {
            endingImg.enabled = true;
            Debug.Log("click");
            printScript();

        }
        else if(SceneEnd)
        {
            endBut.SetActive(true);
        }
    }

    public void endButton()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("StartScene");
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
            printImg(excelNum);
            string content = Dialog[excelNum]["content"].ToString();
            Debug.Log(content);
            script1.SetMsg(content);
            excelNum++;
        }
    }

    void printImg(int excelNum)
    {
        List<Dictionary<string, object>> Dialog = CSVReader.Read("endings");
        int checkImg = int.Parse(Dialog[excelNum]["img"].ToString());

        if (checkImg != 0)
        {
            endingImg.sprite = endingImgList[checkImg];
        }

    }

    void endingChoice()
    {
        switch (SettleScript.endingNum) //SettleScript.endingNum
        {
            case 1:
                endingName = "True:극적인 균형";
                excelNum = findExcelNum(endingName);
                Debug.Log(excelNum);
                break;
            case 2:
                endingName = "Bad1: 반동분자";
                excelNum = findExcelNum(endingName);
                break;
            case 3:
                endingName = "Bad2: 정부의 끄나풀";
                excelNum = findExcelNum(endingName);
                break;
            case 4:
                endingName = "Bad3: 빚쟁이";
                excelNum = findExcelNum(endingName);
                break;
            case 5:
                endingName = "Bad4: 파산";
                excelNum = findExcelNum(endingName);
                break;
            case 6:
                endingName = "good1: 충실한 협력자";
                excelNum = findExcelNum(endingName);
                break;
            case 7:
                endingName = "good2: 반란 주도자";
                excelNum = findExcelNum(endingName);
                break;
            case 8:
                endingName = "good3: 이중 스파이";
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

        for (int i = 0; i < 47; i++)
        {
            if (endingName == Dialog[i]["ending"].ToString())
            {
                return i;
            }

        }

        return 0;
    }
}
