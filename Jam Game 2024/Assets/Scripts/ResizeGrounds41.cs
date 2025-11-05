using UnityEngine;
using System.Collections;

public class ResizeGrounds41 : MonoBehaviour
{
    public string groundTag = "Ground";
    public string energyCubeTag = "EnergyCube";
    public float resizeDuration = 2.0f; // Duration of the resizing effect
    public ResizeGrounds11 resizeGrounds;
    private int Buttonnumber = -2;
    public EnergyCount energycount;
    private int Buttondifference;
    private int currentenergy;
    public PlayerMovement player; // Reference to the Player object

    void Start()
    {
        UpdateButtonDifferenceAndEnergy();
    }

    // This method will be called by the UI button
    public void OnResizeButtonClick()
    {
        UpdateButtonDifferenceAndEnergy();

        if (currentenergy > 0 && Buttondifference == 1)
        {
            StartCoroutine(ResizeAndRepositionGrounds());
            StartCoroutine(ResizeAndRepositionEnergyCubes());
            StartCoroutine(ResizeAndRepositionPlayer()); // Adjust player position
            energycount.energy -= 1;
            resizeGrounds.Currentbutton = Buttonnumber;
        }
    }

    private void UpdateButtonDifferenceAndEnergy()
    {
        if (resizeGrounds != null)
        {
            int CButton = resizeGrounds.Currentbutton;
            Buttondifference = Mathf.Abs(Buttonnumber - CButton);
        }

        if (energycount != null)
        {
            currentenergy = energycount.energy;
        }
    }

    IEnumerator ResizeAndRepositionGrounds()
    {
        Vector3[] normalSizes = resizeGrounds.originalSizes;
        Vector3[] normalPositions = resizeGrounds.originalPositions;
        GameObject[] grounds = GameObject.FindGameObjectsWithTag(groundTag);
        Vector3[] originalSizes = new Vector3[grounds.Length];
        Vector3[] originalPositions = new Vector3[grounds.Length];
        for (int i = 0; i < grounds.Length; i++)
        {
            originalSizes[i] = grounds[i].transform.localScale;
            originalPositions[i] = grounds[i].transform.position;
        }

        Vector3[] targetSizes = new Vector3[grounds.Length];
        Vector3[] targetPositions = new Vector3[grounds.Length];
        for (int i = 0; i < grounds.Length; i++)
        {
            Vector3 size = normalSizes[i];
            targetSizes[i] = new Vector3(size.x * 2, size.y / 2, size.z);
            targetPositions[i] = new Vector3(normalPositions[i].x * 2, normalPositions[i].y / 2, normalPositions[i].z);
        }

        float startTime = Time.time;

        while (Time.time - startTime < resizeDuration)
        {
            float t = (Time.time - startTime) / resizeDuration;

            for (int i = 0; i < grounds.Length; i++)
            {
                Vector3 newSize = Vector3.Lerp(originalSizes[i], targetSizes[i], t);
                Vector3 newPosition = Vector3.Lerp(originalPositions[i], targetPositions[i], t);

                grounds[i].transform.localScale = newSize;
                grounds[i].transform.position = newPosition;
            }

            yield return null;
        }

        for (int i = 0; i < grounds.Length; i++)
        {
            grounds[i].transform.localScale = targetSizes[i];
            grounds[i].transform.position = targetPositions[i];
        }
    }

    IEnumerator ResizeAndRepositionEnergyCubes()
    {
        GameObject[] energyCubes = GameObject.FindGameObjectsWithTag(energyCubeTag);
        Vector3[] originalPositions = new Vector3[energyCubes.Length];
        Vector3[] targetPositions = new Vector3[energyCubes.Length];

        for (int i = 0; i < energyCubes.Length; i++)
        {
            originalPositions[i] = energyCubes[i].transform.position;
        }

        for (int i = 0; i < energyCubes.Length; i++)
        {
            Vector3 pos = originalPositions[i];
            targetPositions[i] = (resizeGrounds.Currentbutton - Buttonnumber > 0) ?
                new Vector3(pos.x * 1.414f, pos.y / 1.414f, pos.z) :
                new Vector3(pos.x / 1.414f, pos.y * 1.414f, pos.z);
        }

        float startTime = Time.time;

        while (Time.time - startTime < resizeDuration)
        {
            float t = (Time.time - startTime) / resizeDuration;

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

    IEnumerator ResizeAndRepositionPlayer()
    {
        if (player == null) yield break;

        Vector3 originalPosition = player.transform.position;
        Vector3 targetPosition = (resizeGrounds.Currentbutton - Buttonnumber > 0) ?
            new Vector3(originalPosition.x * 1.414f, originalPosition.y / 1.414f, originalPosition.z) :
            new Vector3(originalPosition.x / 1.414f, originalPosition.y * 1.414f, originalPosition.z);

        float startTime = Time.time;

        while (Time.time - startTime < resizeDuration)
        {
            float t = (Time.time - startTime) / resizeDuration;
            player.transform.position = Vector3.Lerp(originalPosition, targetPosition, t);
            yield return null;
        }

        player.transform.position = targetPosition;
    }
}
