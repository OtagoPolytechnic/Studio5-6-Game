using UnityEngine;

public class Lantern : MonoBehaviour
{
    public bool isActive = false; // Track lantern state
    public Light lanternLight; // Reference to the Point Light

    void Start()
    {
        lanternLight.enabled = isActive;
    }

    public void ToggleLantern()
    {
        isActive = !isActive;
        lanternLight.enabled = isActive;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ToggleLantern();
        }
    }
}
