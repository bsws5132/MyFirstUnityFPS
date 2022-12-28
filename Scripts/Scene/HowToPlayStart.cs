using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlayStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickHowToPlayStart()
    {
        SceneManager.LoadScene("KillHouse");
        Time.timeScale = 1.0f;
    }

}
