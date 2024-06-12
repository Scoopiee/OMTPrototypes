using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public Text healthText;
    // Start is called before the first frame update
    void Start()
    {
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        
      

    }

    public void UpdateHealthUI(int health)
    {
       if (healthText != null)
        {
            Debug.Log($"Updating Health UI to: {health}");
            healthText.text = $"Health: {health}";
        }
    }
    
}
