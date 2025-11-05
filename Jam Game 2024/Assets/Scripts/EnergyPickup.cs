using UnityEngine;

public class EnergyPickup : MonoBehaviour
{
    public EnergyCount energycount;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            energycount.energy += 1;
            gameObject.SetActive(false);
        }
    }
}
