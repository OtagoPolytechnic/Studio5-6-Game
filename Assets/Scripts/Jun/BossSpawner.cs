using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab;  
    public Transform spawnLocation; 
    private bool hasSpawned = false; 

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasSpawned) 
        {
            SpawnBoss();
            hasSpawned = true;
        }
    }

    
    private void SpawnBoss()
    {
        if (bossPrefab != null && spawnLocation != null)
        {
            Instantiate(bossPrefab, spawnLocation.position, spawnLocation.rotation);
        }
        else
        {
            Debug.LogError("BossPrefab or SpawnLocation is not set!");
        }
    }
}
