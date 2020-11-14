using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditStatText : MonoBehaviour
{
    public GameObject playerRef;

    public GameObject AttText;
    public static int Att = 0;

    public GameObject AttSpdText;
    public static float AttSpd = 0;

    public GameObject SpdText;
    public static float Spd = 0;

    // Update is called once per frame
    void Update()
    {
        if (playerRef)
        {
            Att = Player.damage;                                                    //Récupération dégats du Joueur
            AttSpd = 2 - playerRef.GetComponent<Player>().GetFireSpeed() / 750 ;    //Récupération et normalisation Vitesse d'Attaque
            Spd = playerRef.GetComponent<Player>().GetSpeed() / 3;                  //Récupération et normalisation Vitesse

            //Affichage
            AttText.GetComponent<Text>().text = Att.ToString("F3");                 //On affiche la valeur d'attaque avec 3 chiffres derrière la virgule
            AttSpdText.GetComponent<Text>().text = AttSpd.ToString("F3");           //On affiche la valeur de vitesse d'attaque avec 3 chiffres derrière la virgule
            SpdText.GetComponent<Text>().text = Spd.ToString("F3");                 //On affiche la valeur de vitesse avec 3 chiffres derrière la virgule
        }
    }
}
