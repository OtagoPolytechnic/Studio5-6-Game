using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    public GameObject meleeEnemy;
    public GameObject rangedEnemy;
    public GameObject heavyEnemy;
    public GameObject fastEnemy;
    public float minDistance = 1.0f; // Minimum distance between enemies

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    void Start()
    {
        int numberOfEnemies = Random.Range(2, 8);

        for (int i = 0; i < numberOfEnemies; i++)
        {
            GameObject chosenPrefab;
            int enemyType = Random.Range(0, 3);

            if (enemyType == 0)
            {
                chosenPrefab = meleeEnemy;
            }
            else if (enemyType == 1)
            {
                chosenPrefab = rangedEnemy;
            }
            else if (enemyType == 2)
            {
                chosenPrefab = heavyEnemy;
            }
            else
            {
                chosenPrefab= fastEnemy;
            }

            Vector2 spawnPosition;
            bool positionValid;

            do
            {
                // Generate a random position
                spawnPosition = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
                positionValid = true;

                // Check the distance to all previously spawned enemies
                foreach (GameObject enemy in spawnedEnemies)
                {
                    if (Vector2.Distance(spawnPosition, enemy.transform.position) < minDistance)
                    {
                        positionValid = false;
                        break;
                    }
                }
            }
            while (!positionValid); // Keep trying until a valid position is found

            // Instantiate the enemy and add it to the list of spawned enemies
            GameObject newEnemy = Instantiate(chosenPrefab, spawnPosition, Quaternion.identity);
            spawnedEnemies.Add(newEnemy);
        }
    }
}