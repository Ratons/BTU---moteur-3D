using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Tooltip("list of enemies prefabs")]
    [SerializeField] GameObject[] m_enemy;

    [Tooltip("list of bosses prefabs")]
    [SerializeField] GameObject[] m_boss;

    [Tooltip("list of minions prefabs")]
    [SerializeField] GameObject[] m_minions;

    [Tooltip("time between each waves")]
    [SerializeField] float timeBetweenWaves;

    [Tooltip("countdown before waves")]
    [SerializeField] float countdown;

    [Tooltip("time between the spawn of 2 enemies")]
    [SerializeField] float m_spawnRate;

    [Tooltip("list of waves patterns")]
    [SerializeField] string[] wavePattern;

    // number of enemies left during boss waves
    public static int enemyLeft = 0;

    // spawn rate during line waves
    int spawnRateLine = 2;
    // number of enemies per waves
    int nbEnemies = 0;
    // wave number
    int waveNumber = 0;
    // waves between each boss waves
    int bossWave = 5;
    // direction for the sinus wave
    int direction = 1;

    void Start()
    {
        waveNumber = 0;
        nbEnemies = 0;
        enemyLeft = 0;
    }

    void Update()
    {
        if(countdown <= 0f)                     // if countdown equal 0
        {
            if(enemyLeft==0)                    // if there is no enemies left (only for boss waves)
                StartCoroutine(SpawnWave());    // start spawning
            countdown = timeBetweenWaves;       // reset countdown
        }
        countdown -= Time.deltaTime;            // decrease countdown
    }

    // spawning system
    IEnumerator SpawnWave()
    {
        if(waveNumber < 30)                                                             // if current wave is 30 or less
            nbEnemies++;                                                                // increase number of enemies per wave
        waveNumber++;                                                                   // increase wave number
        
        if (waveNumber % bossWave == 0)                                                 // if it's boss wave
        {
            SpawnBoss(waveNumber);                                                      // spawn boss wave
        }
        else {
            if(wavePattern[Random.Range(0, wavePattern.Length)] == "sinus")             // if it's a sinus wave pattern
            {
                for (int i = 0; i < nbEnemies; i++)
                {
                    if (i % 6 == 0)                                                     // each 6 enemies
                    {
                        direction *= -1;                                                // change the direction for the sinus pattern
                    }
                    SpawnEnemy(nbEnemies, (-6*direction) + (2*direction) * (i % 6));    // spawn enemy
                    yield return new WaitForSeconds(m_spawnRate);                       // wait before spawning the next enemy
                }
                direction = 1;
            }
            else if (wavePattern[Random.Range(0, wavePattern.Length)] == "line")        // if it's a line wave pattern
            {
                for (int i = 0; i < nbEnemies; i++)
                {
                    if(i%7 == 0)
                        yield return new WaitForSeconds(spawnRateLine);
                    SpawnEnemy(nbEnemies, -6 + 2 * (i % 7));
                }
            }
            // default : random
            else
            {
                for (int i = 0; i < nbEnemies; i++)
                {
                    SpawnEnemy(nbEnemies, Random.Range(-6, 6));
                    yield return new WaitForSeconds(m_spawnRate);
                }
            }
        }
        timeBetweenWaves++;                                                             // increase the time between each wave
        if (waveNumber % 10 == 0)                                                       // each 10 waves increase health of all enemies, bosses and minions
        {
            for (int l = 0; l < m_enemy.Length; l++)
                m_enemy[l].gameObject.GetComponent<Enemy>().AddHealth();
            for (int l = 0; l < m_boss.Length; l++)
                m_boss[l].gameObject.GetComponent<Boss>().AddHealth();
            for (int l = 0; l < m_minions.Length; l++)
                m_minions[l].gameObject.GetComponent<Boss>().AddHealth();
        }
    }

    // define which enemy spawn each waves
    void SpawnEnemy(int index, int posX)
    {
        if (index <= 10)                                                                                                // if current wave is 10 or less
            Instantiate(m_enemy[0], new Vector3(posX, 15, 0), Quaternion.identity);                                     // spawn basic enemy
        else if (index <= 20)                                                                                           // if current wave is 20 or less
            Instantiate(m_enemy[(int)Random.Range(0, 2)], new Vector3(posX, 15, 0), Quaternion.identity);               // spawn basic + firing enemies
        else if (index <= 30)                                                                                           // if current wave is 30 or less
            Instantiate(m_enemy[(int)Random.Range(0, m_enemy.Length)], new Vector3(posX, 15, 0), Quaternion.identity);  // spawn all enemies
        else                                                                                  
            Instantiate(m_enemy[(int)Random.Range(1, m_enemy.Length)], new Vector3(posX, 15, 0), Quaternion.identity);  // spawn firing + tank enemies
    }
    
    // spawn boss wave
    void SpawnBoss(int wave)
    {
        if (wave == 5)                                                              // if current wave is 5
        {
            Instantiate(m_boss[0], new Vector3(0, 15, 0), Quaternion.identity);     // spawn boss
            enemyLeft++;                                                            // increase number of enemies left
        }
        else if (wave == 10)                                                        // if current wave is 10
        {
            Instantiate(m_boss[0], new Vector3(0, 15, 0), Quaternion.identity);
            enemyLeft++;
            Instantiate(m_minions[0], new Vector3(-3, 15, 0), Quaternion.identity); // spawn minions
            enemyLeft++;
            Instantiate(m_minions[0], new Vector3(3, 15, 0), Quaternion.identity);
            enemyLeft++;
        }
        else if (wave == 15)                                                        // if current wave is 15
        {
            Instantiate(m_boss[1], new Vector3(4, 15, 0), Quaternion.identity);
            enemyLeft++;
        }
        else if (wave == 20)                                                        // if current wave is 20
        {
            Instantiate(m_boss[1], new Vector3(4, 15, 0), Quaternion.identity);
            enemyLeft++;
            Instantiate(m_minions[0], new Vector3(-3, 15, 0), Quaternion.identity);
            enemyLeft++;
            Instantiate(m_minions[0], new Vector3(3, 15, 0), Quaternion.identity);
            enemyLeft++;
        }
        else if (wave == 25)                                                        // if current wave is 25
        {
            Instantiate(m_boss[0], new Vector3(4, 15, 0), Quaternion.identity);
            enemyLeft++;
            Instantiate(m_boss[0], new Vector3(-4, 15, 0), Quaternion.identity);
            enemyLeft++;
        }
        else if (wave == 30)                                                        // if current wave is 30
        {
            Instantiate(m_boss[0], new Vector3(4, 15, 0), Quaternion.identity);
            enemyLeft++;
            Instantiate(m_boss[0], new Vector3(-4, 15, 0), Quaternion.identity);
            enemyLeft++;
            Instantiate(m_minions[0], new Vector3(0, 15, 0), Quaternion.identity);
            enemyLeft++;
        }
        else if (wave == 35)                                                        // if current wave is 35
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
