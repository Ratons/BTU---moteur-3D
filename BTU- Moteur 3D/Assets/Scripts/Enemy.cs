using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Rigibody of the game object
    Rigidbody2D rb;

    [Tooltip("speed of the enemy")]
    [SerializeField] float m_enemySpeed;

    [Tooltip("health of the enemy")]
    [SerializeField] int m_maxHealth;

    [Tooltip("can the enemy shoot")]
    [SerializeField] bool canShoot;

    [Tooltip("attack speed of the enemy")]
    [SerializeField] int fireRate;

    [Tooltip("prefab of the bullet the enemy shoot")]
    [SerializeField] GameObject bulletPrefab;

    [Tooltip("score that the enemy give on death")]
    [SerializeField] int score;

    [Tooltip("list of boosters prefabs that can spawn when the enemy die")]
    [SerializeField] GameObject[] m_booster;

    // used to increase and decrease the health without changing the starting health of the enemy
    int m_health;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   // set the rigibody
    }

    // enemy basics movements
    void EnemyControl()
    {
        if (this.transform.position.y <= -2)                                            // if the enemy goes out the bottom of the screen
        {
            PlayerScore.Score -= score;                                                 // decrease the player score
            Destroy(this.gameObject);                                                   // destroy the enemy
        }
        else
            transform.Translate(new Vector3(0, -1, 0) * m_enemySpeed * Time.deltaTime); // movement
    }

    void Start()
    {
        m_health = m_maxHealth;                             // instantiate m_health
        if (canShoot)                                       // if the enemy can shoot
            InvokeRepeating("Shoot", fireRate, fireRate);   // // call shoot function "fireRate" seconds after the enemy spawns and every "fireRate" seconds
    }

    // shoot bullets
    void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab).GetComponent<Bullet>();           // create a bullet
        bullet.transform.position = transform.position + new Vector3(0, -1f, 0);    // set the bullet position
    }

    void Update()
    {
        EnemyControl(); // basics movements
    }

    // actions done when the enemy is colliding on other objects
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")                 // if the collider is the player
        {
            col.gameObject.GetComponent<Player>().Damage(); // the player is damaged
            Damage(Player.damage);                          // the enemy is damaged
        }
    }

    // damages dealt to the enemy
    public void Damage(int degats)
    {
        m_health -= degats;   // decrease health of the enemy according to the player damages
        if (m_health <= 0)    // when the enemy health is equal to 0 it dies
            Die();
    }

    // actions done when the enemy dies
    void Die()
    {
        PlayerScore.Score += score;     // increment the player score
        if (Random.Range(0, 100) < 5)   // 5% chance of spawning a booster on death
            BoosterSpawn();
        Destroy(gameObject);            // destroy the enemy
    }

    // create a booster at the enemy death point
    void BoosterSpawn()
    {
        Instantiate(m_booster[(int)Random.Range(0, m_booster.Length)], transform.position, Quaternion.identity);    // create a booster
        // multishot booster is a one time booster so it doesn't spawn anymore after it spawns once
        if (Player.multishot == true)
            Instantiate(m_booster[(int)Random.Range(1, m_booster.Length)], transform.position, Quaternion.identity);
    }

    // enemies get more health each 10 waves
    public void AddHealth()
    {
        m_health ++;    // health increase by 5
    }
}