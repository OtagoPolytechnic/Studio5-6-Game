using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public GameObject player;
    private float distance;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float attackRange = 3f;
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private int health = 500;
    [SerializeField] private GameObject bullet; // Ranged attack
    [SerializeField] private Transform bulletPosition;
    [SerializeField] private float rangedAttackInterval = 3f;
    private float attackCooldown;
    private bool attacking = false;
    private GameObject meleeAttack; // Melee attack

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        meleeAttack = gameObject.transform.GetChild(0).GetChild(0).gameObject;
        attackCooldown = 0;
    }

    void Update()
    {
        if (player == null)
            return;

        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance > detectionRange)
        {
            return;
        }

        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (!attacking)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            transform.GetChild(0).rotation = Quaternion.Euler(Vector3.forward * angle);

            if (distance <= attackRange)
            {
                StartCoroutine(MeleeAttack());
            }
            else if (attackCooldown <= 0)
            {
                RangedAttack();
            }
        }
    }

    IEnumerator MeleeAttack()
    {
        attacking = true;
        meleeAttack.SetActive(true);
        meleeAttack.GetComponent<BoxCollider2D>().enabled = true;

        yield return new WaitForSeconds(1f);

        meleeAttack.SetActive(false);
        attacking = false;
        StopCoroutine(MeleeAttack());
    }

    void RangedAttack()
    {
        Instantiate(bullet, bulletPosition.position, Quaternion.identity);
        attackCooldown = rangedAttackInterval;
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
