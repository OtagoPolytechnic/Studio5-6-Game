using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public Coin coinDrop;
    [SerializeField]
    private float spawnRadius = 1;
    public GameObject spawnCircle;
    [SerializeField]
    private Sprite chestOpen;

    private void Awake()
    {
        ItemSpawnManager.Instance.AddItem(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {
            if (gameObject.GetComponent<SpriteRenderer>().sprite != chestOpen)
            {
                //change to chest open sprite
                this.gameObject.GetComponent<SpriteRenderer>().sprite = chestOpen;
                StartCoroutine(WaitBeforeDestroy());
            }
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
        //spawns a coin randomly inside a circle around the chest
        Vector3 randomPos = Random.insideUnitCircle * spawnRadius;
        Instantiate(coinDrop, transform.position + randomPos, Quaternion.identity);
    }

    private IEnumerator WaitBeforeDestroy()
    {
        //spawns coins and then waits before removing chest
        //could change this to a variable in the editor if needed
        SpawnCoins(5);

        yield return new WaitForSeconds(2);

        Destroy(this.gameObject);
    }
}
