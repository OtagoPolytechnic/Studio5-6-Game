using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : MonoBehaviour
{
    public GameObject player;
    public float speed;
    private float distance;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletPosition;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackInterval;
    [SerializeField] private float detectionRange; // Distance at which the enemy detects the player
    private float attackCooldown;

    private MapManager mapManager;

    private void Awake()
    {
        mapManager = FindObjectOfType<MapManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player object not found in EnemyRanged.Awake().");
        }
        attackCooldown = 0;
    }

    void Update()
    {
        if (player == null)
            return;

        Vector2 playerPosition = player.transform.position;
        distance = Vector2.Distance(transform.position, playerPosition);

        if (distance > detectionRange)
        {
            // Player is outside detection range, so enemy stays in place
            return;
        }

        Vector2 direction = playerPosition - (Vector2)transform.position;

        // Turns enemy towards player
        RotateTowardsPlayer(direction);

        if (distance >= attackRange)
        {
            MoveTowardsPlayer(playerPosition);
        }
        else
        {
            HandleAttack();
        }
    }

    private void RotateTowardsPlayer(Vector2 direction)
    {
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.GetChild(0).rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    private void MoveTowardsPlayer(Vector2 playerPosition)
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);
    }

    private void HandleAttack()
    {
        if (attackCooldown <= 0)
        {
            Shoot();
        }
        else
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    void Shoot()
    {
        Instantiate(bullet, bulletPosition.position, Quaternion.identity);
        attackCooldown = attackInterval;

        // Play the enemy shooting sound
        if (SFXManager.Instance != null)
        {
            SFXManager.Instance.EnemyShootSound();
        }
        else
        {
            Debug.LogError("SFXManager instance is null in EnemyRanged.Shoot().");
        }
    }
}