using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;


public class BossController : MonoBehaviour
{
    public int maxHealth = 500;

    private int currentHealth;

    [SerializeField] private GameObject circlePrefab;
    [SerializeField] private GameObject bossHealthBar;
    [SerializeField] private GameObject arenaObj;
    [SerializeField] private GameObject catPosition;
    [SerializeField] private GameObject player;
    private TextMeshProUGUI bossHealthText;
    [SerializeField] private Rigidbody2D rb;
    private Image bossHealthBarImage;

    //private BoxCollider2D arenaCollider;
    private Vector2 arenaPos;

    private float distance;
    private float speed = 5;

    private const int CIRCLE_SPAWN_AREA = 20;

    public bool startFight = false;
    private bool isReturning = false;
    private bool isPhaseOneActive = false;
    private bool isAttacking = false;

    void Start()
    {
        arenaPos = arenaObj.transform.position;
        currentHealth = maxHealth;

        //arenaCollider = arenaObj.GetComponent<BoxCollider2D>();

        bossHealthBarImage = bossHealthBar.transform.GetChild(0).GetComponent<Image>();
        bossHealthText = bossHealthBar.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        rb = GetComponent<Rigidbody2D>();
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

    private void HealthUpdate()
    {
        float healthFraction = (float)currentHealth / maxHealth;
        bossHealthBarImage.fillAmount = healthFraction;
        //bossHealthText.text = currentHealth.ToString("F0") + "/" + maxHealth.ToString("F0");
    }

    public IEnumerator PhaseOne()
    {
        //for (int i = 0; i < 3; i++)
        //{
            for (int j = 0; j < 20; j++) // 20 circles
            {
                Vector2 randomPosition = new Vector2(
                Random.Range(arenaPos.x-CIRCLE_SPAWN_AREA, arenaPos.x+CIRCLE_SPAWN_AREA),
                Random.Range(arenaPos.y-CIRCLE_SPAWN_AREA, arenaPos.y+CIRCLE_SPAWN_AREA));
                Instantiate(circlePrefab, randomPosition, Quaternion.identity);
                yield return new WaitForSeconds(0.5f);
            }
        //}
        isPhaseOneActive = false;
        isAttacking = true;
        yield return new WaitForSeconds(1f);
        StartCoroutine(LengthOfAttackPhase());
    }

    private void Move()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = (player.transform.position - transform.position).normalized;

        if (speed > 0)
        {
            rb.MovePosition(Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime));
        }
    }

    private IEnumerator LengthOfAttackPhase()
    {
        Debug.Log("Waiting for attack to end");
        yield return new WaitForSeconds(15.0f);
        isReturning = true;
        isAttacking = false;
        MoveBackToPosition();
    }

    private void MoveBackToPosition()
    {
        transform.position = Vector2.MoveTowards(transform.position, catPosition.transform.position, speed * Time.deltaTime);
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