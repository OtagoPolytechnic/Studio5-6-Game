using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatUpgrade : MonoBehaviour
{
    private int level = 1;
    private const int MAXLEVEL = 10;
    private float baseCost = 25f;
    private Stats stat;
    public Stats Stat { get => stat; set => stat = value; }
    private bool applyAsMultiple;
    private float upgradeModifier;
    public TextMeshProUGUI currentLevelText;
    public Button button;
    public TextMeshProUGUI nextLevelText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI titleText;


    public static float GetStatValue(Stats stat)
    {
        if (stat == Stats.SPEED)
        {
            return TopDownMovement.moveSpeed;
        }
        else if (stat == Stats.DAMAGE)
        {
            return PlayerHealth.damage;
        }
        else if (stat == Stats.HEALTH)
        {
            return PlayerHealth.instance.MaxHealth;
        }

        return -1;

    }
    public static void SetStatValue(Stats stat, float value)
    {
        switch(stat)
        {
            case Stats.SPEED:
                TopDownMovement.moveSpeed = value;
                break;
            case Stats.DAMAGE:
                PlayerHealth.damage = value;
                break;
            case Stats.HEALTH:
                PlayerHealth.instance.MaxHealth = value;
                break;
        }
    }
    public void UpgradeClicked()
    {
        if (level < MAXLEVEL && PlayerHealth.instance.playerGold >= baseCost)
        {
            SetStatValue(Stat, PlayerHealth.instance.UpgradeStat(GetStatValue(Stat), upgradeModifier, applyAsMultiple));
            ScoreManager.Instance.SpendGold((int)baseCost);
            baseCost += 10;
            level++;
            UpdateUI();
        }
    }
    private void Start()
    {
   
        switch(Stat)
        {
            case Stats.SPEED:
                upgradeModifier = 1.05f;
                applyAsMultiple = true;
                break;
            case Stats.DAMAGE:
                upgradeModifier = 10f;
                applyAsMultiple = false;
                break;
            case Stats.HEALTH:
                upgradeModifier = 1.1f;
                applyAsMultiple = true;
                break;
        }
        UpdateUI();
    }

    private string ConvertStatToString(Stats stat)
    {
        string output = null;
        switch(stat)
        {
            case Stats.SPEED:
                output = "Speed";
                break;
            case Stats.DAMAGE:
                output = "Damage";
                break;
            case Stats.HEALTH:
                output = "Health";
                break;
            default:
                output = "Null";
                break;
        }
        return output;
    }

    private void UpdateUI()
    {
        currentLevelText.text = GetStatValue(Stat).ToString("F2");
        nextLevelText.text = (PlayerHealth.instance.UpgradeStat(GetStatValue(Stat) ,upgradeModifier,applyAsMultiple)).ToString("F2");
        costText.text = "$" + baseCost.ToString("F2");
        titleText.text = ConvertStatToString(Stat);
        if (level >= MAXLEVEL)
        {
            button.interactable = false;
            button.GetComponentInChildren<TextMeshProUGUI>().text = "MAX";
        }
        else if (PlayerHealth.instance.playerGold < baseCost)
        {
            button.interactable = false;
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Upgrade";

        }
        else
        {
            button.interactable = true;
            button.GetComponentInChildren<TextMeshProUGUI>().text = "Upgrade";
        }

    }

    public void OnEnable()
    {
        UpdateUI();
        Debug.Log("THE  " + GetStatValue(Stat));
    }

}
