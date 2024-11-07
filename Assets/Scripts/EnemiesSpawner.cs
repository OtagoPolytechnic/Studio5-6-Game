using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    public GameObject meeleEnemy;
    public GameObject rangedEnemy;
    public GameObject fastEnemy;
    public GameObject heavyEnemy;

    public Transform[] spawnPoints; // List of predefined spawn points

    private int minEnemiesPerPoint = 2; // Ensure a minimum of 2 enemies spawn
    private int maxEnemiesPerPoint = 5; // Maximum number of enemies to spawn at each point
    private float spawnRadius = 2f; // Radius around the spawn point to space out enemies
    private float minimumDistanceBetweenEnemies = 1.5f; // Minimum distance between spawned enemies

    private List<Vector3> usedPositions = new List<Vector3>(); // Track used positions to avoid overlap

    void Start()
    {
        // Ensure all prefabs and spawn points are assigned
        if (meeleEnemy == null || rangedEnemy == null || fastEnemy == null || heavyEnemy == null)
        {
            Debug.LogError("One or more enemy prefabs are not assigned.");
            return;
        }

        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned.");
            return;
        }

        // Iterate over each spawn point
        foreach (Transform spawnPoint in spawnPoints)
        {
            // Randomly decide how many enemies to spawn at this spawn point
            int numberOfEnemies = Random.Range(minEnemiesPerPoint, maxEnemiesPerPoint + 1);

            usedPositions.Clear(); // Clear previous used positions for the current spawn point

            for (int i = 0; i < numberOfEnemies; i++)
            {
                // Randomly choose one of the enemy types
                GameObject chosenPrefab = GetRandomEnemyPrefab();

                // Get a valid random position around the spawn point
                Vector3 spawnPosition = GetValidSpawnPosition(spawnPoint.position);

                // Instantiate the enemy at the valid position
                GameObject newEnemy = Instantiate(chosenPrefab, spawnPosition, Quaternion.identity);

                // Check if the enemy is instantiated correctly
                if (newEnemy == null)
                {
                    Debug.LogError("Failed to instantiate enemy.");
                }
            }
        }
    }

    private GameObject GetRandomEnemyPrefab()
    {
        GameObject[] enemyPrefabs = { meeleEnemy, rangedEnemy, fastEnemy, heavyEnemy };
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        return enemyPrefabs[randomIndex];
    }

    private Vector3 GetValidSpawnPosition(Vector3 spawnCenter)
    {
        Vector3 spawnPosition;
        bool validPosition = false;

        // Try to find a valid position
        do
        {
            // Generate a random position within the spawn radius
            spawnPosition = spawnCenter + (Vector3)Random.insideUnitCircle * spawnRadius;

            // Check if this position is far enough from previously spawned enemies
            validPosition = true;
            foreach (Vector3 usedPos in usedPositions)
            {
                if (Vector3.Distance(spawnPosition, usedPos) < minimumDistanceBetweenEnemies)
                {
                    validPosition = false;
                    break;
                }
            }

        } while (!validPosition);

        // Add the valid position to the list of used positions
        usedPositions.Add(spawnPosition);

        return spawnPosition;
    }
}
