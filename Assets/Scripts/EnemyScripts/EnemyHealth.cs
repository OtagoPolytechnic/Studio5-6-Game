using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    // Reference to the health bar UI
    [SerializeField] private Image healthBarImage;

    void Start()
    {
        health = baseHealth;

        // Set the health bar position (optional if you're using World Space canvas)
        healthBarImage.transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);

        // Update the health bar at the start
        UpdateHealthBar();
    }

    void Update()
    {
        Bleed();

        if (health <= 0)
        {
            SFXManager.Instance.EnemyDieSound();
            EnemySpawner.currentEnemies.Remove(gameObject);
            Debug.Log("Enemy died " + gameObject.name);
            Destroy(gameObject);

            // Drop coin on death
            Instantiate(coinDrop, transform.position, Quaternion.identity);
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

        health -= damageTaken;
        Debug.Log("Current Health: " + health);
        UpdateHealthBar();  // Update the health bar after taking damage

        if (critTrue)
        {
            GameObject critTextInst = Instantiate(critText, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
            critTextInst.GetComponent<TextMeshPro>().text = damageTaken.ToString() + "!";
        }
        else
        {
            GameObject damageTextInst = Instantiate(damageText, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
            damageTextInst.GetComponent<TextMeshPro>().text = damageTaken.ToString();
        }
    }

    // Update the health bar to reflect the current health
    void UpdateHealthBar()
    {
        // Use fillAmount for Image-based health bars
        healthBarImage.fillAmount = (float)health / baseHealth;
    }
}