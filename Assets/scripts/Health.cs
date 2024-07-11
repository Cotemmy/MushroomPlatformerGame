using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int NumberOfHearts;

    public Image[] heart;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public Transform respawnPoint; // Respawn point object
    public SampleCollector sampleCollector; // Reference to SampleCollector

    void Update()
    {
        if (health > NumberOfHearts)
        {
            health = NumberOfHearts;
        }
        for (int i = 0; i < heart.Length; i++)
        {
            if (i < health)
            {
                heart[i].sprite = fullHeart;
            }
            else
            {
                heart[i].sprite = emptyHeart;
            }
            if (i < NumberOfHearts)
            {
                heart[i].enabled = true;
            }
            else
            {
                heart[i].enabled = false;
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        // Move player to the respawn point
        if (respawnPoint != null)
        {
            transform.position = respawnPoint.position;
            Physics.SyncTransforms();
            health = NumberOfHearts; // Reset health

            // Call the ResetSamples method of the SampleCollector instance
            sampleCollector = SampleCollector.GetInstance();
            if (sampleCollector != null)
            {
                sampleCollector.ResetSamples();
            }
            else
            {
                Debug.LogWarning("SampleCollector not set!");
            }
        }
        else
        {
            Debug.LogWarning("Respawn point not set!");
            // Optionally, handle the situation where the respawn point is not set
        }
    }
}
