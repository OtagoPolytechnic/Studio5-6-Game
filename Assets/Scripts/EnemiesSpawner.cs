using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    public GameObject spritePrefab;

    void Start()
    {
        int numberOfSprites = Random.Range(1, 6);

        for (int i = 0; i < numberOfSprites; i++)
        {
            GameObject newSprite = Instantiate(spritePrefab);

            newSprite.transform.position = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        }
    }
}
