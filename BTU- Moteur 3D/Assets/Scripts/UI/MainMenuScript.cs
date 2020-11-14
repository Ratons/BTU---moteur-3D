using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //On charge la scene avec le jeu
    }

    public void QuitGame()
    {
       Debug.Log("Quit");
       Application.Quit();
    }
}
