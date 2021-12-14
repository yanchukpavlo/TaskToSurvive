using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] [Min(1)] int maxWave = 5;
    [SerializeField] Transform[] zones;
    [SerializeField] int addEnemyToSpawn = 2;
    [SerializeField] float timeBetweenSpawn = 1;

    int enemyAmount;
    int currentEnemyAmount;
    int currentWave;

    private void Awake()
    {
        enemyAmount = addEnemyToSpawn;
        currentEnemyAmount = enemyAmount;
    }

    private void Start()
    {
        EventsManager.Instance.onEnemyDie += EnemyDestroy;
        StartCoroutine(SpawnLoop(5));
    }

    private void OnDestroy()
    {
        EventsManager.Instance.onEnemyDie -= EnemyDestroy;
    }

    void EnemyDestroy()
    {
        currentEnemyAmount--;
        if (currentEnemyAmount <= 0)
        {
            enemyAmount += addEnemyToSpawn;
            currentEnemyAmount = enemyAmount;
            StartCoroutine(SpawnLoop(5f));
        }
    }

    void SpawnEnemy()
    {
        ObjectPooler.Instance.GetFromPool(PoolType.ZombieDefault, GetRandomSpawnPoint(), Quaternion.identity);
    }

    Vector3 GetRandomSpawnPoint()
    {
        Vector3 vector = Vector3.zero;
        Transform zone = zones[Random.Range(0, zones.Length)];
        float x = zone.localScale.x / 2;
        float z = zone.localScale.z / 2;
        vector = new Vector3(Random.Range(-x, x), 0, Random.Range(-z, z)) + zone.position;
        return vector;
    }

    IEnumerator SpawnLoop(float timerToSpawn)
    {
        if (currentWave != maxWave)
        {
            yield return new WaitForSeconds(timerToSpawn);
            currentWave++;
            UIManager.Instance.SetShowText($"Wave {currentWave}");
            yield return new WaitForSeconds(1);
            for (int i = 0; i < currentEnemyAmount; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(timeBetweenSpawn);
            }
        }
        else
        {
            UIManager.Instance.SetShowText("You win!");
        }
    }
}
