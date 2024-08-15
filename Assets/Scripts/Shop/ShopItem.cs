using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    private string itemName,
        itemDesc;
    private float cost;
    private bool upgradeable;
    private const int MAXLEVEL = 5;
    private int level = 1;
    private TextMeshProUGUI itemNameText,
        costText,
        descText;
    private Button button;

    private List<UpgradeData> upgrades = new List<UpgradeData>();

    public void SetItem(ItemData item)
    {
        this.itemName = item.itemName;
        this.cost = item.baseCost;
        this.upgradeable = item.upgradeable;
        this.itemDesc = item.desc;
        this.upgrades = item.upgrades;
    }

    private void Start()
    {
        itemNameText = transform.Find("title").GetComponent<TextMeshProUGUI>();
        costText = transform.Find("cost").GetComponent<TextMeshProUGUI>();
        descText = transform.Find("desc").GetComponent<TextMeshProUGUI>();
        button = transform.Find("BT_Purchase").GetComponent<Button>();
        UpdateUI();
    }

    private string RomanNumeral(int number)
    {
        string roman;
        switch (number)
        {
            case 1:
                roman = "I";
                break;
            case 2:
                roman = "II";
                break;
            case 3:
                roman = "III";
                break;
            case 4:
                roman = "IV";
                break;
            case 5:
                roman = "V";
                break;
            default:
                roman = "0";
                break;
        }
        return roman;
    }

    public void UpgradeClicked()
    {
        if (level < MAXLEVEL)
        {
            level++;
            cost *= 1.1f;
            foreach (UpgradeData data in upgrades)
            {
                StatUpgrade.SetStatValue(
                    data.stat,
                    PlayerHealth.instance.UpgradeStat(
                        StatUpgrade.GetStatValue(data.stat),
                        data.modifier,
                        data.percentUpgrade
                    )
                );
                Debug.Log(
                    "Upgrading "
                        + data.stat
                        + " to "
                        + StatUpgrade.GetStatValue(data.stat)
                        + " and "
                );
                Debug.Log(
                    PlayerHealth.instance.UpgradeStat(
                        StatUpgrade.GetStatValue(data.stat),
                        data.modifier,
                        data.percentUpgrade
                    )
                );
            }

            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        itemNameText.text = itemName + " " + RomanNumeral(level);
        costText.text = cost.ToString("F2");
        descText.text = itemDesc;

        if (level == MAXLEVEL)
        {
            button.interactable = false;
            button.GetComponentInChildren<TextMeshProUGUI>().text = "OUT OF STOCK";
        }
    }

    public void OnEnable()
    {
        Debug.Log("Enabling " + itemName);
    }
}
