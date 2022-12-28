using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            
            Time.timeScale = 0.0f;
            GameObject.Find("ScoreManager").GetComponent<ScoreManager>().score = 0;
            GameObject.Find("ScoreManager").GetComponent<ScoreManager>().LeftTarget = 37;
            SceneManager.LoadScene("Main Menu");

        }
    }
}
