using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using PlasticPipe.PlasticProtocol.Messages;

public class PlayerHealth : MonoBehaviour
{
    //health vars
    public float playerGold;
    public static float maxHealth = 100;
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
    public static int damage = 20;
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
        playerGold = 50;
        currentHealth = maxHealth;
    }

    void Update()
    {
        Regen();
        if (currentHealth <= 0)
        {

            if (lifeEggs.Count > 0)
            {
                Respawn();
            }
            else
            {
                FindObjectOfType<GameManager>().GameOver();
            }
        }
    }
    void Regen() //currently will regen players health even when paused
    {
        regenTick -= Time.deltaTime;
        if (regenTick <= 0 && regenTrue && currentHealth < maxHealth) //only works if the player is missing health
        {
            regenTick = regenInterval;
            currentHealth += regenAmount;
            if (currentHealth > maxHealth)//if the player will regen too much health
            {
                currentHealth = maxHealth;

            }
            Debug.Log($"Regen: {currentHealth}");
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
        //Debug.Log("Player health before collisions turned off: " + currentHealth);
        currentHealth = maxHealth;
        StartCoroutine(DisableCollisionForDuration(2f));
    }

    IEnumerator DisableCollisionForDuration(float duration)
    {
        // Get the layer mask of the player
        int playerLayer = gameObject.layer;

        // Set the collision matrix to ignore collisions between the player layer and itself for the specified duration
        Physics2D.IgnoreLayerCollision(playerLayer, playerLayer, true);
        //Debug.Log("Collisions disabled for 2 seconds.");

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Re-enable collisions between the player layer and itself
        Physics2D.IgnoreLayerCollision(playerLayer, playerLayer, false);
        //Debug.Log("Collisions enabled after 2 seconds.");

        // Log player health after collisions are turned back on
        //Debug.Log("Player health after collisions turned back on: " + currentHealth);
    }
    public void ReceiveDamage(int damageTaken)
    {
        GameObject damageTextInst = Instantiate(damageText, gameObject.transform);
        damageTextInst.GetComponent<TextMeshPro>().text = damageTaken.ToString();
    }

    public  void UpgradeStat(float stat, float value, bool percentageUpgrade = false)
    {
        if (percentageUpgrade)
        {
            stat *= value;
        }
        else
        {
            stat += value;
        }
    }
}