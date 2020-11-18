using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class Boss : MonoBehaviour
{
    // Rigibody of the game object
    Rigidbody2D rb;

    [Tooltip("speed of the boss")]
    [SerializeField] float m_enemySpeed;

    [Tooltip("health of the boss")]
    [SerializeField] int m_maxHealth;

    [Tooltip("attack speed of the boss")]
    [SerializeField] int fireRate;

    [Tooltip("prefab of the bullet the boss shoot")]
    [SerializeField] GameObject bulletPrefab;

    [Tooltip("score that the boss give on death")]
    [SerializeField] int score;

    [Tooltip("list of boosters prefabs that can spawn when the boss die")]
    [SerializeField] GameObject[] m_booster;

    [Tooltip("which boss it is, example: boss1")]
    [SerializeField] string type;

    Stopwatch m_Timer;
    // direction the boss will go on the X axe
    int direction = 1;
    // used to increase and decrease the health without changing the starting health of the boss
    int m_health;
    // buffer health
    public static int tmp = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   // set the rigibody

        m_Timer = new Stopwatch();          // set the timer
        m_Timer.Start();                    // start the timer
    }

    // boss basics movements
    void EnemyControl()
    {
        if (this.transform.position.y <= -2)                                            // if the boss goes out the bottom of the screen
        {
            PlayerScore.Score -= score;                                                 // decrease the player score
            Destroy(this.gameObject);                                                   // destroy the boss
        }
        else
            transform.Translate(new Vector3(0, -1, 0) * m_enemySpeed * Time.deltaTime); // movement
    }

    // boss movements on X axe
    void EnemyControlX()
    {
        if (m_Timer.ElapsedMilliseconds > 5000)                                             // each 5000 milliseconds                                              
        {
            direction *= -1;                                                                // change the direction of the movement
            m_Timer.Restart();                                                              // restart the timer
        }
        transform.Translate(new Vector3(direction, 0, 0) * m_enemySpeed * Time.deltaTime);  // movement
    }

    void Start()
    {
        m_health = m_maxHealth + tmp;           // instantiate m_health
        InvokeRepeating("Shoot", 5, fireRate);  // call shoot function 5 seconds after the boss spawns and every "fireRate" seconds
    }

    // shoot bullets
    void Shoot()
    {
        // first bullet
        Bullet bullet = Instantiate(bulletPrefab).GetComponent<Bullet>();               // create a bullet
        bullet.transform.position = transform.position + new Vector3(-0.5f, -1f, 0);    // set the bullet position

        // second bullet
        Bullet bullet2 = Instantiate(bulletPrefab).GetComponent<Bullet>();
        bullet2.transform.position = transform.position + new Vector3(0.5f, -1f, 0);
    }

    void Update()
    {
        if (type == "boss1")                // if it's the first boss
        {
            if (transform.position.y >= 7)  // while boss position on Y axe is more than 7
                EnemyControl();             // basics movements
        }
        if (type == "minions")              // if it's a minion
        {
            if (transform.position.y >= 6)
                EnemyControl();
        }
        if (type == "boss2")                // if it's the second boss
        {
            if (transform.position.y >= 7)
                EnemyControl();
            else
                EnemyControlX();            // movement on X axe

        }
    }

    // actions done when the boss is colliding on other objects
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")                                 // if the collider is the player
        {
            col.gameObject.GetComponent<Player>().EndgameManagement();      // the player is damaged
            for (int i = 0; i < Player.damage; i++)
                Damage();                                                   // the boss is damaged
        }
    }

    // damages dealt to the boss
    public void Damage()
    {
        m_health--;         // decrease health of the boss
        if (m_health <= 0)  // when the boss health is equal to 0 it dies
            Die();
    }

    // actions done when the boss dies
    void Die()
    {
        PlayerScore.Score += score;     // increment the player score
        if (Random.Range(0, 100) < 50)  // 50% chance of spawning a booster on death
            BoosterSpawn();
        Spawner.enemyLeft --;           // decrease the number of ennmies left during boss wave
        Destroy(gameObject);            // destroy the boss
    }

    // create a booster at the boss death point
    void BoosterSpawn()
    {
        Instantiate(m_booster[(int)Random.Range(0, m_booster.Length)], transform.position, Quaternion.identity);    // create a booster
        // multishot booster is a one time booster so it doesn't spawn anymore after it spawns once
        if(Player.multishot == true)
            Instantiate(m_booster[(int)Random.Range(1, m_booster.Length)], transform.position, Quaternion.identity);
    }

    // bosses get more health each 10 waves
    public void AddHealth()
    {
        tmp += 5;  // health increase by 5
    }
}
