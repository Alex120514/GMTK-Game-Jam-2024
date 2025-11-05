using UnityEngine;
using System.Collections;

public class ResizeGrounds11 : MonoBehaviour
{
    public string groundTag = "Ground";
    public string energyCubeTag = "EnergyCube";
    public float resetDuration = 2.0f; // Duration of the reset effect
    public Vector3[] originalSizes; // To store original sizes
    public Vector3[] originalPositions; // To store original positions
    private GameObject[] grounds; // To store references to all ground objects
    private GameObject[] energyCubes; // To store references to all energy cube objects

    private bool isResetting = false; // Flag to check if resetting is happening
    public int Currentbutton = 0;
    private int Buttonnumber = 0;
    public EnergyCount energycount;
    private int Buttondifference;
    private int currentenergy;
    public PlayerMovement player; // Reference to the Player object

    void Start()
    {
        grounds = GameObject.FindGameObjectsWithTag(groundTag);
        energyCubes = GameObject.FindGameObjectsWithTag(energyCubeTag);
        originalSizes = new Vector3[grounds.Length];
        originalPositions = new Vector3[grounds.Length];
        for (int i = 0; i < grounds.Length; i++)
        {
            originalSizes[i] = grounds[i].transform.localScale;
            originalPositions[i] = grounds[i].transform.position;
        }

        // Cache original sizes and positions for energy cubes
        foreach (var energyCube in energyCubes)
        {
            // Assuming you might want to keep track of the original sizes and positions
            // If not, you can adjust these lines based on your needs.
        }
        
        UpdateButtonDifferenceAndEnergy();
    }

    public void ResetToOriginal()
    {
        if (!isResetting)
        {
            UpdateButtonDifferenceAndEnergy();

            if (currentenergy > 0 && Buttondifference == 1)
            {
                StartCoroutine(ResetGroundsCoroutine());
                StartCoroutine(ResetEnergyCubesCoroutine()); // Adjust energy cubes
                StartCoroutine(ResetPlayerCoroutine()); // Adjust player position
                Currentbutton = Buttonnumber;
                energycount.energy -= 1;
            }
        }
    }

    private void UpdateButtonDifferenceAndEnergy()
    {
        int CButton = Currentbutton;
        Buttondifference = Mathf.Abs(Buttonnumber - CButton);

        if (energycount != null)
        {
            currentenergy = energycount.energy;
        }
    }

    private IEnumerator ResetGroundsCoroutine()
    {
        isResetting = true;

        // Calculate the current sizes and positions
        Vector3[] currentSizes = new Vector3[grounds.Length];
        Vector3[] currentPositions = new Vector3[grounds.Length];
        for (int i = 0; i < grounds.Length; i++)
        {
            currentSizes[i] = grounds[i].transform.localScale;
            currentPositions[i] = grounds[i].transform.position;
        }

        float startTime = Time.time;

        while (Time.time - startTime < resetDuration)
        {
            float t = (Time.time - startTime) / resetDuration;

            for (int i = 0; i < grounds.Length; i++)
            {
                Vector3 newSize = Vector3.Lerp(currentSizes[i], originalSizes[i], t);
                Vector3 newPosition = Vector3.Lerp(currentPositions[i], originalPositions[i], t);

                grounds[i].transform.localScale = newSize;
                grounds[i].transform.position = newPosition;
            }

            yield return null;
        }

        for (int i = 0; i < grounds.Length; i++)
        {
            grounds[i].transform.localScale = originalSizes[i];
            grounds[i].transform.position = originalPositions[i];
        }

        isResetting = false;
    }

    private IEnumerator ResetEnergyCubesCoroutine()
    {
        Vector3[] originalPositions = new Vector3[energyCubes.Length];
        Vector3[] targetPositions = new Vector3[energyCubes.Length];

        for (int i = 0; i < energyCubes.Length; i++)
        {
            originalPositions[i] = energyCubes[i].transform.position;
        }

        for (int i = 0; i < energyCubes.Length; i++)
        {
            Vector3 pos = originalPositions[i];
            targetPositions[i] = (Currentbutton - Buttonnumber > 0) ?
                new Vector3(pos.x * 1.414f, pos.y / 1.414f, pos.z) :
                new Vector3(pos.x / 1.414f, pos.y * 1.414f, pos.z);
        }

        float startTime = Time.time;

        while (Time.time - startTime < resetDuration)
        {
            float t = (Time.time - startTime) / resetDuration;

            for (int i = 0; i < energyCubes.Length; i++)
            {
                energyCubes[i].transform.position = Vector3.Lerp(originalPositions[i], targetPositions[i], t);
            }

            yield return null;
        }

        for (int i = 0; i < energyCubes.Length; i++)
        {
            energyCubes[i].transform.position = targetPositions[i];
        }
    }

    private IEnumerator ResetPlayerCoroutine()
    {
        if (player == null) yield break;

        Vector3 originalPosition = player.transform.position;
        Vector3 targetPosition = (Currentbutton - Buttonnumber > 0) ?
            new Vector3(originalPosition.x * 1.414f, originalPosition.y / 1.414f, originalPosition.z) :
            new Vector3(originalPosition.x / 1.414f, originalPosition.y * 1.414f, originalPosition.z);

        float startTime = Time.time;

        while (Time.time - startTime < resetDuration)
        {
            float t = (Time.time - startTime) / resetDuration;
            player.transform.position = Vector3.Lerp(originalPosition, targetPosition, t);
            yield return null;
        }

        player.transform.position = targetPosition;
    }
}
