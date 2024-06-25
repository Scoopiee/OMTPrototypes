using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 20;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Collision detected with {collision.gameObject.name}");

        // Check if the collided object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit!");
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("Player component found");
                // Apply damage to the player
                player.TakeDamage(damage);
                Debug.Log("Player took 20 damage");
            }
            else
            {
                Debug.Log("Player component not found");
            }
        }

        Debug.Log("Bullet destroyed");
        // Destroy the bullet on collision with any object
        Destroy(gameObject);
    }
}