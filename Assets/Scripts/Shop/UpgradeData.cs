using UnityEngine;
using System.Collections.Generic;
using System;
/// <summary>
/// The UpgradeData class holds all the data for an upgrade in the shop
/// </summary>
[Serializable]
public class UpgradeData
{
    public Stats stat; //The stat that the upgrade will affect
    public float modifier; //The amount that the stat will be increased by
    public bool applyAsMultiple;  //If the modifier should be applied as a multiple of the current value
}
