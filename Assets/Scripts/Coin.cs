using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    public int coinValue;

    //adds to points and destroys coin on collection
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            ScoreManager.Instance.IncreasePoints(coinValue);
            Destroy(gameObject);
        }
    }
}
