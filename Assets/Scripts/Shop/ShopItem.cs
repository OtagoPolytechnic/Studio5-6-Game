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
    private ItemData item;

    public void SetItem(ItemData item) //Set all the required data for the item
    {
        this.itemName = item.itemName;
        this.cost = CatShop.instance.GetRarityToPrice(item.rarity);
    
        this.upgradeable = item.upgradeable;
        this.itemDesc = item.desc;
        this.upgrades = item.upgrades;
        this.item = item;
    }

    private void Start()
    {
        itemNameText = transform.Find("title").GetComponent<TextMeshProUGUI>();
        costText = transform.Find("cost").GetComponent<TextMeshProUGUI>();
        descText = transform.Find("desc").GetComponent<TextMeshProUGUI>();
        button = transform.Find("BT_Purchase").GetComponent<Button>();
        UpdateUI();
    }

    private string RomanNumeral(int number) //Convert a number to a roman numeral for the level of that item
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
        if (level < MAXLEVEL && PlayerHealth.instance.playerGold >= cost)
        {
            if(upgradeable) //If the item is upgradeable, increase the level otherwise set it to 2 as it is a one time purchase
                level++;
            else
                level = 2;
            ScoreManager.Instance.SpendGold((int)cost);
            cost += 5; //Increase the cost of the item by a fixed amount
            foreach (UpgradeData data in upgrades)
            {
                StatUpgrade.SetStatValue(
                    data.stat,
                    PlayerHealth.instance.UpgradeStat(
                        StatUpgrade.GetStatValue(data.stat),
                        data.modifier,
                        data.applyAsMultiple
                    )
                );
            }
                item.triggerEvent.Invoke();

            UpdateUI();
        }
    }

    private void UpdateUI() //Update the UI based on the level of the item or if it is out of stock
    {
        if(upgradeable)
            itemNameText.text = itemName + " " + RomanNumeral(level);
        else
            itemNameText.text = itemName;
        costText.text = cost.ToString("F2");
        descText.text = itemDesc;

        if ((upgradeable && level == MAXLEVEL) || (!upgradeable && level == 2))
        {
            button.interactable = false;
            button.GetComponentInChildren<TextMeshProUGUI>().text = "OUT OF STOCK";
        }
        else if (PlayerHealth.instance.playerGold < cost)
        {
            button.interactable = false;
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Purchase";

        }
        else
        {
            button.interactable = true;
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Purchase";
        }
    }

}
