using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Spawn points")]
    [SerializeField] private Transform[] spawnPoints;
    [Header("Bosses")]
    [SerializeField] private GameObject[] bosses;
    [Header("Enemies")]
    [SerializeField] private GameObject[] enemies;

    private int waveCount = 0;
    private int allEnemiesKilled;
    private int enemiesCount;
    private float timeAlive;
    private float timeBreak = 5f;
    private float difficultyMultiplier = 1f;

    private void Start()
    {
        StartNextWave();
    }
    private void Update()
    {
        timeAlive += Time.deltaTime;
    }
    private Transform SpawnPos()
    {
        if (spawnPoints.Length == 0) return null;
        int indexSpawn = Random.Range(0, spawnPoints.Length);
        return spawnPoints[indexSpawn];
    }

    private GameObject GetRandomEnemyFromAvailable()
    {
        int availableTypes = Mathf.Min(waveCount, enemies.Length);

        int randomIndex = Random.Range(0, availableTypes);

        return enemies[randomIndex];
    }
    private IEnumerator SpawnWaveRoutine()
    {
        int countToSpawn = 5 + waveCount * 2;
        enemiesCount = countToSpawn;

        if(waveCount %10 == 0)
        {
            int bossesIndex = (waveCount / 10 -1) % bosses.Length;
            Instantiate(bosses[bossesIndex], SpawnPos().position, Quaternion.identity);
            enemiesCount++;
        }
        for(int i = 0; i< countToSpawn; i++)
        {
            GameObject enemyPrefab = GetRandomEnemyFromAvailable();
            Instantiate(enemyPrefab, SpawnPos().position, Quaternion.identity);

            yield return new WaitForSeconds(0.5f);
        }
    }
    private void StartNextWave()
    {
        waveCount++;
        Debug.Log("Start new wave "+ waveCount);
        StartCoroutine(SpawnWaveRoutine());
    }
    public void EnemyDied()
    {
        enemiesCount--;
        allEnemiesKilled++;
        if(enemiesCount <= 0)
        {
            Debug.Log("Wave cleaned");
            if(waveCount %10 == 0)
            {
                difficultyMultiplier *= 1.5f;
                Debug.Log("Difficulty increased: x" + difficultyMultiplier);
            }
            Invoke("StartNextWave", timeBreak); // Relax 5 sec 
        }
    }
    public float GetDifficultyMultiplier()
    {
        return difficultyMultiplier;
    }

    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(timeAlive / 60);
        int seconds = Mathf.FloorToInt(timeAlive % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public string GetCountOfEnemiesDead()
    {
        return allEnemiesKilled.ToString();
    }
}
