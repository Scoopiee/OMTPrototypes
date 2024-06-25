using UnityEngine;
using System.Collections;

public abstract class PowerUp : MonoBehaviour
{
    public float duration = 5f; // Duration of the power-up effect

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D called");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player collided with power-up");
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("Player component found");
                ApplyPowerUp(player);
                Destroy(gameObject); // Destroy power-up object after applying
            }
        }
    }

    protected abstract void ApplyPowerUp(Player player);

    protected IEnumerator PowerUpRoutine(Player player)
    {
        Debug.Log("PowerUpRoutine started");
        ActivatePowerUp(player);
        yield return new WaitForSeconds(duration);
        Debug.Log("WaitForSeconds completed");
        DeactivatePowerUp(player);
        Debug.Log("PowerUpRoutine ended");
    }

    protected abstract void ActivatePowerUp(Player player);

    protected abstract void DeactivatePowerUp(Player player);
}