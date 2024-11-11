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

    public void ShowLantern() => GameObject.Find("Lantern").GetComponent<Lantern>().enabled = true;
    public void IncreaseBulletAmount() 
    {
        PlayerHealth.hasShotgun = true;
        PlayerHealth.bulletAmount += 2;
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

        switch (itemID)
        {
            case 0:
                PlayerHealth.damage += 10;
                break;
            case 01:
                float current = PlayerHealth.instance.MaxHealth;
                PlayerHealth.instance.MaxHealth *= 1.10f;
                Mathf.RoundToInt(PlayerHealth.instance.MaxHealth);
                PlayerHealth.currentHealth += PlayerHealth.instance.MaxHealth - current;
                break;
            case 02:
                TopDownMovement.moveSpeed *= 1.05f;
                break;
            case 03:
                PlayerHealth.regenAmount += 1f;
                break;
            case 04:
                Shooting.firerate *= 0.9f;
                break;
            case 05:
                EnemyHealth.bleedAmount += 5;
                PlayerHealth.bleedTrue = true;
                break;
            case 06:
                PlayerHealth.lifestealAmount += 1f;
                break;
            case 07:
                PlayerHealth.explosionSize += 1;
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

                break;
            case 10:
                PlayerHealth.instance.MaxHealth /= 2f;
                if (PlayerHealth.instance.MaxHealth <= PlayerHealth.currentHealth)
                {
                    PlayerHealth.currentHealth = PlayerHealth.instance.MaxHealth;
                }
                else
                {
                    PlayerHealth.currentHealth /= 2f;
                }
                PlayerHealth.damage *= 2;

                break;
            case 11:
                PlayerHealth.hasShotgun = true;
                PlayerHealth.bulletAmount += 2;
                break;
            case 12:
                for (int i = 0; i < 2; i++)
                {
                    int randomRoll = UnityEngine.Random.Range(0, 4);
                    if (randomRoll == 0)
                    {
                        PlayerHealth.damage += 5;
                    }
                    else if (randomRoll == 1)
                    {
                        float current2 = PlayerHealth.instance.MaxHealth;
                        PlayerHealth.instance.MaxHealth *= 1.05f;
                        Mathf.RoundToInt(PlayerHealth.instance.MaxHealth);
                        PlayerHealth.currentHealth += PlayerHealth.instance.MaxHealth - current2;
                    }
                    else if (randomRoll == 2)
                    {
                        TopDownMovement.moveSpeed *= 1.025f;
                    }
                    else if (randomRoll == 3)
                    {
                        Shooting.firerate *= 0.95f;
                    }
                }

                break;
            case 13:
                //PlayerHealth.hasLantern = true;
                break;
            case 14:
                //PlayerHealth.coins ++;

                break;
            default:

                break;
        }
        InventoryPage.itemList[itemID].stacks += 1;
    }
}
