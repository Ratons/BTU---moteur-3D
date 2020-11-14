using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHighscore : MonoBehaviour
{
    public GameObject HighScoreBoard;
    void Start()
    {
        if(PlayerPrefs.HasKey("BestRecord"))                                                            //S'il y a un record enregistré sur le pc
            HighScoreBoard.GetComponent<Text>().text = PlayerPrefs.GetInt("BestRecord").ToString();     //  On affiche ce record
        else                                                                                            //Sinon
            HighScoreBoard.GetComponent<Text>().text = "0";                                             //  On affiche 0

    }
}
