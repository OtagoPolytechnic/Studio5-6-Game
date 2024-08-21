using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemy : MonoBehaviour
{
    public GameObject player;
    private float distance;
    private MapManager mapManager;
    private bool attacking = false;
    [SerializeField] private float attackRange;
    [SerializeField] private float speed;
    [SerializeField] private GameObject heavyAttackPrefab;

    void Awake()
    {
        mapManager = FindObjectOfType<MapManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float tileSpeedModifier = mapManager.GetTileWalkingSpeed(transform.position);

        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.GetChild(0).rotation = Quaternion.Euler(Vector3.forward * angle);

        if (!attacking)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, (speed * tileSpeedModifier) * Time.deltaTime);

            if (distance <= attackRange)
            {
                StartCoroutine(HeavyAttack());
            }
        }
    }

    IEnumerator HeavyAttack()
    {
        attacking = true;

        // Perform a heavy melee attack
        Debug.Log("Tank performs a heavy attack!");
        Instantiate(heavyAttackPrefab, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(1.5f); // Heavy attack duration

        attacking = false;
    }
}
