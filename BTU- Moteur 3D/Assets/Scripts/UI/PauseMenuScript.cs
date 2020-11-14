using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;

    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))// ON/OFF du menu de pause
        {
            if(GameIsPaused)
            {
                Continue();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);    //Afficher le menu
        Time.timeScale = 0f;            //Mettre le jeu en pause
        GameIsPaused = true;            //Modifier le booléen pour la gestion de l'UI
    }

    public void Continue()
    {
        PauseMenuUI.SetActive(false);   //Enelever le menu
        Time.timeScale = 1f;            //Enlever la pause du jeu
        GameIsPaused = false;           
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);      //Aller au MenuPrincipal
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
