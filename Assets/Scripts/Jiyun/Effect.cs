using UnityEngine;

public class Effect : MonoBehaviour
{
    public Animator smoke, wave;
    public static bool effect = false;  // 물약 생성되면 효과 적용
    public static bool wav = false; // 재료 투여시 효과 적용

    void Start()
    {
        smoke.SetBool("eff", false);
        wave.SetBool("wav", false);
    }

    void Update()
    {
        if(effect){
            smoke.SetBool("eff", true);
            effect = false;
        }   
        if(wav){
            wave.SetBool("wav", true);
            wav = false;
        }
    }
}
