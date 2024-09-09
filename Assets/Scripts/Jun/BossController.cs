using UnityEngine;

public class BossController : MonoBehaviour
{
    public int maxHealth = 500;
    public float moveSpeed = 3.0f;
    public float attackRange = 1.5f;
    private int currentHealth;
    private Transform player;
    private bool isAttacking = false;

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            Move();


            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= attackRange && !isAttacking)
            {
                Attack();
            }
        }
    }

    void Move()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer > attackRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Boss takes damage: " + damage);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Attack()
    {
        isAttacking = true;
        Debug.Log("Boss is attacking the player!");


        Invoke("ResetAttack", 1.0f);
    }

    void ResetAttack()
    {
        isAttacking = false;
    }

    void Die()
    {
        Debug.Log("Boss is dead!");//hello


        Destroy(gameObject, 2.0f);
    }
}