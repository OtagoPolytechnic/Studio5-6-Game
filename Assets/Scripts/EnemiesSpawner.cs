using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    public GameObject meeleEnemy;
    public GameObject rangedEnemy;
    public GameObject fastEnemy;
    public GameObject heavyEnemy;

    [SerializeField] private Vector2 spawnRange = new Vector2(5f, 5f); // Range for enemy spawn positions
    [SerializeField] private int minEnemies = 2; // Minimum number of enemies to spawn
    [SerializeField] private int maxEnemies = 7; // Maximum number of enemies to spawn

    void Start()
    {
        // Ensure all prefabs are assigned
        if (meeleEnemy == null || rangedEnemy == null || fastEnemy == null || heavyEnemy == null)
        {
            Debug.LogError("One or more enemy prefabs are not assigned.");
            return;
        }

        int numberOfEnemies = Random.Range(minEnemies, maxEnemies);

        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Randomly choose one of the four enemy types
            GameObject chosenPrefab = GetRandomEnemyPrefab();

            // Instantiate the chosen enemy
            GameObject newEnemy = Instantiate(chosenPrefab);

            // Set a random position for the enemy
            Vector2 randomPosition = new Vector2(Random.Range(-spawnRange.x, spawnRange.x), Random.Range(-spawnRange.y, spawnRange.y));
            newEnemy.transform.position = randomPosition;
        }
    }

    private GameObject GetRandomEnemyPrefab()
    {
        GameObject[] enemyPrefabs = { meeleEnemy, rangedEnemy, fastEnemy, heavyEnemy };
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        return enemyPrefabs[randomIndex];
    }
}
