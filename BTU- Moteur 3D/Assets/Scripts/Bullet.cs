using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour

{
    [SerializeField] int m_BulletSpeed;
    [SerializeField] int m_BulletSize;

    void BulletControl()
    {
        transform.Translate(new Vector3(0, 1, 0) * m_BulletSpeed * Time.deltaTime);

        if (transform.position.y > Screen.height +1) // condition percuter un ennemi
        {
            Destroy(this);
        }
    }

    void OnHit()
    {

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
}
