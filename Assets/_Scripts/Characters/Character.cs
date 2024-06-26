using UnityEngine;

public class Character : MonoBehaviour
{
    public int health;
    public int movementDirection;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        // Common death behavior
        Debug.Log(gameObject.name + " died.");
        Destroy(gameObject);
    }

    public virtual void Flip()
    {
        Vector3 scale = transform.localScale;
        if (movementDirection < 0)
        {
            // Ensure the sprite is facing left
            scale.x = -Mathf.Abs(scale.x);
        }
        else if (movementDirection > 0)
        {
            // Ensure the sprite is facing right
            scale.x = Mathf.Abs(scale.x);
        }
        // Apply the new scale to the character
        transform.localScale = scale;
    }
}