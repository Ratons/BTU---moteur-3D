using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    public GameObject ScoreText;
    public static int Score=0;  //On stocke le Score du joueur dans cette variable

    void Update()
    {
        ScoreText.GetComponent<Text>().text = Score.ToString(); //On affiche la variable Sore à l'écran
    }
}
