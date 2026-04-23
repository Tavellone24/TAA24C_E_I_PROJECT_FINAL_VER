using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform player; // Drag your XR Origin/Camera here
    public GameObject enemyPrefab;

    [Header("Spawn Settings")]
    public float spawnRadius = 10f;
    public float minHeight = 2f;
    public float maxHeight = 6f;
    
    public float initialSpawnRate = 3f;
    public float spawnRateDecrease = 0.1f; // How much faster they spawn each time
    public float minSpawnRate = 0.5f; // Fastest possible spawn rate

    [Header("Enemy Speed Settings")]
    public float initialEnemySpeed = 2f;
    public float enemySpeedIncrease = 0.2f;

    private float currentSpawnRate;
    private float currentEnemySpeed;
    private Coroutine spawnCoroutine;

    public void StartSpawning()
    {
        currentSpawnRate = initialSpawnRate;
        currentEnemySpeed = initialEnemySpeed;
        spawnCoroutine = StartCoroutine(SpawnRoutine());
    }

    public void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnEnemy();
            
            // Wait for the current spawn rate duration
            yield return new WaitForSeconds(currentSpawnRate);
            
            // Increase difficulty over time
            if (currentSpawnRate > minSpawnRate)
                currentSpawnRate -= spawnRateDecrease;
                
            currentEnemySpeed += enemySpeedIncrease;
        }
    }

    void SpawnEnemy()
    {
        // 1. Pick a random angle between -90 and 90 degrees (a semicircle in front of the player)
        float randomAngle = Random.Range(-90f, 90f);
        
        // 2. Calculate direction based on player's forward facing direction
        Vector3 spawnDirection = Quaternion.Euler(0, randomAngle, 0) * player.forward;
        
        // 3. Set the position at the correct distance
        Vector3 spawnPosition = player.position + (spawnDirection * spawnRadius);
        
        // 4. Adjust the height to be above the horizon
        spawnPosition.y = player.position.y + Random.Range(minHeight, maxHeight);

        // 5. Spawn the enemy and pass it the player reference and its specific speed
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        Enemy enemyScript = newEnemy.GetComponent<Enemy>();
        
        enemyScript.Initialize(player, currentEnemySpeed, FindObjectOfType<GameManager>());
    }
}