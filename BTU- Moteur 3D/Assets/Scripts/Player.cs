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
    [SerializeField] Camera m_MainCamera;
    [SerializeField] float m_VerticalSpeed;
    [SerializeField] float m_HorizontalSpeed;
    [SerializeField] float m_fireRate;
    [SerializeField] GameObject m_bulletPrefab;
    [SerializeField] int health;
    [SerializeField] int numOfHearts;

    public static int damage = 1;
    bool multishot = false;


    [SerializeField] Image[] hearts;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;

    [SerializeField] GameObject EndGameMenu;

    Rigidbody2D rb;
    Stopwatch stopWatch;
    Stopwatch m_fireTimer;

    //Controle du vaisseau
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
        rb = GetComponent<Rigidbody2D>();
        stopWatch = new Stopwatch();
        stopWatch.Start();

        m_fireTimer = new Stopwatch();
        m_fireTimer.Start();
    }

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
    // Start is called before the first frame update
    void Start()
    {
        HealthManagement();

        //On assure que le jeu n'est pas figé et on reset le score à 0
        Time.timeScale = 1f;
        PauseMenuScript.GameIsPaused = false;
        AudioListener.pause = false;
        PlayerScore.Score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerControl();
    }

    public void Damage()
    {
        health--;
        HealthManagement();
        if (health == 0)
        {
            EndGameMenu.SetActive(true);
            Time.timeScale = 0f;
            PauseMenuScript.GameIsPaused = true;
            AudioListener.pause = true;
            Destroy(gameObject);
        }
    }

    public void AddDamage()
    {
        damage++;
    }

    public void AddHealth()
    {
        if (health < numOfHearts)
        {
            health++;
            HealthManagement();
        }
    }

    public void AddMaxHealth()
    {
        if (numOfHearts < 10)
        { 
            numOfHearts++;
            HealthManagement();
        }
    }

    public void AddSpeed()
    {
        m_VerticalSpeed += 0.2f;
        m_HorizontalSpeed += 0.2f;
    }

    public void AddAttackSpeed()
    {
        m_fireRate -= 25;
    }

    public void AddMultishot()
    {
        multishot = true;
    }

    public float GetSpeed()
    {
        return m_HorizontalSpeed;
    }

    public float GetFireSpeed()
    {
        return m_fireRate;
    }
}
