using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "NewItem", menuName = "Shop/Item")]

public class ItemData : ScriptableObject
{
    public string itemName;
    public string desc;
    public rarity rarity;
    public int baseCost; //Initial cost of the item
    public bool upgradeable;
    public List<UpgradeData> upgrades;
}
