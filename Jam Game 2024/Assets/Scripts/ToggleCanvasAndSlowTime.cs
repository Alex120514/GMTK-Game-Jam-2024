using UnityEngine;

public class ToggleCanvasAndSlowTime : MonoBehaviour
{
    public Canvas Canvas; // Reference to the Scale Canvas
    private bool isCanvasVisible = false; // Track whether the Canvas is currently visible

    void Start()
    {
        // Ensure the canvas is hidden at the start
        if (Canvas != null)
        {
            Canvas.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Canvas != null)
            {
                // Toggle canvas visibility
                isCanvasVisible = !isCanvasVisible;
                Canvas.gameObject.SetActive(isCanvasVisible);

                // Set the time scale based on canvas visibility
                Time.timeScale = isCanvasVisible ? 0f : 1f;
            }
        }
    }
}
