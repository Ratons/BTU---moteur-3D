using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float m_enemySpeed;
    [SerializeField] int m_health;
    [SerializeField] bool canShoot;
    [SerializeField] int fireRate;
    [SerializeField] GameObject bulletPrefab;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void EnemyControl()
    {
        if(this.transform.position.y <= -2)
            Destroy(this.gameObject);
        else
            transform.Translate(new Vector3(0, -1, 0) * m_enemySpeed * Time.deltaTime);
    }

    void Start()
    {
        if(canShoot)
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

    public void Damage()
    {
        m_health--;
        if (m_health == 0)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
