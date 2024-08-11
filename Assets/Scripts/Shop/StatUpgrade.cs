using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatUpgrade : MonoBehaviour
{
    public int level = 1;
    public float baseCost = 2f;
    public Stats stat;
    public TextMeshProUGUI currentLevelText;

    public TextMeshProUGUI nextLevelText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI titleText;



    private void Start()
    {
        currentLevelText.text = level.ToString();
        nextLevelText.text = "";
        costText.text = "$" + baseCost.ToString();
        titleText.text = ConvertStatToString(stat);
    }
    public void Initialise()
    {

    }
    public string ConvertStatToString(Stats stat)
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
