using UnityEngine;
using UnityEngine.UI; // For UI components
using TMPro; // For TextMeshPro

public class EnergyDisplay : MonoBehaviour
{
    public TextMeshProUGUI Energytext;

    public EnergyCount energyCount; // Reference to the EnergyCount component

    void Update()
    {
        if (energyCount != null)
        {
            // Update the text with the current energy value
            if (Energytext != null)
            {
                Energytext.text = "Energy: " + energyCount.energy.ToString();
            }
        }
    }
}
