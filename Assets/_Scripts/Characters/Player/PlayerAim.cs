using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public Player Player;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        int playerDirection = Player.movementDirection;
        
        
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y * playerDirection, rotation.x * playerDirection) * Mathf.Rad2Deg;   
        transform.rotation = Quaternion.Euler(0,0, rotZ);
    }
    
}
