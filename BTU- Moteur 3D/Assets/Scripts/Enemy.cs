using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float m_enemySpeed;
    [SerializeField] int m_maxHealth;
    [SerializeField] bool canShoot;
    [SerializeField] int fireRate;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int score;
    [SerializeField] GameObject[] m_booster;

    int m_health;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void EnemyControl()
    {
        if (this.transform.position.y <= -2)
        {
            PlayerScore.Score -= score;
            Destroy(this.gameObject);
        }
        else
            transform.Translate(new Vector3(0, -1, 0) * m_enemySpeed * Time.deltaTime);
    }

    void Start()
    {
        m_health = m_maxHealth;
        if (canShoot)
            InvokeRepeating("Shoot", fireRate, fireRate);
    }

    void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
        bullet.transform.position = transform.position + new Vector3(0, -1f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        EnemyControl();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Player>().Damage();
            Die();
        }
    }

    public void Damage(int degats)
    {
        m_health -= degats;
        if (m_health <= 0)
            Die();
    }

    void Die()
    {
        PlayerScore.Score += score;
        if (Random.Range(0, 100) < 5)
            BoosterSpawn();
        Destroy(gameObject);
    }

    void BoosterSpawn()
    {
        Instantiate(m_booster[(int)Random.Range(0, m_booster.Length)], transform.position, Quaternion.identity);
        if (Player.multishot == true)
            Instantiate(m_booster[(int)Random.Range(1, m_booster.Length)], transform.position, Quaternion.identity);
    }

    public void AddHealth()
    {
        m_health ++;
    }
}