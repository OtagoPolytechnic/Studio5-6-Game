using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewItem", menuName = "Shop/Item")]

public class ItemData : ScriptableObject
{
    public string itemName;
    public string desc;
    public rarity rarity;
    public bool upgradeable;
    public List<UpgradeData> upgrades;
    public UnityEvent triggerEvent;
}
