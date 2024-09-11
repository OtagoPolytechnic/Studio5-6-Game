using UnityEngine;
using UnityEngine.UI;
using TMPro;


/*
* Player enters boss room
* Player is met with cat trader
* player is confused, dialogue.
* cat transforms into evil monster cat
* phase 1: cat pounce.
* when the cat is within distance of the player, it will pounce into the player then jump back.
* phase 2: dodge appearing circles
* the cat will summon circles that will appear on the ground, the player must dodge them.

*other ideas:
* cat will summon minions to attack the player
* cat will circle player and have fake clones appear, player must hit the real cat.
*/


public class BossController : MonoBehaviour
{
    public int maxHealth = 500;
    public float moveSpeed = 3.0f;
    public float attackRange = 1.5f;
    private int currentHealth;
    private Transform player;
    private bool isAttacking = false;

    [SerializeField] private GameObject bossHealthBar;
    private TextMeshProUGUI bossHealthText;
    private Image bossHealthBarImage;

    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        bossHealthBarImage = bossHealthBar.transform.GetChild(0).GetComponent<Image>();
        bossHealthText = bossHealthBar.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // if (player != null)
        // {
        //     Move();


        //     float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        //     if (distanceToPlayer <= attackRange && !isAttacking)
        //     {
        //         Attack();
        //     }
        // }

        //Move();
        HealthUpdate();
    }

    private void HealthUpdate()
    {
        float healthFraction = (float)currentHealth / maxHealth;
        bossHealthBarImage.fillAmount = healthFraction;
        bossHealthText.text = currentHealth.ToString("F0") + "/" + maxHealth.ToString("F0");
    }

    void Move()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer > attackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
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