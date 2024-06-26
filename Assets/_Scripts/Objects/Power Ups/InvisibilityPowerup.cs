/**************************************************************************************************************
* <Invisibility Powerup> Class
*
* Contains logic for the invisibility powerup object such as collision logic, powerup logic and duration
* TODO: Make powerup parent class so this all works hopefully, a bit simpler
*
* Created by: <Aidan McCarthy> 
* Date: <16/06/2024>
*
***************************************************************************************************************/
using System.Collections;
using UnityEngine;

public class InvisibilityPowerUp : MonoBehaviour
{
    public float duration = 5f; // Duration of the power-up effect

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                PowerUpManager.Instance.ApplyPowerUp(player, PowerUpRoutine(player));
                Destroy(gameObject); // Destroy power-up object after applying
            }
        }
    }

    // Powerup logic, This is simple since the player has an invisibility method so we use that.
    private IEnumerator PowerUpRoutine(Player player)
    {
        player.SetInvisible(true);
        yield return new WaitForSeconds(duration);
        player.SetInvisible(false);
    }
}
