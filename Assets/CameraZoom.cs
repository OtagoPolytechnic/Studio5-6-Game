using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 1.0f; // Speed of zooming
    public float minZoom = 3.0f;   // Minimum zoom limit
    public float maxZoom = 10.0f;  // Maximum zoom limit

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();

        // Ensure the camera is in orthographic mode
        if (!cam.orthographic)
        {
            Debug.LogError("This script only works with orthographic cameras.");
        }
    }

    void Update()
    {
        // Check if CTRL is held down
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            // Get mouse scroll wheel input (positive scrolls up, negative scrolls down)
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (scroll != 0f)
            {
                // Adjust the camera's orthographic size based on the scroll input
                cam.orthographicSize -= scroll * zoomSpeed;
                cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom); // Clamp to keep within min and max zoom limits
            }
        }
    }
}
