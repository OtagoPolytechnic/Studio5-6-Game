using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : MonoBehaviour
{
    public GameObject player;
    private float distance;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float detectionRange = 7f;
    [SerializeField] private int health = 50;
    private bool attacking = false;
    private GameObject attack;

    void Awake()
    {
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
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        attacking = true;
        attack.SetActive(true);
        attack.GetComponent<BoxCollider2D>().enabled = true;

        yield return new WaitForSeconds(0.5f);

        attack.SetActive(false);
        attacking = false;
        StopCoroutine(Attack());
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