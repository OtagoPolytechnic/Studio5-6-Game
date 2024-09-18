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
    private bool isAttacking = false;

    [SerializeField] private GameObject bossHealthBar;
    private TextMeshProUGUI bossHealthText;
    private Image bossHealthBarImage;

    [SerializeField] private GameObject circlePrefab;
    [SerializeField] private GameObject arenaObj;
    //private BoxCollider2D arenaCollider;
    private Vector2 arenaPos;

    public bool startFight = false;

    private float distance;
    private float speed;

    public bool phaseOneEnd = false;

    private const int CIRCLE_SPAWN_AREA = 20;

    [SerializeField] private GameObject catPosition;

    [SerializeField] private GameObject player;
    private bool isReturning = false;
    private bool isPhaseOneActive = false;

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

        //arenaCollider = arenaObj.GetComponent<BoxCollider2D>();

        bossHealthBarImage = bossHealthBar.transform.GetChild(0).GetComponent<Image>();
        bossHealthText = bossHealthBar.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {

        //Move();
        HealthUpdate();

        if (startFight && !isPhaseOneActive && !isReturning)
        {
            startFight = false;
            isPhaseOneActive = true;
            StartCoroutine(PhaseOne());
        }

        if (phaseOneEnd)
        {
            phaseOneEnd = false;
            isPhaseOneActive = false;
            Debug.Log("Phase One has ended");
            Move();
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
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 20; j++) // 20 circles
            {
                Vector2 randomPosition = new Vector2(
                Random.Range(arenaPos.x-CIRCLE_SPAWN_AREA, arenaPos.x+CIRCLE_SPAWN_AREA),
                Random.Range(arenaPos.y-CIRCLE_SPAWN_AREA, arenaPos.y+CIRCLE_SPAWN_AREA));
                Instantiate(circlePrefab, randomPosition, Quaternion.identity);
                yield return new WaitForSeconds(0.5f);
            }
        }
        phaseOneEnd = true;
        yield return new WaitForSeconds(0.5f);
    }

    private void Move()
    {
        Debug.Log("Moving");
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, (speed ) * Time.deltaTime);
        transform.GetChild(0).rotation = Quaternion.Euler(Vector3.forward * angle);

        if (distance <= 1.5f)
        {
            isAttacking = true;
            StartCoroutine(Attack());
            StartCoroutine(LengthOfAttackPhase());
        }
    }

    // attack player
    private IEnumerator Attack()
    {
        if (isAttacking)
        {
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator LengthOfAttackPhase()
    {
        Debug.Log("Waiting for attack to end");
        yield return new WaitForSeconds(15.0f);
        StopCoroutine(Attack());
        MoveBackToPosition();
        isReturning = true;
    }

    private void MoveBackToPosition()
    {
        transform.position = Vector2.MoveTowards(transform.position, catPosition.transform.position, 2 * Time.deltaTime);
        if (Vector2.Distance(transform.position, catPosition.transform.position) <= 0.1f)
        {
            isReturning = false;
            startFight = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<PlayerHealth>().ReceiveDamage(10);
        }
    }
}