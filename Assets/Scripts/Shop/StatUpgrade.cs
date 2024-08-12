using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatUpgrade : MonoBehaviour
{
    public int level = 1;
    public float baseCost = 2f;
    public Stats stat;
    public float value;
    public bool percentUpgrade;
    public float upgradeModifier;
    public TextMeshProUGUI currentLevelText;
    public Button button;
    public TextMeshProUGUI nextLevelText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI titleText;


    private float GetStatValue(Stats stat)
    {
        switch(stat)
        {
            case Stats.SPEED:
                return TopDownMovement.moveSpeed;
                break;
            case Stats.DAMAGE:
                return PlayerHealth.damage;
                break;
            case Stats.HEALTH:
                return PlayerHealth.maxHealth;
                break;
        }
        return -1;

    }
    private void SetStatValue(Stats stat, float value)
    {
        switch(stat)
        {
            case Stats.SPEED:
                TopDownMovement.moveSpeed = value;
                break;
            case Stats.DAMAGE:
                PlayerHealth.damage =(int) value;
                break;
            case Stats.HEALTH:
                PlayerHealth.maxHealth = value;
                break;
        }
    }
    public void UpgradeClicked()
    {
        SetStatValue(stat, PlayerHealth.instance.UpgradeStat(GetStatValue(stat), upgradeModifier, percentUpgrade));
        baseCost += level;
        UpdateUI();
    }
    private void Start()
    {
   

        switch(stat)
        {
            case Stats.SPEED:
                upgradeModifier = 1.05f;
                percentUpgrade = true;
                break;
            case Stats.DAMAGE:
                upgradeModifier = 10f;
                percentUpgrade = false;
                break;
            case Stats.HEALTH:
                upgradeModifier = 1.1f;
                percentUpgrade = true;
                break;
        }
        UpdateUI();
    }

    private string ConvertStatToString(Stats stat)
    {
        switch(stat)
        {
            case Stats.SPEED:
                return "Speed";
                break;
            case Stats.DAMAGE:
                return "Damage";
                break;
            case Stats.HEALTH:
                return "Health";
                break;
            default:
                return "Null";
                break;
        }
        return "Null";
    }

    private void UpdateUI()
    {
            currentLevelText.text = GetStatValue(stat).ToString("F2");
        nextLevelText.text = (PlayerHealth.instance.UpgradeStat(GetStatValue(stat) ,upgradeModifier,percentUpgrade)).ToString("F2");
        costText.text = "$" + baseCost.ToString();
        titleText.text = ConvertStatToString(stat);
    }

}
