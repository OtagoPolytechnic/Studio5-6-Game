using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    //if you change something in this list you need to change it in InventoryPage.cs's list named itemlist
    public GameObject eggPrefab;
    public void ItemPicked(int itemID)
    {
        Debug.Log(itemID);

        switch(itemID) 
        {
            case 0:
                PlayerHealth.damage += 10;
                Debug.Log($"Damage: {PlayerHealth.damage}");
            break;
            case 01:
                PlayerHealth.maxHealth *= 1.10f;
                Math.Round(PlayerHealth.maxHealth, 0, MidpointRounding.AwayFromZero);
                Debug.Log($"Max health: {PlayerHealth.maxHealth}");
            break;
            case 02:
                TopDownMovement.moveSpeed *= 1.05f;
                Debug.Log($"Speed: {TopDownMovement.moveSpeed}");           
            break;
            case 03:
                PlayerHealth.regenAmount += 5f;
                PlayerHealth.regenTrue = true;
                Debug.Log($"Regen amount: {PlayerHealth.regenAmount}"); 
            break;
            case 04:
                Shooting.firerate *= 0.9f;
                Debug.Log($"Firerate: {Shooting.firerate}"); 
            break;
            case 05:
                EnemyHealth.bleedAmount = 5f;
                EnemyHealth.bleedTrue = true;
                Debug.Log($"Bleed amount: {EnemyHealth.bleedAmount}"); 
            break;
            case 06:
                PlayerHealth.lifestealAmount = 5f;
                Debug.Log($"Lifesteal amount: {PlayerHealth.lifestealAmount}"); 
            break;
            case 07:
                PlayerHealth.explosiveBullets = true;
                PlayerHealth.explosionAmount +=1;
            break;     
            case 08:
                Instantiate(eggPrefab,  new Vector3(0,0,0), Quaternion.identity, GameObject.Find("Nest").transform);
            break;
            case 09:
                PlayerHealth.critChance += 0.15f;
                if (PlayerHealth.critChance >= 1)
                {
                    PlayerHealth.critChance = 1;
                }
            break;
            case 10:
                PlayerHealth.maxHealth /= 0.50f;
                if (PlayerHealth.maxHealth <= PlayerHealth.currentHealth)
                {
                    PlayerHealth.currentHealth = PlayerHealth.maxHealth;
                }
                else
                {
                    PlayerHealth.currentHealth /= 0.50f;
                }
                PlayerHealth.damage *= 2;
            break;
            case 11:
                PlayerHealth.hasShotgun = true;
                PlayerHealth.bulletAmount += 2;
            break;
            default:
                Debug.LogError("No item was picked, either there is a new item added that hasn't been mirrored here or an item's name is incorrect.");
            break;
        }
    }
}
