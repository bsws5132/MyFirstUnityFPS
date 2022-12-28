using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float LimitTime;
    public Text text_timer;

    void Update()
    {

        if (LimitTime > 0)
        {
            LimitTime -= Time.deltaTime;
        }
        else if (LimitTime <= 0)
        {
            Time.timeScale = 0.0f;
            SceneManager.LoadScene("Result");
        }
        text_timer.text = "½Ã°£: " + Mathf.Round(LimitTime);
    }
}