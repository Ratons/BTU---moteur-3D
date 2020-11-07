using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boosters : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float m_boosterSpeed;
    [SerializeField] string boosterName;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void BoosterControl()
    {
        if (this.transform.position.y <= -2)
            Destroy(this.gameObject);
        else
            transform.Translate(new Vector3(0, -1, 0) * m_boosterSpeed * Time.deltaTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BoosterControl();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if(boosterName == "damage")
                col.gameObject.GetComponent<Player>().AddDamage();
            else if(boosterName == "attackSpeed")
                col.gameObject.GetComponent<Player>().AddAttackSpeed();
            else if (boosterName == "health")
                col.gameObject.GetComponent<Player>().AddHealth();
            else if (boosterName == "maxHealth")
                col.gameObject.GetComponent<Player>().AddMaxHealth();
            else if (boosterName == "speed")
                col.gameObject.GetComponent<Player>().AddSpeed();
            else if (boosterName == "multishot")
                col.gameObject.GetComponent<Player>().AddMultishot();
            Destroy(this.gameObject);
        }
    }
}
