using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFadeEffect : TutorialBase
{
    [SerializeField]
    private FadeEffect fadeEffect;
    [SerializeField]
    private bool isFadeIn = false;
    private bool isCompleted = false;   // 효과재생이 완료되었는가?

    public override void Enter(){
        if(!isFadeIn){  // true일때
            fadeEffect.fadeIn(OnAfterFadeEffect);
        }
        else{
            fadeEffect.FadeOut(OnAfterFadeEffect);
        }
    }    

    private void OnAfterFadeEffect() {
        isCompleted = true;
    }

    public override void Execute(TutorialController controller){
        if(isCompleted){
            controller.SetNextTutorial();
        }
    }

    public override void Exit(){
        // 오버라이드 해놔야 오류없음
    }
}
