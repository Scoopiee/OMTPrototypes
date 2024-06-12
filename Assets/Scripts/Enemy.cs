using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player; // Reference to the player's transform
    public float speed; // Movement speed of the enemy
    public int damage; 

    
    public  float followDistance = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    
    void Start() // Start is called before the first frame update
    {
       player = GameObject.FindGameObjectWithTag("Player");
       rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = new Vector2(player.transform.position.x - transform.position.x, 0);

        float distance = Vector2.Distance(transform.position, player.transform.position); 
        

        if (distance <= followDistance)
        {
            if (direction.x > 0)
            {
                movement = Vector2.right;
            }
            else 
            {
                movement = Vector2.left;
            }
        }
        else
        {
            movement = Vector2.zero;
;        }  
    }
    void FixedUpdate()
    {
        MoveEnemy();
    }
    private void MoveEnemy()
    {
        rb.MovePosition((Vector2)transform.position + (movement * speed * Time.deltaTime));
    }
}
