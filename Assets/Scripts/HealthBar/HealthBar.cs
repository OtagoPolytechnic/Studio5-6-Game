using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Text healthText;
    private Image healthBar;

    void Start()
    {
        healthBar = GetComponent<Image>();
    }

    void Update()
    {
        float healthFraction = PlayerHealth.currentHealth / PlayerHealth.instance.MaxHealth;
        healthBar.fillAmount = healthFraction;
        healthText.text = PlayerHealth.currentHealth.ToString("F0") + "/" + PlayerHealth.instance.MaxHealth.ToString("F0");
    }
}

