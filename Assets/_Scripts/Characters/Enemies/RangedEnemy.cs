using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RangedEnemy : Enemy
{
    // Start is called before the first frame update
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public float bulletSpawnOffset = 1f; 
    void Awake()
    {
        enemySpeed = 3f;
        detectionRadius = 10f;
        stopDistance = 4.9f;
        attackRadius = 5f;
        attackCooldown = 1f;

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

    public override void Attack()
    {
        if (Time.time - lastAttackTime >= attackCooldown) // Check if the attack is within cooldown period
        {
            if (distanceToPlayer < attackRadius)
            {
                lastAttackTime = Time.time;
                // Calculate bullet spawn position with offset
                Vector3 bulletSpawnPosition = transform.position + (Vector3.right * movementDirection * bulletSpawnOffset);

                // Instantiate the bullet
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition, Quaternion.identity);

                //Bullet rotation
                Vector3 rotation = playerTransform.position - bulletSpawnPosition;
                float rotZ = Mathf.Atan2(rotation.y * movementDirection, rotation.x * movementDirection) * Mathf.Rad2Deg; 
                bullet.transform.rotation = Quaternion.Euler(0,0, rotZ);
                
                // Set bullet direction
                Vector2 bulletDirection = (playerTransform.position - bulletSpawnPosition).normalized;
                bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * 10f; // Set bullet speed
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        FindPlayer();
        ApproachPlayer();
        Attack();
    }
}
