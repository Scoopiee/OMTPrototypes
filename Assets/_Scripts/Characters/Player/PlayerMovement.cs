using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Player movement variables
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
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

    // Update is called once per frame
    void Update()
    {
       if (isDashing)
       {
        return;
       }
       
       SetDirection();
      
       GetInput();
       
    }

    private void FixedUpdate() // Fixed interval updates for physics calculations
    {
        if (isDashing)
        {
            return;
        }
        
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y); //Left Right Movement
    }
    
    private bool IsGrounded()
    {
        LayerMask combinedLayerMask = groundLayer | objectsLayer; // Combines the ground and objects layers to a single layermask to be checked.
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, combinedLayerMask);
    }
    
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
    void SetDirection() //Sets the player.movement variable depending on where
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
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    
}