using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damageAmount = 5; // Amount of damage to deal to the player
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider that entered the trigger belongs to the player
        if (other.CompareTag("Player"))
        {
            // Get the PlayerHealth component from the player GameObject
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            // If the player has a PlayerHealth component, apply damage
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                Debug.Log("Player entered trigger and took damage");
            }
        }
    }
}
