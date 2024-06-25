using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   [SerializeField] private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private float checkRadius = 0.3f;
    private bool isFacingRight = true;
    private int  playerDirection = 1;

    [SerializeField] private bool canDash = true;
    [SerializeField] private bool isDashing;
    [SerializeField] private float dashingPower = 24f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;

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

       horizontal = Input.GetAxis("Horizontal"); //Returns value of -1,- +1, depending on direction moving.
       
       if (horizontal > 0)
       {
        player.movementDirection = 1;
       }
       else if (horizontal < 0)
       {
        player.movementDirection = -1;
       }

        Debug.Log($"Moving: {player.movementDirection}");

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
        
       player.Flip();
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
    
    private void Flip() // If player character is facing the wrong way, flip the sprite
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) 
        {
            isFacingRight = !isFacingRight; //Flip the boolean 
            playerDirection *= -1; //Flip direction (numerical representation)
                
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f; // Multiplies the x value of the scale component, flipping the character
            transform.localScale = localScale;
        } 
    }

    public int GetDirection()
    {
        return playerDirection;
    }

    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        }
    }
    
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravitty = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravitty;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;

    }
    
}