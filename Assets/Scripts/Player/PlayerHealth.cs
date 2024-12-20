using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    //health vars
    public float playerGold = 0;

    private  float maxHealth = 100;
    public  float MaxHealth
    {
        get
        {
            return maxHealth;
        }
        set
        {
            if (value < 1) //If the max health is less than 1, set it to 1
            {
                maxHealth = 1;
            }
            else
            {
                maxHealth = value;
            }
            if (currentHealth > value) //If the current health is greater than the max health, set it to the max health
            {
                currentHealth = value;
            }
        }
    }

    public static float currentHealth;
    float regenTick = 3f;
    float regenInterval = 3f;
    public static float regenAmount = 0;
    public static bool regenTrue { //The bool will check if the regen amount is greater than 0, if it is, regen is true
        get
        {
            return regenAmount > 0;
        }

    }
    public static float lifestealAmount = 0;
    //damage vars
    public static float damage = 20;
    public static int explosionSize = 0;
    public static bool explosiveBullets {
        get
        {
            return explosionSize > 0;
        }
    }

    public static float CritChance     {
        get
        {
            return critChance;
        }
        set
        {
            if (value > 1) //If the crit chance is greater than 1, set it to 1
            {
                critChance = 1;
            }
            else
            {
                critChance = value;
            }
        }
    }

    public static bool bleedTrue = false;

    private static float critChance;
    public static bool hasShotgun = false;
    public static int bulletAmount = 0; //this is for the extra bullets spawned by the shotgun item - it should always be even
    //other vars
    public GameObject damageText;
    public List<GameObject> lifeEggs;
    public UnityEvent onPlayerRespawn = new UnityEvent();
    public static PlayerHealth instance;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    
    void Start()
    {
        playerGold = 0;
        currentHealth = MaxHealth;
    }

    void Update()
    {
        Regen();
        if (currentHealth <= 0)
        {

            {
                FindObjectOfType<GameManager>().GameOver();
            }
        }
    }
    void Regen() //currently will regen players health even when paused
    {
        regenTick -= Time.deltaTime;
        if (regenTick <= 0 && regenTrue && currentHealth < MaxHealth) //only works if the player is missing health
        {
            regenTick = regenInterval;
            currentHealth += regenAmount;
            if (currentHealth > MaxHealth)//if the player will regen too much health
            {
                currentHealth = MaxHealth;

            }
        }
    }

    void Respawn()
    {
        //This event currently has no listeners, it is here for future use 
        onPlayerRespawn?.Invoke();

        if (lifeEggs.Count > 0) //This should never run if there are no eggs, but this is here just in case
        {
            gameObject.transform.position = lifeEggs[lifeEggs.Count - 1].transform.position;
            Destroy(lifeEggs[lifeEggs.Count - 1]);
            lifeEggs.Remove(lifeEggs[lifeEggs.Count - 1]);
        }
        currentHealth = MaxHealth;
        StartCoroutine(DisableCollisionForDuration(2f));
    }

    IEnumerator DisableCollisionForDuration(float duration)
    {
        // Get the layer mask of the player
        int playerLayer = gameObject.layer;

        // Set the collision matrix to ignore collisions between the player layer and itself for the specified duration
        Physics2D.IgnoreLayerCollision(playerLayer, playerLayer, true);

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Re-enable collisions between the player layer and itself
        Physics2D.IgnoreLayerCollision(playerLayer, playerLayer, false);

        // Log player health after collisions are turned back on
    }
    public void ReceiveDamage(int damageTaken)
    {
        GameObject damageTextInst = Instantiate(damageText, gameObject.transform);
        damageTextInst.GetComponent<TextMeshPro>().text = damageTaken.ToString();
    }


    /// <summary>
    /// Upgrades a stat by a given value. If applyAsMultiple is true, the value is multiplied by the stat, otherwise it is added to the stat.
    /// </summary>
    /// <param name="stat">What is being upgraded</param>
    /// <param name="value">The ammount to be applied </param>
    /// <param name="applyAsMultiple">Whether the value should be treated as a percentage</param>
    public  float UpgradeStat(float stat, float value, bool applyAsMultiple = false) 
    {
        if (applyAsMultiple)
        {
            stat *= value;
        }
        else
        {
            stat += value;
        }
        return stat;
    }
}