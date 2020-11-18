using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using System.Diagnostics;
using static Bullet;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Tooltip("main camera prefab")]
    [SerializeField] Camera m_MainCamera;

    [Tooltip("player vertical speed")]
    [SerializeField] float m_VerticalSpeed;

    [Tooltip("player horizontal speed")]
    [SerializeField] float m_HorizontalSpeed;

    [Tooltip("player attack speed")]
    [SerializeField] float m_fireRate;

    [Tooltip("bullet prefab")]
    [SerializeField] GameObject m_bulletPrefab;

    [Tooltip("player health")]
    [SerializeField] int health;

    [Tooltip("hearts number")]
    [SerializeField] int numOfHearts;

    [Tooltip("UI life")]
    [SerializeField] Image[] hearts;

    [Tooltip("FullHeart sprite")]
    [SerializeField] Sprite fullHeart;

    [Tooltip("EmptyHeart sprite")]
    [SerializeField] Sprite emptyHeart;

    [Tooltip("GameOver Screen")]
    [SerializeField] GameObject EndGameMenu;

    // player damage
    public static int damage = 1;
    // is multishot activated
    public static bool multishot = false;

    Rigidbody2D rb;
    Stopwatch stopWatch;
    Stopwatch m_fireTimer;

    /*on utilise les inputs entrés dans les ProjectSettings,  
     si une touche qui modifie un axe horizontal/vertical
    etst appuyée, on calcule la translation de notre joueur
    avec la valeur de cette touche selon l'axe (-1 ou 1)*/
    void PlayerControl()
    {
        //Horizontal
        if (Input.GetAxis("Horizontal") < 0 &&
            transform.position.x > -6)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(new Vector3(horizontalInput, 0, 0) * m_HorizontalSpeed * Time.deltaTime);
        }
        if (Input.GetAxis("Horizontal") > 0 &&
            transform.position.x < 6)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(new Vector3(horizontalInput, 0, 0) * m_HorizontalSpeed * Time.deltaTime);
        }
        //Vertical
        if (Input.GetAxis("Vertical") < 0 &&
            m_MainCamera.WorldToScreenPoint(transform.position).y > m_VerticalSpeed * Time.deltaTime)
        {
            float verticalInput = Input.GetAxis("Vertical");
            transform.Translate(new Vector3(0, verticalInput, 0) * m_HorizontalSpeed * Time.deltaTime);
        }
        if (Input.GetAxis("Vertical") > 0 &&
                transform.position.y < 9)
        {
            float verticalInput = Input.GetAxis("Vertical");
            transform.Translate(new Vector3(0, verticalInput, 0) * m_HorizontalSpeed * Time.deltaTime);
        }
        
        if(Input.GetAxis("Fire1") > 0 && m_fireTimer.ElapsedMilliseconds > m_fireRate)
        {
            if(multishot == false)
            {
                Bullet bullet = Instantiate(m_bulletPrefab).GetComponent<Bullet>();
                bullet.transform.position = transform.position + new Vector3(0, 1, 0);
            }
            else
            {
                Bullet bullet = Instantiate(m_bulletPrefab).GetComponent<Bullet>();
                bullet.transform.position = transform.position + new Vector3(0.5f, 1, 0);

                Bullet bullet2 = Instantiate(m_bulletPrefab).GetComponent<Bullet>();
                bullet2.transform.position = transform.position + new Vector3(-0.5f, 1, 0);
            }

            m_fireTimer.Restart();
        }

    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   // set the rigibody
        stopWatch = new Stopwatch();        // set the stopwatch
        stopWatch.Start();                  // start the stopwatch

        m_fireTimer = new Stopwatch();      // set the fireTimer
        m_fireTimer.Start();                // start the fireTimer
    }


    //Fonction pour gérer l'aspect de la "barre" de vie en jeu
    void HealthManagement()
    {
        if (health > numOfHearts) //Si la vie dépasse le maximum possible, on la réajuste au maximum de vie possible
        {
            health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)     //boucle pour gérer l'aspect des coeurs
        {
            if (i < health)                         //tous les coeurs sous le seuil de vie obtiennent l'apparence d
            {
                hearts[i].sprite = fullHeart;
            }
            else                                    //Sinon, ils ont l'apparence du coeur vide
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < numOfHearts)                    //numOfHearts corresponds à la vie maximum du joueur actuellement
            {
                hearts[i].enabled = true;           //Nous avons prévu une liste de 10 coeurs max donc nous n'affichons que les coeurs dont le joueur dispose actuellement
            }
            else
            {
                hearts[i].enabled = false;          //et les autres ne sont pas apparents
            }
        }
    }

    void Start()
    {
        HealthManagement();
        damage = 1;
        multishot = false;

        Time.timeScale = 1f;                    // On autorise le temps du jeu à découler normalement
        PauseMenuScript.GameIsPaused = false;   
        AudioListener.pause = false;            // On enleve la pause de la musique
        PlayerScore.Score = 0;                  // set score to 0
    }

    void Update()
    {
        PlayerControl();    // basics movements
    }

    // damages to the player
    public void Damage()
    {
        health--;                   // decrease health
        HealthManagement();         // Fonction réalisant la gestion de l'aspect visuel de la vie
        if (health == 0)            //if player health equal 0 it dies
        {
            EndgameManagement();    // Quand le joueur meurt, on appel la fonction qui réalise le gameOver
        }
    }

    // TO DO
    public void EndgameManagement()
    {
        health = 0;                                 //Pour ne pas afficher les coeurs
        HealthManagement();
        EndGameMenu.SetActive(true);                //On affiche l'écran de fin
        Time.timeScale = 0f;                        //On arrete le jeu
        PauseMenuScript.GameIsPaused = true;        
        AudioListener.pause = true;                 //On arrete la musique
        if (PlayerPrefs.HasKey("BestRecord"))       //On regarde s'il y a déjà un record
        {
            if (PlayerScore.Score > PlayerPrefs.GetInt("BestRecord")) //si le nouveau score est plus grand, on remplace l'ancien record par le nouveau
                PlayerPrefs.SetInt("BestRecord", PlayerScore.Score);
        }
        else                                        //S'il n'y a pas de record, on enregistre ce score comme record
            PlayerPrefs.SetInt("BestRecord", PlayerScore.Score);
        Destroy(gameObject);                        //et enfin on détruit l'actor player
    }

    // increse damages
    public void AddDamage()
    {
        damage++;
    }

    // heal
    public void AddHealth()
    {
        if (health < numOfHearts)
        {
            health++;
            HealthManagement();
        }
    }

    // increase maximum health
    public void AddMaxHealth()
    {
        if (numOfHearts < 10)
        { 
            numOfHearts++;
            
        }
        health++;
        HealthManagement();
    }

    // increase horizontal and vertical speed
    public void AddSpeed()
    {
        if (m_VerticalSpeed < 10)
        {
            m_VerticalSpeed += 0.2f;
            m_HorizontalSpeed += 0.2f;
        }
    }

    // increase attack speed
    public void AddAttackSpeed()
    {
        if(m_fireRate > 100)
            m_fireRate -= 25;
    }

    // activate multishot
    public void AddMultishot()
    {
        multishot = true;  
    }

    // horizontalSpeed getter
    public float GetSpeed()
    {
        return m_HorizontalSpeed;
    }

    // fireRate getter 
    public float GetFireSpeed()
    {
        return m_fireRate;
    }
}
