/**************************************************************************************************************
* <Player> Class
*
* Player class which inherits from the character class to give the player all of the functionality of such.
* Contains information such as current active powerup and health 
* 
* Created by: <Aidan McCarthy> 
* Date: <24/06/2024>
*
***************************************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private SpriteRenderer spriteRenderer;
    private bool isInvisible = false;
    public float speed = 8f;
    public float jumpHeight = 16f;
    public string ActivePowerup = "";
   
    // Set health and grab relevant components
    void Awake()
    {
        health = 100;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    
    public void SetInvisible(bool isInvisible) // Player invisibility is used for the invisibility powerup and scrap piles.
    {
        this.isInvisible = isInvisible; // Update the invisibility status
        
        // Set the player color.a (alpha) to 0.5 if isInvisible is true and 1 if not.
        Color color = spriteRenderer.color;
        color.a = isInvisible ? 0.5f : 1f; 
        spriteRenderer.color = color;
    }

    // Used for enemy detection
    public bool GetInvisibilityStatus()
    {
        return isInvisible;
    }

}