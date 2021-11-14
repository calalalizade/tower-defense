using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region SINGLETON
    public static EnemySpawner Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<GameObject> activeEnemies;

    [SerializeField] Transform towers;
    [SerializeField] List<GameObject> enemies;

    [SerializeField] float spawnRate;

    bool canSpawn = true;

    int waveIndex = 1;
    int numberOfEnemies;

    float MaxHealth;
    float damage;

    private void Start()
    {
        activeEnemies = new List<GameObject>();

        numberOfEnemies = Random.Range(waveIndex, waveIndex + 10);
    }

    private void Update()
    {
        if(activeEnemies.Count == 0 && !canSpawn)
        {
            waveIndex++;
            IncreaseDiff();
            GameManager.Instance.SetLastWaveIndex(waveIndex);
            GameManager.Instance.GainHealth();
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
            //Spawn Enemies
            Spawn(enemies[randomEnemy]);

            numberOfEnemies--;
            if(numberOfEnemies == 0)
            {
                canSpawn = false;
            }
        }
    }
    private void Spawn(GameObject go)
    {
        var spawnedEnemy = Pooling.Instance.ActivateObject(go.tag);
        spawnedEnemy.SetActive(true);
        spawnedEnemy.transform.position = transform.position;

        activeEnemies.Add(spawnedEnemy);
    }

    public void StartWave()
    {
        GameManager.Instance.isStarted = true;
        StartCoroutine(SpawnDelay());

        foreach (Transform child in towers)
            child.gameObject.SetActive(false);
    }

    private void IncreaseDiff()
    {
        int rnd = Random.Range(0, 2);
        switch (rnd)
        {
            case 0:
                EnemyBehavior.healthAdd += 5;
                break;
            case 1:
                EnemyBehavior.damageAdd += 5;
                break;
            default:
                break;
        }
    }
}