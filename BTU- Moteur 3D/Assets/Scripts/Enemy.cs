using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float m_enemySpeed;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyControl();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "player")
        {
            col.gameObject.GetComponent<Player>().Damage();
            Die();
        }
        if(col.gameObject.tag == "bullet")
        {
            //add score
            col.gameObject.GetComponent<Bullet>().Die();
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
