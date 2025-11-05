using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public ResizeGrounds11 resizeGrounds; // Reference to the ResizeGrounds11 script
    public Color activeColor = Color.green; // Color when active
    private Color originalColor; // Original color of the Exit object
    private Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            originalColor = renderer.material.color; // Store the original color
        }
    }

    void Update()
    {
        if (resizeGrounds != null && resizeGrounds.Currentbutton == 0)
        {
            // Change color to green
            if (renderer != null)
            {
                renderer.material.color = activeColor;
            }
        }
        else
        {
            // Revert to original color
            if (renderer != null)
            {
                renderer.material.color = originalColor;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && renderer != null && renderer.material.color == activeColor)
        {
            Debug.Log("Activated");
            // Load scene2 when player collides with Exit and color is green
            SceneManager.LoadScene("scene1");
        }
    }
}
