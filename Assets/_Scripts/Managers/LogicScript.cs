using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public Text healthText;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        healthText = GameObject.Find("HealthText").GetComponent<Text>();

        player = FindObjectOfType<Player>();
        if (player != null)
        {
            Debug.Log($"Found player script, {player}");
        }
        else
        {
            Debug.Log("Couldn't find Player script");
        }
    }

    void Update()
    {
       UpdateHealthUI(player.health);
    }
    
    public void UpdateHealthUI(int health)
    {
       if (healthText != null)
        {
            healthText.text = $"Health: {health}";
        }
    }

}
