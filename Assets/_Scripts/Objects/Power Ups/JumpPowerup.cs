/**************************************************************************************************************
* <Jump Powerup> Class
*
* Contains logic for the jump powerup object such as collision logic, powerup logic and duration
* TODO: Make powerup parent class so this all works hopefully, a bit simpler
*
* Created by: <Aidan McCarthy> 
* Date: <26/06/2024>
*
***************************************************************************************************************/
using System.Collections;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class JumpPowerUp : MonoBehaviour
{
    public float duration = 5f; // Duration of the power-up effect
    private float originalJumpHeight;
    private float newJumpHeight;
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

    // Powerup logic
    private IEnumerator PowerUpRoutine(Player player)
    {
        player.ActivePowerup = "Active Powerup: Jump Boost!";
        originalJumpHeight = player.jumpHeight;
        newJumpHeight = originalJumpHeight * 2.0f;
        player.jumpHeight = newJumpHeight;
        yield return new WaitForSeconds(duration);
        player.jumpHeight = originalJumpHeight;
        player.ActivePowerup = "";
    }
}
