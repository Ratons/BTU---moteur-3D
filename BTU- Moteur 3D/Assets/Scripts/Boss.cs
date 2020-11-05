using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float m_enemySpeed;
    [SerializeField] int m_health;
    [SerializeField] int fireRate;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int score;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void EnemyControl()
    {
        if (this.transform.position.y <= -2)
            Destroy(this.gameObject);
        else
            transform.Translate(new Vector3(0, -1, 0) * m_enemySpeed * Time.deltaTime);
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
        if (transform.position.y >= 7)
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

    public void Damage()
    {
        m_health--;
        if (m_health == 0)
            Die();
    }

    void Die()
    {
        PlayerScore.Score += score;
        Destroy(gameObject);
    }
}
