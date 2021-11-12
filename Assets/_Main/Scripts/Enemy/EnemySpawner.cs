using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform towers;
    [SerializeField] List<GameObject> enemies;
    [SerializeField] List<Transform> spawnPoints;

    [SerializeField] float spawnRate;

    bool canSpawn = true;

    int waveIndex = 1;
    int numberOfEnemies;

    private void Start()
    {
        // initialize
        EnemyBehavior.damageAdd = 0;
        EnemyBehavior.healthAdd = 0;

        numberOfEnemies = Random.Range(waveIndex, waveIndex + 10);
    }

    private void Update()
    {
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(totalEnemies.Length == 0 && !canSpawn)
        {
            waveIndex++;
            IncreaseDiff();
            GameManager.Instance.SetLastWaveIndex(waveIndex);
            numberOfEnemies = Random.Range(waveIndex, waveIndex + 10);

            canSpawn = true;
        }
    }

    private IEnumerator SpawnDelay()
    {
        SpawnEnemy();
        
        yield return new WaitForSeconds(spawnRate);
        StartCoroutine(SpawnDelay());
    }

    void SpawnEnemy()
    {
        if (canSpawn)
        {
            // Randomize Enemies
            int randomEnemy = Random.Range(0, enemies.Count);
            // Randomize SpawnPoints
            int randomSpawnPoint = Random.Range(0, spawnPoints.Count);
            //Spawn Enemies
            GameObject clone = Instantiate(enemies[randomEnemy], spawnPoints[randomSpawnPoint].position, Quaternion.identity);

            numberOfEnemies--;
            if(numberOfEnemies == 0)
            {
                canSpawn = false;
            }
        }
    }

    public void StartWave()
    {
        GameManager.Instance.isStarted = true;
        StartCoroutine(SpawnDelay());

        foreach (Transform child in towers)
            child.gameObject.SetActive(false);
    }

    public void IncreaseDiff()
    {
        int rnd = Random.Range(0, 2);
        switch (rnd)
        {
            case 0:
                EnemyBehavior.damageAdd += Random.Range(1,6);
                break;
            case 1:
                EnemyBehavior.healthAdd += Random.Range(1,16);
                break;
            default:
                break;
        }
    }
}