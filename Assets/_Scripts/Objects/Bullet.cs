/**************************************************************************************************************
* <Bullet> Class
*
* Contains logic for bullet object, such as damage, collision logic, and  
*
* Created by: <Full Name> 
* Date: <dd/mm/yy>
*
***************************************************************************************************************/
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 20;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object is the player
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>(); // Assign player reference
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
        Destroy(gameObject); // Destroy the bullet on collision with any object
    }
}