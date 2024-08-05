using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatShop : MonoBehaviour
{

    private bool playerInShop = false;
    private bool shopMenuOpen = false;
    public static CatShop instance;
    void Awake()
    {
        instance = this;
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            playerInShop = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInShop = false;
        }
    }
    void Update()
    {
        if (playerInShop)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!shopMenuOpen)
                {
                    OpenShop();
                }
                else
                {
                    CloseShop();
                }
            }
        }
    }

    public void OpenShop()
    {
        InventoryController.instance.ShowInventory();
        shopMenuOpen = true;
    }
    public void CloseShop()
    {
        InventoryController.instance.HideInventory();
        shopMenuOpen = false;
    }
}
