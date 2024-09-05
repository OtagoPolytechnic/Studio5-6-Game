using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject gameOverUI;
    public ScoreManager scoreManager;
    public TMP_Text gameOverText;
   private bool playerDead = false;

    void Awake()
    {
        Instance = this;
    }
    public void GameOver()
   {
    //This should be handled under a game state end or dead
        if (!playerDead)
        {
            playerDead = true;
            //disables shooting and movement
            FindObjectOfType<Shooting>().enabled = false;
            FindObjectOfType<TopDownMovement>().enabled = false;
            SFXManager.Instance.GameOverSound();
            scoreManager.FinalScore();
            gameOverUI.SetActive(true);
        }
        //call kill all active enemies
            CullEnemies();
   }
    public void CullEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");

        foreach (GameObject enemy in enemies)
        {
            EnemySpawner.currentEnemies.Remove(enemy);
            Destroy(enemy);
        }
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
    }

    //call this in Princess script when bugs are fixed
    public void Victory()
    {
        gameOverText.color = Color.green;
        gameOverText.text = "Victory!";
        gameOverUI.SetActive(true);
    }

    public void Restart() 
    {
        ResetVariables();
        SceneManager.LoadScene("MainScene");
    }

    public void MainMenu() 
    {
        SceneManager.LoadScene("Titlescreen");
    }

    public void ResetVariables() //Any static variables that need to be reset on game start should be added to this method
    {
        //Player variables
        PlayerHealth.instance.MaxHealth = 100;
        PlayerHealth.regenAmount = 0;
        PlayerHealth.lifestealAmount = 0;
        PlayerHealth.damage = 20;
        PlayerHealth.explosionSize = 0;
        PlayerHealth.CritChance = 0.01f;
        PlayerHealth.hasShotgun = false;
        PlayerHealth.bulletAmount = 0;

        Shooting.firerate = 0.5f;

        TopDownMovement.moveSpeed = 10f;

        PlayerHealth.bleedTrue = false;
        EnemyHealth.bleedAmount = 0;

        //Enemy variables
        EnemySpawner.healthMultiplier = 1f;
        EnemySpawner.spawnTimer = 5f;

        //Item stacks
        foreach (Item i in InventoryPage.itemList)
        {
            i.stacks = 0;
        }
    }
}
