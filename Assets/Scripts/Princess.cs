using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Princess : MonoBehaviour
{
    public GameObject victoryScreen;
    public TMP_Text victoryText;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //GameManager.Instance.Victory();
            Victory();
        }
    }

    //temp until bugs in Game Manager are fixed
    private void Victory()
    {
        victoryText.color = Color.green;
        victoryText.text = "Victory!";
        victoryScreen.SetActive(true);
    }
}
