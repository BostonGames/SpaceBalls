using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject powerup0Prefab;
    [SerializeField] GameObject powerup1Prefab;
    [SerializeField] float startDelay = 0.5f;
    [SerializeField] float spawnMinSecs = 0.5f;
    [SerializeField] float spawnMaxSecs = 18.5f;
    private float spawnEveryXSec;

    public int enemyCount;
    [SerializeField] int waveNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        spawnEveryXSec = Random.Range(spawnMinSecs, spawnMaxSecs);

        SpawnEnemyWave(waveNumber);
        Instantiate(powerup0Prefab, GenerateSpawnPos(), powerup0Prefab.transform.rotation);
        InvokeRepeating("SpawnPowerup1", startDelay, spawnEveryXSec);

        // old way of spawning:
        //InvokeRepeating("InstantiateEnemy", startDelay, spawnEveryXSec);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        //makes the enemy spawn count go up one every wave
        if(enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            SpawnPowerup0();
        }
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPos(), enemyPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPos()
    {
        float spawnPosX = Random.Range(-5.5f, 5.5f);
        float spawnPosZ = Random.Range(-7.0f, 7.0f);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    private void SpawnPowerup0()
    {
        Instantiate(powerup0Prefab, GenerateSpawnPos(), powerup0Prefab.transform.rotation);
    }

    private void SpawnPowerup1()
    {
        Instantiate(powerup1Prefab, GenerateSpawnPos(), powerup0Prefab.transform.rotation);
    }
}
