/**************************************************************************************************************
* <Character> Parent Class
*
* Parent class that defines basic functions and fields for all characters (player and npc). 
* Extends monobehaviour so that all characters still have that functionality
*
* Created by: <Aidan McCarthy> 
* Date: <25/06/24>
*
***************************************************************************************************************/
using UnityEngine;
public class Character : MonoBehaviour
{
    public int health;
    public int movementDirection;

    // Reduces characters health by the passed amount
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    
    // For now just kills the game object. TODO: Create game over/respawn logic
    protected virtual void Die()  
    {
        Debug.Log(gameObject.name + " died.");
        Destroy(gameObject);
    }

    public virtual void Flip() // Flips the sprite based on the movement direction
    {
        Vector3 scale = transform.localScale;
        if (movementDirection < 0)
        {
            scale.x = -Mathf.Abs(scale.x);// Makes sure the sprite is facing left
        }
        else if (movementDirection > 0)
        {
            scale.x = Mathf.Abs(scale.x);// Makes sure the sprite is facing right
        }
        transform.localScale = scale;
    }
}