using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] GameObject[] m_enemy;
    [SerializeField] float m_spawnRate;

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
                Instantiate(m_enemy[(int)Random.Range(0, m_enemy.Length)], new Vector3(Random.Range(-5, 6), 15, 0), Quaternion.identity);
            yield return new WaitForSeconds(m_spawnRate);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
