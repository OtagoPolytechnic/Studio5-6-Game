using UnityEngine;

public class BossController : MonoBehaviour
{
    public int maxHealth = 500;
    public float moveSpeed = 3.0f;
    private int currentHealth;
    private Transform player;
    private bool isPhaseTwo = false;

    // Animator reference
    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>(); // Get the Animator component
    }

    void Update()
    {
        Move();

        // Check if it's time to enter phase two
        if (currentHealth <= maxHealth / 2 && !isPhaseTwo)
        {
            EnterPhaseTwo();
        }
    }

    void Move()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

            // Set moving animation
            animator.SetBool("isMoving", true);
        }
        else
        {
            // Set idle animation
            animator.SetBool("isMoving", false);
        }
    }

    void EnterPhaseTwo()
    {
        isPhaseTwo = true;
        moveSpeed *= 4.0f; // Increase speed drastically
        Debug.Log("Boss has entered Phase Two!");

        // Optionally, change animation for phase two if any
        animator.SetBool("isPhaseTwo", true);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Play damage animation
        animator.SetTrigger("TakeDamage");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Boss is dead!");

        // Trigger death animation
        animator.SetTrigger("Die");

        // Destroy the boss object after death animation (adjust the delay as needed)
        Destroy(gameObject, 2.0f);
    }
}
