using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public int id;
    public string name;
    public string desc;
    public rarity rarity;
    public int stacks;
    public int baseCost; //Initial cost of the item
    public Action itemAction; //This can be called to apply the item's effect such as increasing player health or damage
    public bool isStackable = true; //Whether the item can be stacked in the inventory
    public bool isInShop = true; //Whether the item can be found in the shop
}

public enum rarity{
    Common,
    Uncommon,
    Rare,
    Epic
}

public class InventoryPage : MonoBehaviour
{

    public static InventoryPage instance;
    [SerializeField] 
    private InventoryItem itemPrefab;
    [SerializeField]
    private RectTransform contentPanel;


    private int index;
    private rarity roll;    
    private List<InventoryItem> preGeneratedItems = new List<InventoryItem>();

    void Awake()
    {
        instance = this;
    }

    //make sure not to dupelicate the item ids
    public static List<Item> itemList = new List<Item>{ //char limit of 99 in description 
        new() { id = 0, name = "Sharpened Talons", desc = "Increases damage you deal", rarity = rarity.Common, stacks = 0, itemAction = () => { PlayerHealth.instance.UpgradeStat(PlayerHealth.damage,10); } , baseCost = 1},
        new() { id = 01, name = "Oats", desc = "Gives you more max health", rarity = rarity.Common, stacks = 0 , itemAction = () => { PlayerHealth.instance.UpgradeStat(PlayerHealth.maxHealth,1.1f,true);  } , baseCost = 5},
        new() { id = 02, name = "Boots", desc = "Increases your waddle speed", rarity = rarity.Common, stacks = 0 , itemAction = () => { PlayerHealth.instance.UpgradeStat(TopDownMovement.moveSpeed,1.05f,true); } , baseCost = 4},
        new() { id = 03, name = "Band-Aid", desc = "Your health slowly regenerates over time", rarity = rarity.Rare, stacks = 0, itemAction = () => { PlayerHealth.instance.UpgradeStat(PlayerHealth.regenAmount,1f); }, baseCost = 2 },
        new() { id = 04, name = "Illegal Trigger", desc = "You shoot faster", rarity = rarity.Common, stacks = 0 , itemAction = () => { PlayerHealth.instance.UpgradeStat(Shooting.firerate,0.9f,true); } , baseCost = 1},
        new() { id = 05, name = "Chompers", desc = "Your hits bleed enemies", rarity = rarity.Uncommon, stacks = 0 , itemAction = () => { PlayerHealth.instance.UpgradeStat(EnemyHealth.bleedAmount,5); } , baseCost = 1},
        new() { id = 06, name = "Leech", desc = "Your hits on enemies heal you", rarity = rarity.Rare, stacks = 0 , itemAction = () => { PlayerHealth.instance.UpgradeStat(PlayerHealth.lifestealAmount,1f); } , baseCost = 1},
        new() { id = 07, name = "Explosive Bullets", desc = "Your bullets explode on impact", rarity = rarity.Rare, stacks = 0, itemAction = () => { PlayerHealth.instance.UpgradeStat(PlayerHealth.explosionSize,1); PlayerHealth.instance.UpgradeStat(PlayerHealth.explosionSize,1); }, baseCost = 1},
        new() { id = 08, name = "Egg", desc = "You gain an extra life", rarity = rarity.Epic, stacks = 0 ,itemAction = () => { ItemController.instance.IncreaseLives(); } , baseCost = 1},
        new() { id = 09, name = "Lucky Feather", desc = "You have an increased chance to deal critical damage" , rarity = rarity.Uncommon, stacks = 0, itemAction = () => { PlayerHealth.instance.UpgradeStat(PlayerHealth.CritChance,0.07f); }, baseCost = 1 },
        new() { id = 10, name = "Glass Cannon", desc = "Halves your health to double your damage", rarity = rarity.Epic, stacks = 0,itemAction = () => { ItemController.instance.GlassCannon(); } , baseCost = 8},
        new() { id = 11, name = "Shotgun", desc = "You shoot a spread of bullets instead of one", rarity = rarity.Epic, stacks = 0 ,itemAction = () => { ItemController.instance.IncreaseBulletAmount(); } , baseCost = 8},
        new() { id = 12, name = "Lucky Dive", desc = "Gain two random basic stats at half strength", rarity = rarity.Uncommon, stacks = 0, itemAction = () => { ItemController.instance.LuckyDive(); } , baseCost = 1},
        new() { id = 13, name = "Lantern", desc = "Allows you to see in the dark", rarity = rarity.Uncommon, stacks = 0, itemAction = () => { Debug.Log("Lantern = true"); } , baseCost = 3, isStackable = false},
        new() { id = 14, name = "Coin", desc = "Spend it", rarity = rarity.Common, stacks = 0,itemAction = () => { Debug.Log("playerCoins++"); } , baseCost = 4 , isInShop = false},
    };
    //in this list, there cannot be less than 3 of each rarity for the case that 3 of one rarity is picked on the item selection. 

