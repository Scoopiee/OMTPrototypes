using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player; // Reference to the player's transform
    public float speed; // Movement speed of the enemy

    private float distance;

    private Rigidbody2D rb;
    private Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
    }
    void FixedUpdate()
    {
        MoveEnemy();
    }
    void MoveEnemy()
    {
        rb.MovePosition((Vector2)transform.position + (movement * speed * Time.fixedDeltaTime));
    }
}
