using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is attached to each circle prefab
// alpha value should go from 0 to 1
// when alpha is 1, check if player is within the circle
// if player is within the circle, deal damage to player
// if player is not within the circle, destroy the circle

public class DodgeCircles : MonoBehaviour
{
    private float alpha = 0.0f;
    private float alphaSpeed = 0.01f;

    private CircleCollider2D circleCollider;

    // Start is called before the first frame update
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (alpha < 1.0f)
        {
            alpha += alphaSpeed;
            GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, alpha);
        }
        else
        {
            circleCollider.enabled = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Deal damage to player
            // Destroy the circle
        }
        else if (collision == null)
        {
            Destroy(gameObject);
        }
    }
}
