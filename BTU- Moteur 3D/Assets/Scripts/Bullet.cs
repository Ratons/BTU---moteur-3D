using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour

{
    Rigidbody2D rb;
    [SerializeField] int m_BulletSpeed;
    [SerializeField] int m_BulletSize;
    [SerializeField] bool direction;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void BulletControl()
    {
        if(direction==true)
            transform.Translate(new Vector3(0, 1, 0) * m_BulletSpeed * Time.deltaTime);
        if(direction==false)
            transform.Translate(new Vector3(0, -1, 0) * m_BulletSpeed * Time.deltaTime);

        if (transform.position.y > 15) // condition percuter un ennemi
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BulletControl();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Player>().Damage();
            Die();
        }
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<Enemy>().Damage();
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
