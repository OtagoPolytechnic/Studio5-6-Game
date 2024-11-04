/// <summary>
/// This script is used to control the boss in the game. The boss will have a number of phases
/// that it will go through. The boss will spawn a number of circles that the player must avoid
/// and then the boss will attack the player. The boss will then return to its starting position
/// and the process will repeat. The boss will have a health bar and text to show the player how
/// much health the boss has left.
/// </summary>
/// <remarks>
/// Author: Jun
/// Last Modified: 19/09/2024
/// Last Modified By: Erika
/// </remarks>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;


public class BossController : MonoBehaviour
{
    public int maxHealth = 500; // Boss health
    private int currentHealth;

    [Header("Boss Attributes")]
    [SerializeField] private GameObject circlePrefab; // Circle prefab
    [SerializeField] private GameObject arenaObj;
    [SerializeField] private GameObject catPosition;
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField]private TextMeshProUGUI bossHealthText;
    [SerializeField]private Image bossHealthBarImage;
    [SerializeField] private SpawnPrincess spawnPrincess;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private Vector2 arenaPos;

    private float distance;

    // Booleans
    public bool startFight = false;
    private bool isReturning = false;
    private bool isPhaseOneActive = false;
    private bool isAttacking = false;

    // Constants
    private const int CIRCLE_SPAWN_AREA = 20;
    private const int SPEED = 5;
    private const float LENGTH_OF_ATTACK_PHASE = 15f;

    void Start()
    {
        arenaPos = arenaObj.transform.position;
        rb = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
        bossHealthText.text = currentHealth.ToString("F0") + "/" + maxHealth.ToString("F0");
    }

    void Update()
    {
        HealthUpdate();

        if (startFight && !isPhaseOneActive && !isReturning)
        {
            startFight = false;
            isPhaseOneActive = true;
            StartCoroutine(PhaseOne());
        }
    }

    /// <summary>
    /// Movement is called in FixedUpdate to ensure smooth movement
    /// and required for rigidbody physics
    /// </summary>
    private void FixedUpdate()
    {
        if (isAttacking)
        {
            Move();
        }

        if (isReturning)
        {
            MoveBackToPosition();
        }
    }

    /// <summary>
    /// Updates the boss health bar and text
    /// </summary>
    private void HealthUpdate()
    {
        float healthFraction = (float)currentHealth / maxHealth;
        bossHealthBarImage.fillAmount = healthFraction;
        bossHealthText.text = currentHealth.ToString("F0") + "/" + maxHealth.ToString("F0");
    }

    /// <summary>
    /// Phase one is where a number of circles are spawned that the player must avoid
    /// </summary>
    /// <returns></returns>
    public IEnumerator PhaseOne()
    {
        for (int j = 0; j < 30; j++) // 30 circles
        {
            Vector2 randomPosition = new Vector2(
            Random.Range(arenaPos.x-CIRCLE_SPAWN_AREA, arenaPos.x+CIRCLE_SPAWN_AREA),
            Random.Range(arenaPos.y-CIRCLE_SPAWN_AREA, arenaPos.y+CIRCLE_SPAWN_AREA));
            Instantiate(circlePrefab, randomPosition, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }

        isPhaseOneActive = false;
        isAttacking = true;
        yield return new WaitForSeconds(2f);
        StartCoroutine(LengthOfAttackPhase());
    }

    /// <summary>
    /// Moves the boss towards the player for an attack phase
    /// </summary>
    private void Move()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = (player.transform.position - transform.position).normalized;

        if (SPEED > 0)
        {
            rb.MovePosition(Vector2.MoveTowards(transform.position, player.transform.position, SPEED * Time.deltaTime));
        }
    }

    /// <summary>
    /// Length of attack phase is the time the boss will persue the player
    /// </summary>
    private IEnumerator LengthOfAttackPhase()
    {
        yield return new WaitForSeconds(LENGTH_OF_ATTACK_PHASE);
        isReturning = true;
        isAttacking = false;
        MoveBackToPosition();
    }

    /// <summary>
    /// At the end of the attack phase the boss will return to the starting position
    /// </summary>
    private void MoveBackToPosition()
    {
        transform.position = Vector2.MoveTowards(transform.position, catPosition.transform.position, SPEED * Time.deltaTime);
        if (Vector2.Distance(transform.position, catPosition.transform.position) <= 0.1f)
        {
            isReturning = false;
            startFight = true;
        }
    }

    /// <summary>
    /// Used for whenever something collides with the boss
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.GetComponent<PlayerHealth>().ReceiveDamage(20);
            PlayerHealth.currentHealth -= 20;
        }

        if (other.CompareTag("Bullet"))
        {
            currentHealth -= 5;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                bossHealthText.transform.parent.gameObject.SetActive(false);
                Destroy(gameObject);
                spawnPrincess.InstantiatePrincess();
                ShowDialogue("Congratulations! You have defeated the boss and saved the princess.");
                // Victory
            }
        }
    }
    private void ShowDialogue(string message)
    {
        dialogueBox.SetActive(true);
        dialogueText.fontSize = 15;
        dialogueText.text = message;
        StartCoroutine(HideDialogueAfterDelay(15f));
    }

    private IEnumerator HideDialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        dialogueBox.SetActive(false);
    }

}