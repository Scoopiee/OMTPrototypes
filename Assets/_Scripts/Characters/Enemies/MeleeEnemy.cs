/**************************************************************************************************************
* <Melee Enemy> Class
*
* Contains logic for basic enemy behaviours, extending the enemy class.
*
* Was just a test enemy for while I implemented new class structure but works great as a basic melee enemy
*
* Created by: <Aidan McCarthy> 
* Date: <25/06/2024>
*
***************************************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    void Awake()
    {
        // Set common variables TODO: This in a better way if possible
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

    void Update()
    {
        FindPlayer();
        ApproachPlayer();
        Attack();
    }
}


