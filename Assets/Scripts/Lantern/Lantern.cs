using UnityEngine;

public class Lantern : MonoBehaviour
{
    public bool isActive = false; // Lantern state
    public GameObject blackOverlay; // Reference to the black overlay

    void ToggleLantern()
    {
        isActive = !isActive;

        if (isActive)
        {
            blackOverlay.SetActive(false); // Disable overlay
        }
        else
        {
            blackOverlay.SetActive(true); // Enable overlay
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ToggleLantern();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ToggleLantern();
        }
    }
}
