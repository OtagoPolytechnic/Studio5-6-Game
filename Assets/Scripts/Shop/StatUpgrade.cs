using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatUpgrade : MonoBehaviour
{
    public int level = 1;
    public float baseCost = 2f;
    public Stats stat;
    public float value;
    public bool percentUpgrade;
    public float upgradeModifier;
    public TextMeshProUGUI currentLevelText;

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
    private void Start()
    {
        if (GetStatValue(stat) != -1)
        {
            value = GetStatValue(stat);
        }
        else
        {
            value = 0;
        }
        currentLevelText.text = value.ToString();
        nextLevelText.text = (PlayerHealth.instance.UpgradeStat(value,upgradeModifier,percentUpgrade)).ToString();
        costText.text = "$" + baseCost.ToString();
        titleText.text = ConvertStatToString(stat);
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

}
