/**************************************************************************************************************
* <Speed Powerup> Class
*
* Contains logic for the speed powerup object such as collision logic, powerup logic and duration
* TODO: Make powerup parent class so this all works hopefully, a bit simpler
*
* Created by: <Aidan McCarthy> 
* Date: <26/06/2024>
*
***************************************************************************************************************/
using System.Collections;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    public float duration = 5f; // Duration of the power-up effect
    private float originalSpeed;
    private float newSpeed;
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
        player.ActivePowerup = "Active Powerup: Speed boost!";
        originalSpeed = player.speed;
        newSpeed = originalSpeed * 2.0f;
        player.speed = newSpeed;
        yield return new WaitForSeconds(duration);
        player.speed = originalSpeed;
        player.ActivePowerup = "";
    }
}
