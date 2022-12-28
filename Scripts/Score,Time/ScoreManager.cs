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
            textScore.text = "���� : " + score;
            textTarget.text = "���� ǥ�� : " + LeftTarget;

        }

        else if(SceneManager.GetActiveScene().buildIndex == 4 ) // TestMap
        {
            textScore.text = "���� : " + score;
            textTarget.text = "���� ǥ���� : " + LeftTarget;
        }


    }

}