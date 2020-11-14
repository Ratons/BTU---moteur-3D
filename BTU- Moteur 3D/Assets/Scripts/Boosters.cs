using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boosters : MonoBehaviour
{
    // Rigibody of the game object
    Rigidbody2D rb;

    [Tooltip("speed of the booster")]
    [SerializeField] float m_boosterSpeed;

    [Tooltip("booster effect")]
    [SerializeField] string boosterName;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   // set the rigibody
    }

    // booster basics movements
    void BoosterControl()
    {
        if (this.transform.position.y <= -2)                                                // if the booster goes out the bottom of the screen
            Destroy(this.gameObject);                                                       // destroy the booster
        else
            transform.Translate(new Vector3(0, -1, 0) * m_boosterSpeed * Time.deltaTime);   // movement
    }

    void Start()
    {
        
    }

    void Update()
    {
        BoosterControl();   // basics movements
    }

    // actions done when the booster is colliding on other objects
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")                             // if the collider is the player
        {
            if(boosterName == "damage")                                 // if it's a damage booster
                col.gameObject.GetComponent<Player>().AddDamage();      // increase the player damage
            else if(boosterName == "attackSpeed")                       // if it's an attack speed booster
                col.gameObject.GetComponent<Player>().AddAttackSpeed(); // increase the player attack speed
            else if (boosterName == "health")                           // if it's an healing booster
                col.gameObject.GetComponent<Player>().AddHealth();      // heal the player
            else if (boosterName == "maxHealth")                        // if it's an max health booster
                col.gameObject.GetComponent<Player>().AddMaxHealth();   // increase the maximum health of the player
            else if (boosterName == "speed")                            // if it's a speed booster
                col.gameObject.GetComponent<Player>().AddSpeed();       // increase the player speed
            else if (boosterName == "multishot")                        // if it's a multishot booster
                col.gameObject.GetComponent<Player>().AddMultishot();   // give multishot to the player 
            Destroy(this.gameObject);                                   // destroy the booster
        }
    }
}
