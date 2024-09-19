using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : MonoBehaviour
{
    public GameObject player; // Reference to the player
    private float distance; // Distance to the player
    [SerializeField] private float speed = 5f; // Movement speed of the enemy
    [SerializeField] private float attackRange = 1.5f; // Range within which the enemy attacks
    [SerializeField] private float detectionRange = 7f; // Range within which the enemy detects the player
    [SerializeField] private int health = 50; // Health of the enemy
    private bool attacking = false; // Flag to determine if the enemy is attacking
    private GameObject attack; // Reference to the attack object

    void Awake()
    {
        // Find the player object
        player = GameObject.FindGameObjectWithTag("Player");

        // Try to get the attack object, and log an error if not found
        if (transform.childCount > 0)
        {
            Transform attackTransform = transform.GetChild(0);
            if (attackTransform.childCount > 0)
            {
                attack = attackTransform.GetChild(0).gameObject;
            }
            else
            {
                Debug.LogError("Attack object not found. Please ensure the hierarchy is correct.");
            }
        }
        else
        {
            Debug.LogError("No children found. Please ensure the hierarchy is correct.");
        }
    }

    void Update()
    {
        if (player == null || attack == null)
            return;

        // Calculate the distance to the player
        distance = Vector2.Distance(transform.position, player.transform.position);

        // If the player is out of detection range, do nothing
        if (distance > detectionRange)
        {
            return;
        }

        // Calculate direction towards the player and rotate the enemy
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (!attacking)
        {
            // Move towards the player
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            transform.GetChild(0).rotation = Quaternion.Euler(Vector3.forward * angle);

            // If within attack range, start the attack
            if (distance <= attackRange)
            {
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        attacking = true;
        // Show the attack object and enable its collider
        attack.SetActive(true);
        BoxCollider2D attackCollider = attack.GetComponent<BoxCollider2D>();
        if (attackCollider != null)
        {
            attackCollider.enabled = true;
        }
        else
        {
            Debug.LogError("BoxCollider2D component missing on attack object.");
        }

        yield return new WaitForSeconds(0.5f); // Duration of the attack

        // Hide the attack object and stop the attack
        attack.SetActive(false);
        attacking = false;
    }

    public void TakeDamage(int damageAmount)
    {
        // Reduce health by the damage amount
        health -= damageAmount;
        if (health <= 0)
        {
            // Destroy the enemy if health is depleted
            Destroy(gameObject);
        }
    }
}