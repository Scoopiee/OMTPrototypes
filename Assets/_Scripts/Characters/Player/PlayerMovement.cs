/**************************************************************************************************************
* <Player Movement> Class
*
* Handles input and contains logic for moving the player character and certain movement abilities
* such as the dash
*
* Created by: <Aidan McCarthy> 
* Date: <10/06/24>
*
***************************************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Player movement variables
    private float horizontal;
    private float speed;
    private float jumpingPower;
    private float checkRadius = 0.3f;

    //Dashing variables
     private bool canDash = true;
     private bool isDashing;
     private float dashingPower = 24f;
     private float dashingTime = 0.2f;
     private float dashingCooldown = 1f;

     //Components
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask objectsLayer;
    [SerializeField] private Player player;

    void Update()
    {
       speed = player.speed;
       jumpingPower = player.jumpHeight;
       if (isDashing) // Dont run if dashing
       {
        return;
       }
       
       SetDirection();
      
       GetInput();
       
    }

    private void FixedUpdate()
    {
        if (isDashing) // Dont run if dashing
        {
            return;
        }
        
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y); // Left Right Movement
    }
    
    // Checking if the player character is grounded
    private bool IsGrounded()
    {
        LayerMask combinedLayerMask = groundLayer | objectsLayer; // Combines the ground and objects layers to a single layermask to be checked.
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, combinedLayerMask);
    }
    // Handles input and contains jump logic     
    void GetInput()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
       {
            rb.velocity= new Vector2(rb.velocity.x, jumpingPower);
       }
       
       if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
       {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
       }
       
       if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
       {
            StartCoroutine(Dash());
       }
    }
    // Sets the player movement field depending on the horizontal value and flips character sprite accordingly
    void SetDirection() 
    {
        horizontal = Input.GetAxis("Horizontal"); //Returns value of -1,- +1, depending on direction moving.
        
        if (horizontal > 0)
        {
            player.movementDirection = 1;
        }
        else if (horizontal < 0)
        {
            player.movementDirection = -1;
        }
        player.Flip();
    }
    // Dash logic
    private IEnumerator Dash()
    {
        // Verifies dash
        canDash = false;
        isDashing = true;
        
        // Briefly remove gravity from player
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        
        // The dash!
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        
        // Return back to normal
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    
}