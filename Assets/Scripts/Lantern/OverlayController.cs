using UnityEngine;
using UnityEngine.UI;

public class OverlayController : MonoBehaviour
{
    public Lantern lantern; // Reference to the Lantern script
    private Image overlayImage;

    void Start()
    {
        overlayImage = GetComponent<Image>();
        UpdateOverlay();
    }

    void Update()
    {
        UpdateOverlay();
    }

    void UpdateOverlay()
    {
        overlayImage.enabled = !lantern.isActive;
    }
}
