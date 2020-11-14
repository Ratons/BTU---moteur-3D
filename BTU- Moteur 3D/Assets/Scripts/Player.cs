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

    // TO DO
    [SerializeField] Image[] hearts;

    // TO DO
    [SerializeField] Sprite fullHeart;

    // TO DO
    [SerializeField] Sprite emptyHeart;

    // TO DO
    [SerializeField] GameObject EndGameMenu;

    // player damage
    public static int damage = 1;
    // is multishot activated
    public static bool multishot = false;

    Rigidbody2D rb;
    Stopwatch stopWatch;
    Stopwatch m_fireTimer;

    //TO DO
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
                m_MainCamera.WorldToScreenPoint(transform.position).y < Screen.height - m_VerticalSpeed * Time.deltaTime)
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

    // TO DO
    void HealthManagement()
    {
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    void Start()
    {
        HealthManagement();

        Time.timeScale = 1f;                    // TO DO On assure que le jeu n'est pas figé et on reset le score à 0
        PauseMenuScript.GameIsPaused = false;   // TO DO
        AudioListener.pause = false;            // TO DO
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
        HealthManagement();         // TO DO
        if (health == 0)            //if player health equal 0 it dies
        {
            EndgameManagement();    // TO DO
        }
    }

    // TO DO
    void EndgameManagement()
    {
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
            HealthManagement();
        }
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
