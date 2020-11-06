using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] m_enemy;
    [SerializeField] GameObject[] m_boss;
    [SerializeField] float timeBetweenWaves;
    [SerializeField] float countdown;           //time before first wave
    [SerializeField] float m_spawnRate;

    public static int enemyLeft = 0;

    int waveNumber = 0;
    int waveNumberBis = 0;
    int bossWave = 5;

    void Update()
    {
        if(countdown <= 0f)
        {
            if(enemyLeft==0)
                StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        if(waveNumber < 30)
            waveNumber++;
        waveNumberBis++;
        if (waveNumberBis % bossWave == 0)
        {
            SpawnBoss();
            enemyLeft++;
        }
        else {
            for (int i = 0; i < waveNumber; i++)
            {
                SpawnEnemy(i);
                yield return new WaitForSeconds(m_spawnRate);
            }
        }
        timeBetweenWaves++;
    }

    void SpawnEnemy(int index)
    {
        if(index <= 10)
            Instantiate(m_enemy[(int)Random.Range(0, 1)], new Vector3(Random.Range(-6, 6), 15, 0), Quaternion.identity);
        else if(index <= 20)
            Instantiate(m_enemy[(int)Random.Range(0, m_enemy.Length)], new Vector3(Random.Range(-6, 6), 15, 0), Quaternion.identity);
        else if(index <= 30)
            Instantiate(m_enemy[(int)Random.Range(1, m_enemy.Length)], new Vector3(Random.Range(-6, 6), 15, 0), Quaternion.identity);
        else
            Instantiate(m_enemy[(int)Random.Range(2, m_enemy.Length)], new Vector3(Random.Range(-6, 6), 15, 0), Quaternion.identity);
    }

    void SpawnBoss()
    {
        Instantiate(m_boss[0], new Vector3(0, 15, 0), Quaternion.identity);
    }
}
