using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField]
    private List<TutorialBase> tutorials;   // 진행할 튜토리얼 행동들
    [SerializeField]
    private string nextSceneName = "";  // 튜토리얼 후 다음 씬 이름

    private TutorialBase currentTutorial = null;    // 현재 진행하는 튜토리얼
    private int currentIndex = -1;  // 현재가 마지막 행동인가


    private void Start()
    {   // 튜토리얼 플레이!
        SetNextTutorial();   
    }

    private void Update()
    {   // 현재 진행중인 튜토리얼
        if(currentTutorial != null){
            currentTutorial.Execute(this);
        }   
    }

    public void SetNextTutorial(){
        if(currentTutorial != null){
            currentTutorial.Exit();
        }

        if(currentIndex >= tutorials.Count - 1){    // 마지막 튜토리얼까지 완수
            CompletedAllTutorials();
            return;
        }

        // 아직 튜토리얼이 남아있다면
        currentIndex++;
        currentTutorial = tutorials[currentIndex];

        currentTutorial.Enter();
    }    

    public void CompletedAllTutorials(){
        currentTutorial = null;

        Debug.Log("Complete!");
        // 씬 전환 넣기..?
    }
}
