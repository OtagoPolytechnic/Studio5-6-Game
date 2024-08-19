using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewItem", menuName = "Shop/Item")]
//The ItemData class is a ScriptableObject that holds all the data for an item in the shop
public class ItemData : ScriptableObject
{
    public string itemName;
    public string desc;
    public rarity rarity;
    public bool upgradeable;
    public List<UpgradeData> upgrades; //The set of upgrades to apply to a stat
    public UnityEvent triggerEvent; //The event to trigger when the item is purchased
}
