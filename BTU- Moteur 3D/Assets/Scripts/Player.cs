using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using System.Diagnostics;
using static Bullet;

public class Player : MonoBehaviour
{
    [SerializeField] Camera m_MainCamera;
    [SerializeField] int m_VerticalSpeed;
    [SerializeField] int m_HorizontalSpeed;
    [SerializeField] float m_fireRate;
    [SerializeField] GameObject m_bulletPrefab;
    [SerializeField] int health;

    Rigidbody2D rb;
    Stopwatch stopWatch;
    Stopwatch m_fireTimer;
    int m_score = 0;

    //Controle du vaisseau
    void PlayerControl()
    {
        //Horizontal
        if (Input.GetAxis("Horizontal") < 0 && 
            m_MainCamera.WorldToScreenPoint(transform.position).x > m_HorizontalSpeed * Time.deltaTime)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(new Vector3(horizontalInput, 0, 0) * m_HorizontalSpeed * Time.deltaTime);
        }
        if (Input.GetAxis("Horizontal") > 0 &&
            m_MainCamera.WorldToScreenPoint(transform.position).x < Screen.width - m_HorizontalSpeed * Time.deltaTime)
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

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        PlayerControl();
    }

    public void Damage()
    {
        health--;
        if (health == 0)
            Destroy(gameObject);
    }
}
