using UnityEngine;

public class Potionimg : MonoBehaviour
{
    public GameObject[] completepotions;    // 완성된 물약들
    public string cpotion, grade;  // 물약 이름
    void Start()
    {
        foreach(GameObject apotion in completepotions){
            apotion.SetActive(false);
        }
    }

    public void complete(){
        cpotion = Potion.potionname;
        grade = Potion.Getpotiongrade();

        if(grade == "F"){
            completepotions[52].SetActive(true);
            return;
        }
        if(grade == "D"){
            completepotions[51].SetActive(true);
            return;
        }

        if(cpotion == "즉사물약"){
            if(grade == "C"){
                completepotions[50].SetActive(true);
            }
            if(grade == "B"){
                completepotions[49].SetActive(true);
            }
            if(grade == "A"){
                completepotions[48].SetActive(true);
            }
        }

        if(cpotion == "캄캄물약"){
            if(grade == "C"){
                completepotions[47].SetActive(true);
            }
            if(grade == "B"){
                completepotions[46].SetActive(true);
            }
            if(grade == "A"){
                completepotions[45].SetActive(true);
            }
        }

        if(cpotion == "꽁꽁물약"){
            if(grade == "C"){
                completepotions[44].SetActive(true);
            }
            if(grade == "B"){
                completepotions[43].SetActive(true);
            }
            if(grade == "A"){
                completepotions[42].SetActive(true);
            }
        }

        if(cpotion == "시원물약"){
            if(grade == "C"){
                completepotions[41].SetActive(true);
            }
            if(grade == "B"){
                completepotions[40].SetActive(true);
            }
            if(grade == "A"){
                completepotions[39].SetActive(true);
            }
        }

        if(cpotion == "무효화물약"){
            if(grade == "C"){
                completepotions[38].SetActive(true);
            }
            if(grade == "B"){
                completepotions[37].SetActive(true);
            }
            if(grade == "A"){
                completepotions[36].SetActive(true);
            }
        }

        if(cpotion == "투명물약"){
            if(grade == "C"){
                completepotions[35].SetActive(true);
            }
            if(grade == "B"){
                completepotions[34].SetActive(true);
            }
            if(grade == "A"){
                completepotions[33].SetActive(true);
            }
        }

        if(cpotion == "환영물약"){
            if(grade == "C"){
                completepotions[32].SetActive(true);
            }
            if(grade == "B"){
                completepotions[31].SetActive(true);
            }
            if(grade == "A"){
                completepotions[30].SetActive(true);
            }
        }

        if(cpotion == "꿈물약"){
            if(grade == "C"){
                completepotions[29].SetActive(true);
            }
            if(grade == "B"){
                completepotions[28].SetActive(true);
            }
            if(grade == "A"){
                completepotions[27].SetActive(true);
            }
        }

        if(cpotion == "기절물약"){
            if(grade == "C"){
                completepotions[26].SetActive(true);
            }
            if(grade == "B"){
                completepotions[25].SetActive(true);
            }
            if(grade == "A"){
                completepotions[24].SetActive(true);
            }
        }

        if(cpotion == "사향물약"){
            if(grade == "C"){
                completepotions[23].SetActive(true);
            }
            if(grade == "B"){
                completepotions[22].SetActive(true);
            }
            if(grade == "A"){
                completepotions[21].SetActive(true);
            }
        }

        if(cpotion == "마비물약"){
            if(grade == "C"){
                completepotions[20].SetActive(true);
            }
            if(grade == "B"){
                completepotions[19].SetActive(true);
            }
            if(grade == "A"){
                completepotions[18].SetActive(true);
            }
        }

        if(cpotion == "활활물약"){
            if(grade == "C"){
                completepotions[17].SetActive(true);
            }
            if(grade == "B"){
                completepotions[16].SetActive(true);
            }
            if(grade == "A"){
                completepotions[15].SetActive(true);
            }
        }

        if(cpotion == "따끈물약"){
            if(grade == "C"){
                completepotions[14].SetActive(true);
            }
            if(grade == "B"){
                completepotions[13].SetActive(true);
            }
            if(grade == "A"){
                completepotions[12].SetActive(true);
            }
        }

        if(cpotion == "야광물약"){
            if(grade == "C"){
                completepotions[11].SetActive(true);
            }
            if(grade == "B"){
                completepotions[10].SetActive(true);
            }
            if(grade == "A"){
                completepotions[9].SetActive(true);
            }
        }

        if(cpotion == "무적물약"){
            if(grade == "C"){
                completepotions[8].SetActive(true);
            }
            if(grade == "B"){
                completepotions[7].SetActive(true);
            }
            if(grade == "A"){
                completepotions[6].SetActive(true);
            }
        }

        if(cpotion == "치유물약"){
            if(grade == "C"){
                completepotions[5].SetActive(true);
            }
            if(grade == "B"){
                completepotions[4].SetActive(true);
            }
            if(grade == "A"){
                completepotions[3].SetActive(true);
            }
        }

        if(cpotion == "폭발물약"){
            if(grade == "C"){
                completepotions[2].SetActive(true);
            }
            if(grade == "B"){
                completepotions[1].SetActive(true);
            }
            if(grade == "A"){
                completepotions[0].SetActive(true);
            }
        }
    }
}
