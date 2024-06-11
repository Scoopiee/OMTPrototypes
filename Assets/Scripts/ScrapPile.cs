using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ScrapPile : MonoBehaviour
{
    public GameObject player; 
    public GameObject yellowCoin;
    public float shootForce = 3f;
    private SpriteRenderer playerSpriteRenderer; 
    private Color playerOriginalColor; 
    private Color playerTransparentColor;
    private float transparentAlpha = 0.5f; 
    [SerializeField] private bool isPlayerBehind = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
            playerOriginalColor = playerSpriteRenderer.color;
            playerTransparentColor = new Color(playerOriginalColor.r, playerOriginalColor.g, playerOriginalColor.b, transparentAlpha);
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerBehind) 
        {
            ShootYellowCoin();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerSpriteRenderer.color = playerTransparentColor;
            isPlayerBehind = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) // Checks if the colliding object is the player 
        {
            playerSpriteRenderer.color = playerOriginalColor;
            isPlayerBehind = false; 
        }
    }
    private void ShootYellowCoin (){
        GameObject coin = GameObject.Instantiate(yellowCoin, transform.position, Quaternion.identity);
        Rigidbody2D coinRb = coin.GetComponent<Rigidbody2D>();
        coinRb.AddForce(transform.up * shootForce, ForceMode2D.Impulse);
    }
}
