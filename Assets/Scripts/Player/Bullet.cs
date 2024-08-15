using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float timer;
    private int critDamage = 0;
    private bool critTrue = false;

    void Start()
    {
        float critRoll = Random.Range(0f,1f);
        
        //crit damage calculation
        if (critRoll < PlayerHealth.CritChance)
        {
            critDamage += Mathf.RoundToInt(PlayerHealth.damage * 1.50f);
            critTrue = true;
            //Change to critical sprite
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
    
    void Update()
    {
        //destroys bullet after time elapsed
        timer += Time.deltaTime;

        if (timer >10)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //destroys bullet on hit with player and lowers health
        if (other.gameObject.CompareTag("Enemy"))
        {
            //lifesteal addition and cap
            PlayerHealth.currentHealth += PlayerHealth.lifestealAmount;
            if (PlayerHealth.currentHealth > PlayerHealth.maxHealth)
            {
                PlayerHealth.currentHealth = PlayerHealth.maxHealth;
            }
            //explosive bullets size calculation
            if (PlayerHealth.explosiveBullets)
            {
                transform.localScale = new Vector3(transform.localScale.x * (2 + 0.2f * PlayerHealth.explosionSize), transform.localScale.y * (2 + 0.2f * PlayerHealth.explosionSize), 1);
            }
            if (critTrue)
            {
                other.gameObject.GetComponent<EnemyHealth>().ReceiveDamage((int) PlayerHealth.damage + critDamage, true);
            }
            else
            {
                other.gameObject.GetComponent<EnemyHealth>().ReceiveDamage((int) PlayerHealth.damage, false);
            }
            Destroy(gameObject);
        }
    }
}
