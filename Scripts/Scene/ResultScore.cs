using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScore : MonoBehaviour
{
    public int ResultScr;
    public int ResultTar;
    public Text TextResultScr;
    public Text TextResultTarget;

    private void Start()
    {
       
       ResultScr = GameObject.Find("ScoreManager").GetComponent<ScoreManager>().score;
       ResultTar = GameObject.Find("ScoreManager").GetComponent<ScoreManager>().LeftTarget;
    }
    // Update is called once per frame
    void Update()
    {
        TextResultScr.text = "점수 : " + ResultScr;
        TextResultTarget.text = "남은 타겟 : " + ResultTar;
    }
}
