using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private SpriteRenderer spriteRenderer;
    private bool isInvisible = false;
   
    void Awake()
    {
        health = 100;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
   
    public void SetInvisible(bool isInvisible)
    {
        Debug.Log($"Setting invisibility to {isInvisible}");
        this.isInvisible = isInvisible; // Update the invisibility status
        Color color = spriteRenderer.color;
        color.a = isInvisible ? 0.5f : 1f; // Make the player semi-transparent if invisible, fully opaque if not
        spriteRenderer.color = color;

        Debug.Log($"Player color set to {color.a}");
    }

    public bool GetInvisibilityStatus()
    {
        return isInvisible;
    }

}