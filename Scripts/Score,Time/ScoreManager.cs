using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public int LeftTarget;
    public Text textScore;
    public Text textTarget;


    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) // Killhouse 
        {
            textScore.text = "점수 : " + score;
            textTarget.text = "남은 표적 : " + LeftTarget;

        }

        else if(SceneManager.GetActiveScene().buildIndex == 4 ) // TestMap
        {
            textScore.text = "점수 : " + score;
            textTarget.text = "맞춘 표적지 : " + LeftTarget;
        }


    }

}