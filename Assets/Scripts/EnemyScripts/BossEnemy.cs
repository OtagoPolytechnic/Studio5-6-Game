using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public GameObject player; // Reference to the player
    private float distance; // Distance to the player
    [SerializeField] private float speed = 2f; // Movement speed of the boss
    [SerializeField] private float attackRange = 3f; // Range within which the boss performs melee attacks
    [SerializeField] private float detectionRange = 10f; // Range within which the boss detects the player
    [SerializeField] private int health = 500; // Health of the boss
    [SerializeField] private GameObject bullet; // Reference to the bullet object for ranged attacks
    [SerializeField] private Transform bulletPosition; // Position from where bullets are shot
    [SerializeField] private float rangedAttackInterval = 3f; // Interval between ranged attacks
    private float attackCooldown; // Cooldown timer for ranged attacks
    private bool attacking = false; // Flag to determine if the boss is performing a melee attack
    private GameObject meleeAttack; // Reference to the melee attack object

    void Awake()
    {
        // Find the player object
        player = GameObject.FindGameObjectWithTag("Player");
        // Get the melee attack object
        meleeAttack = gameObject.transform.GetChild(0).GetChild(0).gameObject;
        attackCooldown = 0;
    }

    void Update()
    {
        if (player == null)
            return;

        // Calculate the distance to the player
        distance = Vector2.Distance(transform.position, player.transform.position);

        // If the player is out of detection range, do nothing
        if (distance > detectionRange)
        {
            return;
        }

        // Calculate direction towards the player and rotate the boss
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (!attacking)
        {
            // Move towards the player
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            transform.GetChild(0).rotation = Quaternion.Euler(Vector3.forward * angle);

            // If within melee attack range, start the melee attack
            if (distance <= attackRange)
            {
                StartCoroutine(MeleeAttack());
            }
            else if (attackCooldown <= 0)
            {
                // Otherwise, if the ranged attack cooldown has expired, perform a ranged attack
                RangedAttack();
            }
        }
    }

    IEnumerator MeleeAttack()
    {
        attacking = true;
        // Show the melee attack object and enable its collider
        meleeAttack.SetActive(true);
        meleeAttack.GetComponent<BoxCollider2D>().enabled = true;

        yield return new WaitForSeconds(1f); // Duration of the melee attack

        // Hide the melee attack object and stop the attack
        meleeAttack.SetActive(false);
        attacking = false;
        StopCoroutine(MeleeAttack());
    }

    void RangedAttack()
    {
        // Instantiate a bullet at the specified position
        Instantiate(bullet, bulletPosition.position, Quaternion.identity);
        attackCooldown = rangedAttackInterval; // Reset the cooldown timer
    }

    public void TakeDamage(int damageAmount)
    {
        // Reduce health by the damage amount
        health -= damageAmount;
        if (health <= 0)
        {
            // Destroy the boss if health is depleted
            Destroy(gameObject);
        }
    }
}
