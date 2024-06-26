/**************************************************************************************************************
* <Power Up Manager> Class
*
* Just contains a method to apply powerups, this was used to fix an issue I had with powerups never ending
* as the original object would be destroyed on contact, messing the coroutine.
*
* Created by: <Aidan McCarthy> 
* Date: <16/06/2024>
*
***************************************************************************************************************/
using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance { get; private set; }

    // Make sure to keep this on at all times
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Takes a player and a powerup and starts the respective coroutine
    public void ApplyPowerUp(Player player, IEnumerator powerUpRoutine)
    {
        StartCoroutine(powerUpRoutine);
    }
}
