using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
     // Radius within which the enemy detects the player
    
    // Start is called before the first frame update
    void Awake()
    {
        enemySpeed = 3f;
        detectionRadius = 10f;
        stopDistance = 1.9f;
        attackRadius = 2f;
        attackCooldown = 2f;

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

    // Update is called once per frame
    void Update()
    {
        FindPlayer();
        ApproachPlayer();
        Attack();
    }
}


