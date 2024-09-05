using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Princess : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.Victory();
        }
    }
}
