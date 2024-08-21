using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : MonoBehaviour
{
    public GameObject player;
    private MapManager mapManager;
    [SerializeField] private float speed;
    [SerializeField] private float chaseRange;

    void Awake()
    {
        mapManager = FindObjectOfType<MapManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float tileSpeedModifier = mapManager.GetTileWalkingSpeed(transform.position);

        float distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.GetChild(0).rotation = Quaternion.Euler(Vector3.forward * angle);

        if (distance <= chaseRange)
        {
            // Move towards the player at a faster speed
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, (speed * tileSpeedModifier) * Time.deltaTime);
        }
    }
}
