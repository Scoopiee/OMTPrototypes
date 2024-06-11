using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer; 
    void Start()// Start is called before the first frame update
    {
      
    }

    // Update is called once per frame
    void Update()
    {
       horizontal = Input.GetAxis("Horizontal"); //Returns value of -1, 0, +1, depending on direction moving.
       
       if (Input.GetButtonDown("Jump") && IsGrounded())
       {
        rb.velocity= new Vector2(rb.velocity.x, jumpingPower);
       }
       
       if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
       {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
       }
       
       Flip();
    }

   private void FixedUpdate() // Fixed interval updates for physics calculations
   {
    rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
   }
   private bool IsGrounded()
   {
    return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
   }
   private void Flip() // If player character is facing the wrong way, flip the sprite
    {
      if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) 
      {
            isFacingRight = !isFacingRight;
            
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f; // Multiplies the x value of the scale component, flipping the character
            transform.localScale = localScale;
      } 
    }
}
