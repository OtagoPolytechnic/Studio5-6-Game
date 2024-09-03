using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    public GameObject meeleEnemy1;
    public GameObject rangedEnemy2;

    void Start()
    {
        int numberOfSprites = Random.Range(1, 6);

        for (int i = 0; i < numberOfSprites; i++)
        {
            GameObject chosenPrefab = Random.Range(0, 2) == 0 ? meeleEnemy1 : rangedEnemy2;

            // Instantiate a new sprite
            GameObject newSprite = Instantiate(chosenPrefab);

            newSprite.transform.position = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        }
    }
}
