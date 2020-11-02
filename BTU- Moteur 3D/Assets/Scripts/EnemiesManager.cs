using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] Enemy m_enemy;
    [SerializeField] float m_spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawner());
    }

    IEnumerator EnemySpawner()
    {
        while (true)
        {
            //SpawnEnemy
            if (Application.isPlaying)
                Instantiate(m_enemy, this.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(m_spawnTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
