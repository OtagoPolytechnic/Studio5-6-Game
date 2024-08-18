using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public Coin coinDrop;
    [SerializeField]
    private float spawnRadius = 1;
    public GameObject spawnCircle;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SpawnCoins(5);
        }
    }

    void SpawnCoins(int coinsNum)
    {
        for (int i = 0; i < coinsNum; i++)
        {
            SpawnRandomCoin();
        }
    }

    void SpawnRandomCoin()
    {
        Vector2 randomPos = Random.insideUnitCircle * spawnRadius;
        spawnCircle.transform.position += new Vector3(randomPos.x, 0, randomPos.y);
        Instantiate(coinDrop, spawnCircle.transform.position, Quaternion.identity);
    }

    
}
