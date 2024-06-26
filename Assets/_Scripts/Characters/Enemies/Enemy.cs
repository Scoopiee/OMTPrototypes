/**************************************************************************************************************
* <Enemy> Parent Class
*
* Parent class that contains basic functionality all enemies should have. 
* Extends the character class so each enemy has that functionality.
* 
* TODO: Stop enemies from flying away if they are moving when outside detection radius
* 
* Created by: <Aidan McCarthy> 
* Date: <25/06/2024>
*
***************************************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    // Movement and pathing variables
    public float enemySpeed; 
    public float detectionRadius; 
    public float stopDistance;
    //Attack variables
    public float attackRadius;
    public float attackCooldown;
    public float lastAttackTime = -Mathf.Infinity; // Why not
    //Player-related variables
    public float distanceToPlayer;
    public Vector2 directionToPlayer;
    public GameObject playerCharacter;
    public Player player;
    public Transform playerTransform;
    
    // Finds the player position, distance and direction on the game, useful for enemy targetting
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
    // Basic logic to move an enemy towards a player position TODO: Simplify this method by taking out movement direction stuff
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
    // Basic attack logic that can be overridden
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
    //TODO: This.
    public virtual void Idle()
    {

    }   
}
