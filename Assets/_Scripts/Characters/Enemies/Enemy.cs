using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public float enemySpeed; 
    public float detectionRadius; // Radius within which the enemy detects the player
    public float stopDistance;
    public float attackRadius;
    public float attackCooldown;
    public float lastAttackTime = -Mathf.Infinity; // Time when the last attack occurred
    public float distanceToPlayer;
    public Vector2 directionToPlayer;
    public GameObject playerCharacter;
    public Player player;
    public Transform playerTransform;
    
    public virtual void FindPlayer()
    {
         if (playerTransform == null || player.GetInvisibilityStatus())
    {
        return; // Do not approach or target the player if they are invisible or don't exist
    }
    else
    {
        Vector3 enemyPosition = transform.position;
        Vector3 playerPosition = playerTransform.position;

        distanceToPlayer = Vector2.Distance(enemyPosition, playerPosition);
        directionToPlayer = (playerPosition - enemyPosition).normalized;
    }
        
    }
    public virtual void ApproachPlayer()
    {
        if (distanceToPlayer < detectionRadius && distanceToPlayer > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, enemySpeed * Time.deltaTime);
        }
        
        if (directionToPlayer.x > 0)
        {
            movementDirection = 1;
        }
        else if (directionToPlayer.x < 0)
        {
            movementDirection = -1;
        }
        Flip();
    }

     public virtual void Attack()
    {
        
        if (Time.time - lastAttackTime >= attackCooldown) // Check if the attack is within cooldown period
        {
            if (distanceToPlayer < attackRadius)
            {
                player.TakeDamage(1);
                lastAttackTime = Time.time; // Reset the cooldown timer
            }
        }
    }
    
    public virtual void Idle()
    {

    }   
}
