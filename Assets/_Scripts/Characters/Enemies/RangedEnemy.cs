/**************************************************************************************************************
* <Ranged Enemy> Class
*
* Contains logic for the ranged enemy.
* Extends the enemy class using many of its methods and overriding the attack method
*
* Created by: <Aidan McCarthy> 
* Date: <25/06/2024>
*
***************************************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangedEnemy : Enemy
{
    //Bullet variables and references
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public float bulletSpawnOffset = 1f; 
    
    void Awake()
    {
        // Set common variables TODO: This in a better way if possible
        
        enemySpeed = 3f;
        detectionRadius = 10f;
        stopDistance = 4.9f;
        attackRadius = 5f;
        attackCooldown = 1f;
        
        // Assign player reference
        playerCharacter = GameObject.FindGameObjectWithTag("Player");
        if (playerCharacter != null)
        {
            playerTransform = playerCharacter.transform;
            player = playerCharacter.GetComponent<Player>();
        }
        else
        {
            Debug.LogError("Player GameObject not found with tag 'Player'");
        }
    }

    // Overridden attack method from the enemy class as this enemy needs to shoot.
    public override void Attack()
    {
        if (Time.time - lastAttackTime >= attackCooldown) // Cooldown logic 
        {
            if (distanceToPlayer < attackRadius)
            {
                lastAttackTime = Time.time;
                
                // Calculate bullet spawn position with offset, so the enemy doesnt shoot itself
                Vector3 bulletSpawnPosition = transform.position + (Vector3.right * movementDirection * bulletSpawnOffset);

                // Instantiate the bullet
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition, Quaternion.identity);

                //Bullet rotation
                Vector3 rotation = playerTransform.position - bulletSpawnPosition;
                float rotZ = Mathf.Atan2(rotation.y * movementDirection, rotation.x * movementDirection) * Mathf.Rad2Deg; 
                bullet.transform.rotation = Quaternion.Euler(0,0, rotZ);
                
                // Set bullet direction and velocity
                Vector2 bulletDirection = (playerTransform.position - bulletSpawnPosition).normalized;
                bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * 10f; // Set bullet speed
            }
        }
    }
    
    //Hell yeah only 3 lines!
    void Update()
    {
        FindPlayer();
        ApproachPlayer();
        Attack();
    }
}
