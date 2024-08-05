using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    public GameObject damageText;
    public GameObject critText;
    public Coin coinDrop;
    public int baseHealth;
    [HideInInspector] public int health;
    public float bleedTick = 1f;
    public float bleedInterval = 1f;
    public bool bleedTrue;
    public static int bleedAmount = 0;

    void Update()
    {
        Bleed();
        if (health <= 0)
        {
            SFXManager.Instance.EnemyDieSound();
            EnemySpawner.currentEnemies.Remove(gameObject);
            Destroy(gameObject);
            //drop coin on death
            Instantiate(coinDrop, transform.position, Quaternion.identity);
        }
    }
    void Bleed() //this function needs to be reworked to be able to stack bleed on the target
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
        
    }
}
