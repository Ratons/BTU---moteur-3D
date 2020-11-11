using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class Boss : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float m_enemySpeed;
    [SerializeField] int m_health;
    [SerializeField] int fireRate;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int score;
    [SerializeField] GameObject[] m_booster;
    [SerializeField] string type;

    Stopwatch m_fireTimer;
    int direction = 1;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        m_fireTimer = new Stopwatch();
        m_fireTimer.Start();
    }

    void EnemyControl()
    {
        if (this.transform.position.y <= -2)
            Destroy(this.gameObject);
        else
            transform.Translate(new Vector3(0, -1, 0) * m_enemySpeed * Time.deltaTime);
    }

    void EnemyControlX()
    {
        if (m_fireTimer.ElapsedMilliseconds > 5000)
        {
            direction *= -1;
            m_fireTimer.Restart();
        }
        transform.Translate(new Vector3(direction, 0, 0) * m_enemySpeed * Time.deltaTime);
    }

    void Start()
    {
        InvokeRepeating("Shoot", 5, fireRate);
    }

    void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
        bullet.transform.position = transform.position + new Vector3(-0.5f, -1f, 0);
        //bullet.transform.rotation = transform.rotation + new Vector3(-45, 0, 0);

        Bullet bullet2 = Instantiate(bulletPrefab).GetComponent<Bullet>();
        bullet2.transform.position = transform.position + new Vector3(0.5f, -1f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (type == "boss1")
        {
            if (transform.position.y >= 7)
                EnemyControl();
        }
        if (type == "minions")
        {
            if (transform.position.y >= 6)
                EnemyControl();
        }
        if (type == "boss2")
        {
            if (transform.position.y >= 7)
                EnemyControl();
            else
                EnemyControlX();

        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Player>().Damage();
            Damage(Player.damage);
        }
    }

    public void Damage(int degats)
    {
        m_health-=degats;
        if (m_health <= 0)
            Die();
    }

    void Die()
    {
        PlayerScore.Score += score;
        if (Random.Range(0, 100) < 50)
            BoosterSpawn();
        Spawner.enemyLeft --;
        Destroy(gameObject);
    }

    void BoosterSpawn()
    {
        Instantiate(m_booster[(int)Random.Range(0, m_booster.Length)], transform.position, Quaternion.identity);
    }
}