    public rarity GetWeightedRarity() {

        // not consts currently incase we want these values to change over time with the waves
        float commonRoll = 0.5f; //50%
        float uncommonRoll = 0.8f; //30%
        float rareRoll = 0.95f; //15%
        float epicRoll = 1f;  //5%  

        // generate a random value (0->1)
        float randomRarity = UnityEngine.Random.value;

        // Figure out which threshold this falls under.
        if (randomRarity < commonRoll)
        {
            roll = rarity.Common;
        }
        else if (randomRarity < uncommonRoll)
        {
            roll = rarity.Uncommon;
        }
        else if (randomRarity < rareRoll)
        {
            roll = rarity.Rare;
        }
        else if (randomRarity < epicRoll)
        {
            roll = rarity.Epic;
        }
        return roll;
    }

    public void InitializeInventoryUI(int inventorySize, bool shop = false) //this is called every time the inventory ui pops up
    { 
        if (shop)
        {
            /// Remove any items that shouldn't be in the shop
            List<Item> itemsToRemove = new List<Item>();
            foreach (Item i in itemList)
            {
                if (!i.isInShop)
                {
                    itemsToRemove.Add(i);
   
                }
            }
            foreach (Item i in itemsToRemove)
            {
                itemList.Remove(i);
            }
        }
        for (int i = preGeneratedItems.Count - 1; i >= 0; i--) //makes sure the items previously generated are cleared to not bunch up on the inventory window
        {
            Destroy(preGeneratedItems[i].gameObject); //removes item
            preGeneratedItems.RemoveAt(i); //removes floating null pointer
        } 

        List<Item> generatedRarityList = new List<Item>();
        List<Item> selectedItems = new List<Item>();

        for (int i = 0; i < inventorySize; i++) 
        {
            // Get a random rarity.
            rarity randomItemRarity = GetWeightedRarity();
            // Create a list of all the available items of that rarity.
            foreach (Item j in itemList)
            {
                if (j.rarity == randomItemRarity)
                {
                    generatedRarityList.Add(j);
                }
            }
            //if the item has already been selected, remove it from the possible pool of items
            foreach (Item k in selectedItems)
            {
                if (generatedRarityList.Contains(k))
                {
                    generatedRarityList.Remove(k);
                }
            }
            // then pick a random index from that subset and use that as the item.
            index = UnityEngine.Random.Range(0, generatedRarityList.Count); 
            selectedItems.Add(generatedRarityList[index]);

            InventoryItem item = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);  
            //assign the item generated to the item prefab and set transfoms
            item.GetComponent<InventoryItem>().itemID = generatedRarityList[index].id;
            item.GetComponent<InventoryItem>().itemName = generatedRarityList[index].name;
            item.GetComponent<InventoryItem>().itemDesc = generatedRarityList[index].desc;
            item.GetComponent<InventoryItem>().itemRarity = generatedRarityList[index].rarity;
            item.GetComponent<InventoryItem>().itemStacks = generatedRarityList[index].stacks;
            item.GetComponent<InventoryItem>().clicked = generatedRarityList[index].itemAction;
            item.GetComponent<InventoryItem>().cost = generatedRarityList[index].baseCost + generatedRarityList[index].stacks;
            item.transform.SetParent(contentPanel);
            item.transform.localScale = new Vector3(1, 1, 1); //this is to fix the parent scale issue. See https://github.com/BIT-Studio-4/Duck-Game/issues/65 for context
            Debug.Log($"In InventoryPage.cs: index chosen is {index} and item is {generatedRarityList[index].name}");
            //add the item to cleanup on next method call
            preGeneratedItems.Add(item);
            generatedRarityList.Clear();
        }
    }
                
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
