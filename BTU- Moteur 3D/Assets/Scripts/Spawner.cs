using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] m_enemy;
    [SerializeField] GameObject[] m_boss;
    [SerializeField] GameObject[] m_minions;
    [SerializeField] float timeBetweenWaves;
    [SerializeField] float countdown;           //time before waves
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
            SpawnBoss(waveNumberBis);
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
        if (index <= 10)
            Instantiate(m_enemy[0], new Vector3(Random.Range(-6, 6), 15, 0), Quaternion.identity);
        else if (index <= 20)
            Instantiate(m_enemy[(int)Random.Range(0, 2)], new Vector3(Random.Range(-6, 6), 15, 0), Quaternion.identity);
        else if (index <= 30)
            Instantiate(m_enemy[(int)Random.Range(0, m_enemy.Length)], new Vector3(Random.Range(-6, 6), 15, 0), Quaternion.identity);
        else if (index <= 40)
            Instantiate(m_enemy[(int)Random.Range(1, m_enemy.Length)], new Vector3(Random.Range(-6, 6), 15, 0), Quaternion.identity);
        else
            Instantiate(m_enemy[(int)Random.Range(2, m_enemy.Length)], new Vector3(Random.Range(-6, 6), 15, 0), Quaternion.identity);
    }

    void SpawnBoss(int wave)
    {
        if (wave == 5)
        {
            Instantiate(m_boss[0], new Vector3(0, 15, 0), Quaternion.identity);
            enemyLeft++;
        }
        else if (wave == 10)
        {
            Instantiate(m_boss[0], new Vector3(0, 15, 0), Quaternion.identity);
            enemyLeft++;
            Instantiate(m_minions[0], new Vector3(-3, 15, 0), Quaternion.identity);
            enemyLeft++;
            Instantiate(m_minions[0], new Vector3(3, 15, 0), Quaternion.identity);
            enemyLeft++;
        }
        else if (wave == 15)
        {
            Instantiate(m_boss[1], new Vector3(4, 15, 0), Quaternion.identity);
            enemyLeft++;
        }
        else if (wave == 20)
        {
            Instantiate(m_boss[1], new Vector3(4, 15, 0), Quaternion.identity);
            enemyLeft++;
            Instantiate(m_minions[0], new Vector3(-3, 15, 0), Quaternion.identity);
            enemyLeft++;
            Instantiate(m_minions[0], new Vector3(3, 15, 0), Quaternion.identity);
            enemyLeft++;
        }
        else if (wave == 25)
        {
            Instantiate(m_boss[0], new Vector3(4, 15, 0), Quaternion.identity);
            enemyLeft++;
            Instantiate(m_boss[0], new Vector3(-4, 15, 0), Quaternion.identity);
            enemyLeft++;
        }
        else if (wave == 30)
        {
            Instantiate(m_boss[0], new Vector3(4, 15, 0), Quaternion.identity);
            enemyLeft++;
            Instantiate(m_boss[0], new Vector3(-4, 15, 0), Quaternion.identity);
            enemyLeft++;
            Instantiate(m_minions[0], new Vector3(0, 15, 0), Quaternion.identity);
            enemyLeft++;
        }
        else if (wave == 35)
        {
            Instantiate(m_boss[1], new Vector3(4, 15, 0), Quaternion.identity);
            enemyLeft++;
            Instantiate(m_boss[1], new Vector3(4, 22.5f, 0), Quaternion.identity);
            enemyLeft++;
        }
        else
        {
            Instantiate(m_boss[0], new Vector3(0, 15, 0), Quaternion.identity);
            enemyLeft++;
            Instantiate(m_boss[1], new Vector3(4, 15, 0), Quaternion.identity);
            enemyLeft++;
        }
    }
}
