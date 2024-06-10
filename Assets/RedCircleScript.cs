using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCircleScript : MonoBehaviour
{
    public GameObject Player; // Reference to the player GameObject
    public float followSpeed = 2f; 
    public Vector3 offset = new Vector3(0.25f, 0f, 0f); // Example offset, adjust as needed
    public bool pickedUp = false;// Speed at which the red circle follows the player
    // Start is called before the first frame update
    public float throwForce = 10f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pickedUp == true) 
        {
        transform.position = Player.transform.position + offset;
        if (Input.GetKeyDown(KeyCode.F))
            {
                // Throw the red circle to the right
                ThrowRedCircle();
            }
        }
         
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
          pickedUp = true;
            
        }
    }
    private void ThrowRedCircle()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.right * throwForce;
        pickedUp = false; // Reset the pickedUp flag after throwing
    }
}
