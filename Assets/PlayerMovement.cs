using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player movement
    private Rigidbody2D rb;
    private Vector2 movement;
    public GameObject scrapPile;
    public GameObject yellowCoinPrefab; // Reference to the yellow coin prefab
   
    public float shootForce = 3f; // The force with which the coin will be shot
    private bool isBehindScrapPile = false;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public float transparentAlpha = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
       movement.x = Input.GetAxis("Horizontal");
       movement.y = Input.GetAxis("Vertical"); 

       if (Input.GetKeyDown(KeyCode.E) && isBehindScrapPile)
       {
            ShootYellowCoin();
       }
    }
     void FixedUpdate()
    {
        // Move the player
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ScrapPile"))
        {
            //They are behind
            isBehindScrapPile = true;
            
            // Set player to semi-transparent
            Color color = spriteRenderer.color;
            color.a = transparentAlpha;
            spriteRenderer.color = color;
            
        }
    }

     private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ScrapPile"))
        {
            isBehindScrapPile = false;
            // Reset player transparency
            spriteRenderer.color = originalColor;
        }
    }

    private void ShootYellowCoin()
    {
        GameObject coin = Instantiate(yellowCoinPrefab, transform.position, Quaternion.identity);
        Rigidbody2D coinRb = coin.GetComponent<Rigidbody2D>();
        coinRb.AddForce(transform.up * shootForce, ForceMode2D.Impulse);
        Destroy(scrapPile);
    }

}
