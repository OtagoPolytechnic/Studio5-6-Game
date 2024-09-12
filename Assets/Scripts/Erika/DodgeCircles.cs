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
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.enabled = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(0.8773585f, 0f, 0f, alpha);
        StartCoroutine(FadeInCirlce());
    }

    // Update is called once per frame
    void Update()
    {
        if (alpha >= 1.0f)
        {
            // Check if player is within the circle
            // If player is within the circle, deal damage to player
            // If player is not within the circle, destroy the circle
            StopCoroutine(FadeInCirlce());
        }
    }

    private IEnumerator FadeInCirlce()
    {
        while (alpha < 1.0f)
        {
            alpha += alphaSpeed * Time.deltaTime * 60;
            if (alpha > 1.0f)
            {
                alpha = 1.0f;
            }
            spriteRenderer.color = new Color(0.8773585f, 0f, 0f, alpha);
            yield return null;
        }

        circleCollider.enabled = true;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Deal damage to player
            // Destroy the circle
            Debug.Log("Player has been hit by a circle");
        }
       
    }
}
