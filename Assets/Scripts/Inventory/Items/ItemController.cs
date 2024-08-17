using System;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    //addition of a new item in the inventory page script, requires its functionailty in here
    public GameObject eggPrefab;
    public static ItemController instance;

    void Awake()
    {
        instance = this;
    }

    public void IncreaseLives()
    {
        GameObject newEgg = Instantiate(
            eggPrefab,
            new Vector3(0, 0, 0),
            Quaternion.identity,
            GameObject.Find("Nest").transform
        );
        newEgg.transform.localScale = new Vector3(0.3333333f, 0.3333333f, 0.3333333f);
    }

    public void IncreaseEnemyBleed()  => EnemyHealth.bleedAmount = (int)PlayerHealth.instance.UpgradeStat(EnemyHealth.bleedAmount, 5);
    
    public void IncreaseLifeSteal()  => PlayerHealth.lifestealAmount = PlayerHealth.instance.UpgradeStat(PlayerHealth.lifestealAmount, 1f); 
        
    public void UpgradeCritChance()  => PlayerHealth.CritChance = PlayerHealth.instance.UpgradeStat(PlayerHealth.CritChance, 0.07f);
    

    public void ExplosiveBullets()  => PlayerHealth.explosionSize = (int)PlayerHealth.instance.UpgradeStat(PlayerHealth.explosionSize, 1); 
    
    public void UpgradeFireRate() => Shooting.firerate = PlayerHealth.instance.UpgradeStat(Shooting.firerate, 0.9f, true);

    public void UpgradeRegen() => PlayerHealth.regenAmount = PlayerHealth.instance.UpgradeStat(PlayerHealth.regenAmount, 1f);


    public void IncreaseBulletAmount() 
    {
        PlayerHealth.hasShotgun = true;
        PlayerHealth.bulletAmount += 2;
        Debug.Log($"Shotgun bullets: {PlayerHealth.bulletAmount}");
    }



    public void AddItemStack(int id)
    {
        if (InventoryPage.itemList[id].isStackable) //If the item is stackable
        {
            InventoryPage.itemList[id].stacks++; //Add 1 to selected item stack
        }
    }


    //Should be Deleted once all references are removed
    public void ItemPicked(int itemID)
    {
        Debug.Log(itemID);

        switch (itemID)
        {
            case 0:
                PlayerHealth.damage += 10;
                Debug.Log($"Damage: {PlayerHealth.damage}");
                break;
            case 01:
                float current = PlayerHealth.maxHealth;
                PlayerHealth.maxHealth *= 1.10f;
                Mathf.RoundToInt(PlayerHealth.maxHealth);
                PlayerHealth.currentHealth += PlayerHealth.maxHealth - current;
                Debug.Log($"Max health: {PlayerHealth.maxHealth}");
                break;
            case 02:
                TopDownMovement.moveSpeed *= 1.05f;
                Debug.Log($"Speed: {TopDownMovement.moveSpeed}");
                break;
            case 03:
                PlayerHealth.regenAmount += 1f;
                Debug.Log($"Regen amount: {PlayerHealth.regenAmount}");
                break;
            case 04:
                Shooting.firerate *= 0.9f;
                Debug.Log($"Firerate: {Shooting.firerate}");
                break;
            case 05:
                EnemyHealth.bleedAmount += 5;
                PlayerHealth.bleedTrue = true;
                Debug.Log($"Bleed amount: {EnemyHealth.bleedAmount}");
                break;
            case 06:
                PlayerHealth.lifestealAmount += 1f;
                Debug.Log($"Lifesteal amount: {PlayerHealth.lifestealAmount}");
                break;
            case 07:
                PlayerHealth.explosionSize += 1;
                Debug.Log($"Explosion size: {PlayerHealth.explosionSize}");
                break;
            case 08:
                GameObject newEgg = Instantiate(
                    eggPrefab,
                    new Vector3(0, 0, 0),
                    Quaternion.identity,
                    GameObject.Find("Nest").transform
                );
                newEgg.transform.localScale = new Vector3(0.3333333f, 0.3333333f, 0.3333333f);
                break;
            case 09:
                PlayerHealth.CritChance += 0.07f;

                Debug.Log($"Crit Chance: {PlayerHealth.CritChance}");
                break;
            case 10:
                PlayerHealth.maxHealth /= 2f;
                if (PlayerHealth.maxHealth <= PlayerHealth.currentHealth)
                {
                    PlayerHealth.currentHealth = PlayerHealth.maxHealth;
                }
                else
                {
                    PlayerHealth.currentHealth /= 2f;
                }
                PlayerHealth.damage *= 2;
                Debug.Log(
                    $"Players max health as been cut in half to:{PlayerHealth.maxHealth}. Their current health is: {PlayerHealth.currentHealth}. Their damage has been doubled to: {PlayerHealth.damage}"
                );
                break;
            case 11:
                PlayerHealth.hasShotgun = true;
                PlayerHealth.bulletAmount += 2;
                Debug.Log($"Shotgun bullets: {PlayerHealth.bulletAmount}");
                break;
            case 12:
                for (int i = 0; i < 2; i++)
                {
                    int randomRoll = UnityEngine.Random.Range(0, 4);
                    if (randomRoll == 0)
                    {
                        PlayerHealth.damage += 5;
                        Debug.Log($"Damage: {PlayerHealth.damage}");
                    }
                    else if (randomRoll == 1)
                    {
                        float current2 = PlayerHealth.maxHealth;
                        PlayerHealth.maxHealth *= 1.05f;
                        Mathf.RoundToInt(PlayerHealth.maxHealth);
                        PlayerHealth.currentHealth += PlayerHealth.maxHealth - current2;
                        Debug.Log($"Max health: {PlayerHealth.maxHealth}");
                    }
                    else if (randomRoll == 2)
                    {
                        TopDownMovement.moveSpeed *= 1.025f;
                        Debug.Log($"Speed: {TopDownMovement.moveSpeed}");
                    }
                    else if (randomRoll == 3)
                    {
                        Shooting.firerate *= 0.95f;
                        Debug.Log($"Firerate: {Shooting.firerate}");
                    }
                }

                break;
            case 13:
                //PlayerHealth.hasLantern = true;
                Debug.Log("Player has a lantern");
                break;
            case 14:
                //PlayerHealth.coins ++;

                Debug.Log("you picked up a coin");
                break;
            default:
                Debug.LogError(
                    "No item was given to the player, either, the item added to the list was not given a case, or the id does not match a current case."
                );
                break;
        }
        InventoryPage.itemList[itemID].stacks += 1;
    }
}
