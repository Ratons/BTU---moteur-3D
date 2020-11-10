using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public GameObject ScoreText;
    public static int Score=0;

    void Update()
    {
        ScoreText.GetComponent<Text>().text = Score+" ";
    }
}
