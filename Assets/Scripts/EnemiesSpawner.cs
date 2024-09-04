using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    public GameObject meeleEnemy;
    public GameObject rangedEnemy;
    public GameObject fastEnemy;
    public GameObject heavyEnemy;

    void Start()
    {
        int numberOfEnemies = Random.Range(2, 7); // Random number between 2 and 6

        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Randomly choose one of the four enemy types
            GameObject chosenPrefab;
            int randomChoice = Random.Range(0, 4);

            switch (randomChoice)
            {
                case 0:
                    chosenPrefab = meeleEnemy;
                    break;
                case 1:
                    chosenPrefab = rangedEnemy;
                    break;
                case 2:
                    chosenPrefab = fastEnemy;
                    break;
                case 3:
                    chosenPrefab = heavyEnemy;
                    break;
                default:
                    chosenPrefab = meeleEnemy;
                    break;
            }

            // Instantiate the chosen enemy
            GameObject newEnemy = Instantiate(chosenPrefab);

            // Set a random position for the enemy
            newEnemy.transform.position = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));

        }
    }
}
