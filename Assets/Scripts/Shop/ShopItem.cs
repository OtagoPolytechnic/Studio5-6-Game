using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopItem : MonoBehaviour 
{
    public string itemName;
    public float cost;
    public bool upgradeable;
    private const int MAXLEVEL = 5;
    private int level = 1;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI costText;
    public Button button;


    private void Start()
    {
        itemNameText = transform.Find("title").GetComponent<TextMeshProUGUI>();
        itemNameText.text = itemName + " " + RomanNumeral(level);
        button = transform.Find("BT_Purchase").GetComponent<Button>();
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
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        itemNameText.text = itemName + " " + RomanNumeral(level);
        if (level == MAXLEVEL)
        {
            button.interactable = false;
            button.GetComponentInChildren<TextMeshProUGUI>().text = "OUT OF STOCK";
        }
        
    }
}