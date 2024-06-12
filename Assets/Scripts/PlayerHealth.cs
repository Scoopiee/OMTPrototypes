using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int playerMaxHealth = 100;
    private int playerCurrentHealth; 
    public GameObject logicManager;
    private LogicScript logicScript;
    // Start is called before the first frame update
    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
        logicScript = FindObjectOfType<LogicScript>();
        if (logicScript != null)
        {
            logicScript.UpdateHealthUI(playerCurrentHealth);
            Debug.Log($"found logic script, updating health {logicScript}");
        }
        else
        {
            Debug.Log("COuldntr Find Logic Script");
        }        
        
        
    }
    
    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        
        playerCurrentHealth -= damage;  // Reduce the player's current health by the damage amount
        Debug.Log("Player Health: " + playerCurrentHealth);  // Log the current health to the console
        logicScript.UpdateHealthUI(playerCurrentHealth); // Tells logic to update health
        if (playerCurrentHealth <= 0)
        {
            Die();
        }
    }
    
    void Die()
    {
         Debug.Log("Player has Died");
         UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex); //Reload Scene
    }
   
}
