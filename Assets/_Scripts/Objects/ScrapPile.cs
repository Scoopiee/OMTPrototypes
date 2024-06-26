/**************************************************************************************************************
* <Scrap Pile> Class
*
* Contains logic for scrap piles such as collision, interaction logic, and drop tables
*
* Created by: <Aidan McCarthy> 
* Date: <10/06/2024>
*
***************************************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapPile : MonoBehaviour
{
    // Object and component references
    public GameObject player;
    public GameObject[] powerUpPrefabs; // Reference to the InvisibilityPowerUp prefab
    private Player playerScript;
    // Powerup spawning variables 
    public float shootForce = 3f;
    public float spawnOffset = 1f; // Offset distance from the player to spawn the power-up
    // Other variables
    private bool isPlayerBehind = false;

    void Start()
    {
        // Assign player references
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerScript = player.GetComponent<Player>();
        }
    }

    void Update()
    {
        // Handle interaction logic 
        if (Input.GetKeyDown(KeyCode.E) && isPlayerBehind)
        {
            SpawnPowerUp();
            Destroy(gameObject);
        }
    }

    // Scrap pile hide logic 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerScript.SetInvisible(true); // Make player invisible to enemies
            isPlayerBehind = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerScript.SetInvisible(false); // Make player visible to enemies again
            isPlayerBehind = false;
        }
    }

    // Logic for spawning a powerup. TODO: Add loot table
    private void SpawnPowerUp()
    {
        // Calculate the position to spawn the power-up to the right side of the player
        Vector3 spawnPosition = transform.position + new Vector3(0, spawnOffset, 0);

        // Select a random power-up from the array
        int randomIndex = Random.Range(0, powerUpPrefabs.Length);
        GameObject selectedPowerUpPrefab = powerUpPrefabs[randomIndex];

        GameObject powerUp = Instantiate(selectedPowerUpPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D powerUpRb = powerUp.GetComponent<Rigidbody2D>();
        powerUpRb.AddForce(transform.up * shootForce, ForceMode2D.Impulse);
    }
}
