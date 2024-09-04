using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    public GameObject player;
    private float distance;
    private MapManager mapManager;
    private bool attacking = false;
    [SerializeField] private float attackRange;
    [SerializeField] private float speed;
    [SerializeField] private float detectionRange; // Distance at which the enemy detects the player
    private GameObject attack;

    void Awake()
    {
        //mapManager = FindObjectOfType<MapManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        attack = gameObject.transform.GetChild(0).GetChild(0).gameObject;
    }

    void Update()
    {
        if (player == null)
            return;

        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance > detectionRange)
        {
            // Player is outside detection range, so enemy stays in place
            return;
        }

        Vector2 direction = player.transform.position - transform.position;

        // Turns enemy towards player
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (!attacking) // This enemy type stops moving to attack
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            transform.GetChild(0).rotation = Quaternion.Euler(Vector3.forward * angle);

            if (distance <= attackRange)
            {
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        attacking = true;
        attack.SetActive(true); // Show the attack
        attack.GetComponent<BoxCollider2D>().enabled = true; // Enable the collider

        // Play the enemy bite sound
        if (SFXManager.Instance != null)
        {
            SFXManager.Instance.EnemyBiteSound();
        }
        else
        {
            Debug.LogError("SFXManager instance is null in EnemyMelee.Attack().");
        }

        yield return new WaitForSeconds(1f); // Attack duration

        attack.SetActive(false);
        attacking = false;
        StopCoroutine(Attack());
    }
}
