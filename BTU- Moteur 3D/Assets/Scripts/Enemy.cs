using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float m_enemySpeed;
    // Start is called before the first frame update
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
}
