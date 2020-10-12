using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using System.Diagnostics;

public class Player : MonoBehaviour
{
    [SerializeField] Camera m_MainCamera;
    [SerializeField] int m_VerticalSpeed;
    [SerializeField] int m_HorizontalSpeed;
    [SerializeField] float m_fireRate;
    [SerializeField] Bullet m_bulletPrefab;

    Stopwatch stopWatch;

    //Controle du vaisseau
    void PlayerControl()
    {
        //Horizontal
        if(Input.GetAxis("Horizontal") < 0 && 
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
        
        if(Input.GetAxis("Fire1") && m_fireTimer.ElapsedMilliseconds > m_FireRate)
        {
            Bullet bullet = Instantiate(m_BulletPrefab).GetComponent<bullet>();
            bullet.transform.position = transform.position;
            bullet.OnHit += OnBulletHit;

            m_FireTimer.Restart();
        }

    }

    void Awake()
    {
        stopWatch = new Stopwatch();
        stopWatch.Start();
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
}
