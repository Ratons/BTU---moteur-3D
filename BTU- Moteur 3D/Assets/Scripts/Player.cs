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
    [SerializeField] int m_VerticalSpeed;
    [SerializeField] int m_HorizontalSpeed;
    [SerializeField] float m_fireRate;
    [SerializeField] GameObject m_bulletPrefab;
    [SerializeField] int health;
    [SerializeField] int numOfHearts;

    public static int damage = 1;


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

            Bullet bullet = Instantiate(m_bulletPrefab).GetComponent<Bullet>();
            bullet.transform.position = transform.position + new Vector3(0,1,0);

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
            Destroy(gameObject);
        }
    }

    public void AddDamage()
    {
        damage++;
    }
}
