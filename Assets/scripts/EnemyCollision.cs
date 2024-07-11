using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public int initialDamage = 1; // Initial damage upon collision
    public int damagePerTick = 1; // Damage per tick
    public float tickRate = 1f; // Rate at which damage is applied (ticks per second)
    private Health playerHealth; // Reference to the player's Health component
    private bool isColliding = false; // Flag to track collision state
    private float timeSinceLastTick = 0f; // Time elapsed since last damage tick

    private void Start()
    {
        playerHealth = FindObjectOfType<Health>(); // Assuming there's only one Health component in the scene
    }

    private void Update()
    {
        // If the collision is active, apply damage over time
        if (isColliding)
        {
            timeSinceLastTick += Time.deltaTime;
            if (timeSinceLastTick >= 1f / tickRate)
            {
                // Apply damage per tick
                playerHealth.TakeDamage(damagePerTick);
                timeSinceLastTick = 0f; // Reset timeSinceLastTick for next tick
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Ensure playerHealth is not null
            if (playerHealth != null)
            {
                // Start dealing initial damage when player enters the trigger
                playerHealth.TakeDamage(initialDamage);
                isColliding = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Stop dealing damage when player exits the trigger
            isColliding = false;
            timeSinceLastTick = 0f; // Reset timeSinceLastTick when collision ends
        }
    }
}
