using System.Collections;
using UnityEngine;

public class InvisibilityPowerUp : MonoBehaviour
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
                PowerUpManager.Instance.ApplyPowerUp(player, PowerUpRoutine(player));
                Destroy(gameObject); // Destroy power-up object after applying
            }
        }
    }

    private IEnumerator PowerUpRoutine(Player player)
    {
        Debug.Log("PowerUpRoutine started");
        player.SetInvisible(true);
        yield return new WaitForSeconds(duration);
        Debug.Log("WaitForSeconds completed");
        player.SetInvisible(false);
        Debug.Log("PowerUpRoutine ended");
    }
}
