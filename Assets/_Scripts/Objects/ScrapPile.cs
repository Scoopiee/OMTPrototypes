using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapPile : MonoBehaviour
{
    public GameObject player;
    public GameObject powerUpPrefab; // Reference to the InvisibilityPowerUp prefab
    public float shootForce = 3f;
    private Player playerScript;
    private bool isPlayerBehind = false;
    public float spawnOffset = 1f; // Offset distance from the player to spawn the power-up

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerScript = player.GetComponent<Player>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerBehind)
        {
            SpawnPowerUp();
            Destroy(gameObject);
        }
    }

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

    private void SpawnPowerUp()
    {
        // Calculate the position to spawn the power-up to the right side of the player
        Vector3 spawnPosition = player.transform.position + new Vector3(spawnOffset, 0, 0);

        GameObject powerUp = Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D powerUpRb = powerUp.GetComponent<Rigidbody2D>();
        powerUpRb.AddForce(transform.up * shootForce, ForceMode2D.Impulse);
    }
}
