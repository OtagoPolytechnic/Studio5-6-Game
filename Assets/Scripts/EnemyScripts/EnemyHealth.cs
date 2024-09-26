using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public GameObject damageText;
    public GameObject critText;
    public Coin coinDrop;
    public int baseHealth;
    public int health;
    public float bleedTick = 1f;
    public float bleedInterval = 1f;
    public bool bleedTrue;
    public static int bleedAmount = 0;

    public GameObject healthBarUI;
    private Image healthBarFill;

    void Start()
    {
        health = baseHealth;
        healthBarUI.SetActive(true);
        healthBarFill = healthBarUI.transform.Find("HealthBar").GetComponent<Image>();
        UpdateHealthBar();
        Debug.Log("Health bar initialized for " + gameObject.name);
    }

    void Update()
    {
        Bleed();
        if (health <= 0)
        {
            healthBarUI.SetActive(false);
            SFXManager.Instance.EnemyDieSound();
            EnemySpawner.currentEnemies.Remove(gameObject);
            Debug.Log("Enemy died " + gameObject.name);
            Destroy(gameObject);
            Instantiate(coinDrop, transform.position, Quaternion.identity);
        }

        if (healthBarUI != null)
        {
            healthBarUI.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 2, 0)); // Adjust Y as needed
        }
    }

    void Bleed() 
    {
        bleedTick -= Time.deltaTime;
        if (bleedTick <= 0 && bleedTrue)
        {
            bleedTick = bleedInterval;
            ReceiveDamage(bleedAmount, false);
        }
    }

    public void ReceiveDamage(int damageTaken, bool critTrue)
    {
        if (PlayerHealth.bleedTrue && !bleedTrue)
        {
            bleedTrue = true;
        }
        
        if (critTrue)
        {
            GameObject critTextInst = Instantiate(critText, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
            critTextInst.GetComponent<TextMeshPro>().text = damageTaken.ToString() + "!";
            health -= damageTaken;
        }
        else
        {
            GameObject damageTextInst = Instantiate(damageText, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
            damageTextInst.GetComponent<TextMeshPro>().text = damageTaken.ToString();
            health -= damageTaken;
        }

        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            float healthPercentage = (float)health / baseHealth;
            healthBarFill.fillAmount = healthPercentage;
        }
    }
}