using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour

{
    // Rigibody of the game object
    Rigidbody2D rb;

    [Tooltip("speed of the bullet")]
    [SerializeField] int m_BulletSpeed;

    [Tooltip("size of the bullet")]
    [SerializeField] int m_BulletSize;

    [Tooltip("direction of the bullet")]
    [SerializeField] bool direction;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   // set the rigibody
    }

    // bullet basics movements
    void BulletControl()
    {
        if(direction==true)                                                                 // shoot up
            transform.Translate(new Vector3(0, 1, 0) * m_BulletSpeed * Time.deltaTime);     // movement
        if(direction==false)                                                                // shoot down
            transform.Translate(new Vector3(0, -1, 0) * m_BulletSpeed * Time.deltaTime);    

        if (transform.position.y > 11 || transform.position.y < -5)                         // if the bullet goes out of the top of the screen
        {
            Destroy(this.gameObject);                                                       // destroy the bullet
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        BulletControl();    // basics movements
    }

    // actions done when the bullet is colliding on other objects
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")                                 // if the collider is the player
        {
            col.gameObject.GetComponent<Player>().Damage();                 // the player is damaged
            Die();                                                          // the bullet dies
        }
        else if (col.gameObject.tag == "Enemy")                             // if the collider is an enemy
        {
            for(int i = 0; i<Player.damage; i++)
                col.gameObject.GetComponent<Enemy>().Damage();              // the enemy is damaged
            Die();
        }
        else if (col.gameObject.tag == "Boss")                              // if the collider is a boss
        {
            for (int i = 0; i < Player.damage; i++)
                col.gameObject.GetComponent<Boss>().Damage();  // the boss is damaged
            Die();
        }
    }

    // actions done when the bullet dies
    public void Die()
    {
        Destroy(gameObject);    // destroy the bullet
    }
}
