using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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

/*
* Actual plan:
* Phase 1: Player must dodge incoming circles that will appear on the ground.
* Three rounds of that.
* Then boss will be actively fighting the player where the cat will pounce at the player.
* These two attacks will repeat until the boss' health is 250 or below.
* Phase 2: 
*/


public class BossController : MonoBehaviour
{
    public int maxHealth = 500;
    //public float moveSpeed = 3.0f;
    //public float attackRange = 1.5f;
    private int currentHealth;
    private Transform player;
    private bool isAttacking = false;

    [SerializeField] private GameObject bossHealthBar;
    private TextMeshProUGUI bossHealthText;
    private Image bossHealthBarImage;

    [SerializeField] private GameObject circlePrefab;
    [SerializeField] private GameObject arenaObj;
    //private BoxCollider2D arenaCollider;
    private Vector2 arenaPos;

    public bool startFight = false;

    public enum ActionState
    {
        Idle,
        Pounce,
        Chase
    }

    void Start()
    {
        arenaPos = arenaObj.transform.position;
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //arenaCollider = arenaObj.GetComponent<BoxCollider2D>();

        bossHealthBarImage = bossHealthBar.transform.GetChild(0).GetComponent<Image>();
        bossHealthText = bossHealthBar.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {

        //Move();
        HealthUpdate();

        if (startFight)
        {
            StartCoroutine(PhaseOne());
            startFight = false;
        }
    }

    private void HealthUpdate()
    {
        float healthFraction = (float)currentHealth / maxHealth;
        bossHealthBarImage.fillAmount = healthFraction;
        //bossHealthText.text = currentHealth.ToString("F0") + "/" + maxHealth.ToString("F0");
    }

    public IEnumerator PhaseOne()
    {
        /*
        * int range of 20 for the area of fighting
        * Every 0.5 seconds, a circle prefab will be instantiated at a random position within the range.
        */
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 20; j++) // 20 circles
            {
                Vector2 randomPosition = new Vector2(
                Random.Range(arenaPos.x-20, arenaPos.y+20),
                Random.Range(arenaPos.x-20, arenaPos.y+20));
                Instantiate(circlePrefab, randomPosition, Quaternion.identity);
                yield return new WaitForSeconds(0.5f);
            }
        }
        yield return new WaitForSeconds(0.5f);
    }

    // if the enemy is outside of an attack range of the player,
    // it will move towards the player
    // when the enemy is within the attack range, it will pause for a second
    // then will lurch forward and return to its original position
    private void Move()
    {
        //if (Vector2.Distance(transform.position, player.position) > attackRange)
        //{
            //transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        //}
        //else
        //{
            if (!isAttacking)
            {
                isAttacking = true;
                StartCoroutine(Attack());
            }
        //}
    }

    private IEnumerator Attack()
    {
        //transform.position = Vector2.MoveTowards(transform.position, player.position, -moveSpeed * Time.deltaTime);
        yield return new WaitForSeconds(1.0f);
        //transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        isAttacking = false;
    }


    // public void TakeDamage(int damage)
    // {
    //     currentHealth -= damage;
    //     Debug.Log("Boss takes damage: " + damage);

    //     if (currentHealth <= 0)
    //     {
    //         Die();
    //     }
    // }



    //     Invoke("ResetAttack", 1.0f);
    // }

    // void ResetAttack()
    // {
    //     isAttacking = false;
    // }

    // void Die()
    // {
    //     Debug.Log("Boss is dead!");//hello


    //     Destroy(gameObject, 2.0f);
    // }
}