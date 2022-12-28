using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HealthManager : MonoBehaviour
{
    public float hitpoint = 100f;

    public void ApplyDamage(float damage)
    {
        hitpoint -= damage;
        
        if(hitpoint == 0)
        {

            GetComponent<TargetScript>().isHit = true;
            GameObject.Find("UI").GetComponent<ScoreManager>().score += 80;
            GameObject.Find("ScoreManager").GetComponent<ScoreManager>().score += 80;
            
            if(SceneManager.GetActiveScene().buildIndex == 4)// testmap
            {
                GameObject.Find("ScoreManager").GetComponent<ScoreManager>().LeftTarget += 1;
                GameObject.Find("UI").GetComponent<ScoreManager>().LeftTarget += 1;
            }
            else
            {
                GameObject.Find("ScoreManager").GetComponent<ScoreManager>().LeftTarget -= 1;
                GameObject.Find("UI").GetComponent<ScoreManager>().LeftTarget -= 1;
            }
            hitpoint = 100;

        }
        else if (hitpoint < 0)
        {
            GetComponent<TargetScript>().isHit = true;
            GameObject.Find("UI").GetComponent<ScoreManager>().score += 160;
            GameObject.Find("ScoreManager").GetComponent<ScoreManager>().score += 160;
            
            if (SceneManager.GetActiveScene().buildIndex == 4) // testmap
            {
                GameObject.Find("ScoreManager").GetComponent<ScoreManager>().LeftTarget += 1;
                GameObject.Find("UI").GetComponent<ScoreManager>().LeftTarget += 1;
            }
            else
            {
                GameObject.Find("ScoreManager").GetComponent<ScoreManager>().LeftTarget -= 1;
                GameObject.Find("UI").GetComponent<ScoreManager>().LeftTarget -= 1;
            }

            hitpoint = 100;

        }
    }
}
