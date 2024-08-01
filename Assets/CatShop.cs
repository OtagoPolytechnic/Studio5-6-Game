using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ShopItem
{
    public string itemName;
    public string itemDesc;
    public int itemCost;
    public ShopItem(string itemName, string itemDesc, int itemCost)
    {
        this.itemName = itemName;
        this.itemDesc = itemDesc;
        this.itemCost = itemCost;
    }
}
public class CatShop : MonoBehaviour
{
    
    private bool playerInShop = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player entered shop");
            playerInShop = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player left the shop");
            playerInShop = false;
        }
    }
    void Update()
    {
        if (playerInShop)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Open Shop Menu");
                InventoryController.instance.ShowInventory();

            }
        }
    }
}
