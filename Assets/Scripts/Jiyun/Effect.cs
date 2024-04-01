using System.Collections;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public Animator animator;
    public static bool effect = false;  // 물약 생성되면 효과 적용

    void Start()
    {
        animator.SetBool("eff", false);
    }

    void Update()
    {
        if(effect){
            animator.SetBool("eff", true);
            effect = false;
        }   
    }
}
