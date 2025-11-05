using UnityEngine;

public class Restartarea : MonoBehaviour
{
    public Spawnpointrecord spawnpointrecord; // Reference to the Spawnpointrecord script

    private void Update()
    {
        // Check if the player object's y coordinate is less than -20
        if (transform.position.y < -20f)
        {
            // Ensure spawnpointrecord is assigned
            if (spawnpointrecord != null)
            {
                // Set the player object's position to the spawnpointrecord's currentSpawnpoint
                transform.position = spawnpointrecord.currentSpawnpoint;
            }
            else
            {
                Debug.LogError("Spawnpointrecord is not assigned!");
            }
        }
    }
}
