using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHighscore : MonoBehaviour
{
    public GameObject HighScoreBoard;
    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.HasKey("BestRecord"))
            HighScoreBoard.GetComponent<Text>().text = PlayerPrefs.GetInt("BestRecord").ToString();
        else
            HighScoreBoard.GetComponent<Text>().text = "0";

    }
}
