using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BankruptcyEnding : MonoBehaviour
{
    public TypeEffect endingTxt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BtnYes()
    {
        SceneManager.LoadScene("startScene");
    }

    public void BtnNo()
    {
        Application.Quit();
    }
}
