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
    [SerializeField] string[] wavePattern;

    public static int enemyLeft = 0;

    int spawnRateLine = 2;
    int waveNumber = 0;
    int waveNumberBis = 0;
    int bossWave = 5;
    int index;
    int direction = 1;

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
            if(wavePattern[Random.Range(0, wavePattern.Length)] == "sinus")
            {
                for (int i = 0; i < waveNumber; i++)
                {
                    if (i % 6 == 0)
                    {
                        direction *= -1;
                    }
                    SpawnEnemy(waveNumber, (-6*direction) + (2*direction) * (i % 6));
                    yield return new WaitForSeconds(m_spawnRate);
                }
                direction = 1;
            }
            else if (wavePattern[Random.Range(0, wavePattern.Length)] == "line")
            {
                for (int i = 0; i < waveNumber; i++)
                {
                    if(i%7 == 0)
                        yield return new WaitForSeconds(spawnRateLine);
                    SpawnEnemy(waveNumber, -6 + 2 * (i % 7));
                }
            }
            // default : random
            else
            {
                for (int i = 0; i < waveNumber; i++)
                {
                    SpawnEnemy(waveNumber, Random.Range(-6, 6));
                    yield return new WaitForSeconds(m_spawnRate);
                }
            }
        }
        timeBetweenWaves++;
        if (waveNumberBis % 10 == 0)
        {
            for (int l = 0; l < m_enemy.Length; l++)
                m_enemy[l].gameObject.GetComponent<Enemy>().AddHealth();
            for (int l = 0; l < m_boss.Length; l++)
                m_boss[l].gameObject.GetComponent<Boss>().AddHealth();
            for (int l = 0; l < m_minions.Length; l++)
                m_minions[l].gameObject.GetComponent<Boss>().AddHealth();
        }
    }

    void SpawnEnemy(int index, int posX)
    {
        if (index <= 10)
            Instantiate(m_enemy[0], new Vector3(posX, 15, 0), Quaternion.identity);
        else if (index <= 20)
            Instantiate(m_enemy[(int)Random.Range(0, 2)], new Vector3(posX, 15, 0), Quaternion.identity);
        else if (index <= 30)
            Instantiate(m_enemy[(int)Random.Range(0, m_enemy.Length)], new Vector3(posX, 15, 0), Quaternion.identity);
        else if (index <= 40)
            Instantiate(m_enemy[(int)Random.Range(1, m_enemy.Length)], new Vector3(posX, 15, 0), Quaternion.identity);
        else
            Instantiate(m_enemy[(int)Random.Range(2, m_enemy.Length)], new Vector3(posX, 15, 0), Quaternion.identity);
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
            Instantiate(m_minions[0], new Vector3(-3, 15, 0), Quaternion.identity);
            enemyLeft++;
            Instantiate(m_minions[0], new Vector3(3, 15, 0), Quaternion.identity);
            enemyLeft++;
        }
    }
}
