using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemy : MonoBehaviour
{
    public GameObject player; // Reference to the player
    private float distance; // Distance to the player
    [SerializeField] private float speed = 1f; // Movement speed of the enemy
    [SerializeField] private float attackRange = 2f; // Range within which the enemy attacks
    [SerializeField] private float detectionRange = 5f; // Range within which the enemy detects the player
    [SerializeField] private int damage = 50; // Damage dealt by the enemy
    private bool attacking = false; // Flag to determine if the enemy is attacking
    private GameObject attack; // Reference to the attack object

    void Awake()
    {
        Debug.Log("Children count: " + gameObject.transform.childCount);
        if (gameObject.transform.childCount > 0)
        {
            Debug.Log("First child: " + gameObject.transform.GetChild(0).name);
            if (gameObject.transform.GetChild(0).childCount > 0)
            {
                Debug.Log("First child of first child: " + gameObject.transform.GetChild(0).GetChild(0).name);
            }
        }
        // Find the player object
        player = GameObject.FindGameObjectWithTag("Player");
        // Get the attack object
        attack = gameObject.transform.GetChild(0).GetChild(0).gameObject;
    }

    void Update()
    {
        if (player == null)
            return;
        // If the player is out of detection range, do nothing
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
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            transform.GetChild(0).rotation = Quaternion.Euler(Vector3.forward * angle);

            // If within attack range, start the attack
            if (distance <= attackRange)
            {
                Debug.Log("Heavy enemy attacking");
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        attacking = true;
        // Show the attack object and enable its collider
        attack.SetActive(true);
        attack.GetComponent<BoxCollider2D>().enabled = true;

        yield return new WaitForSeconds(1.5f); // Duration of the attack

        // Hide the attack object and stop the attack
        attack.SetActive(false);
        attacking = false;
        StopCoroutine(Attack());
    }

    public void TakeDamage(int damageAmount)
    {
        // Reduce health by the damage amount
        // health -= damageAmount;
        // if (health <= 0)
        // {
            // Destroy the enemy if health is depleted
        //    Destroy(gameObject);
        //}
    }
}