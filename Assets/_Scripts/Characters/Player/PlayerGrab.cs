/**************************************************************************************************************
* <Player Grab> Class
*
* This class just contains logic for the player grabbing objects
* TODO: This is all a mess that needs reorganising
* Created by: <Aidan McCarthy> 
* Date: <11/06/2024>
*
***************************************************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObjects : MonoBehaviour
{ 
   private Camera mainCam; 
   private Vector3 mousePos;
   [SerializeField] private Transform grabPoint;
   [SerializeField] private Transform rayPoint;
   [SerializeField] private float rayDistance;
   [SerializeField] private GameObject grabbedObject = null;
   private int layerIndex; 
   [SerializeField] private Player player;
    
    void Start() // Start is called before the first frame update
    {
        layerIndex = LayerMask.NameToLayer("Objects");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        
    }

    // Update is called once per frame
    void Update()
    {
         mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        
        int playerDirection = player.movementDirection;
        
        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, Vector2.right * playerDirection, rayDistance); // Ray to scan for objects in front of player


        if (hitInfo.collider != null && hitInfo.collider.gameObject.layer == layerIndex)
        {
            if (Input.GetKeyDown(KeyCode.E) && grabbedObject == null)
            {
                grabbedObject = hitInfo.collider.gameObject; // Checks what has been grabbed
                grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true; 
                grabbedObject.transform.position = grabPoint.position; // Places the object at the grab point of the player
                grabbedObject.transform.SetParent(transform); // Makes the object a child of the player character TODO:
            }
        }
        
        if (Input.GetKeyDown(KeyCode.F)) //Logic to drop object
        {
            grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
            grabbedObject.transform.SetParent(null); //
            
            grabbedObject = null;
        }
        
        if (Input.GetKeyDown(KeyCode.R))//Logic to throw object
        {
            Rigidbody2D rb = grabbedObject.GetComponent<Rigidbody2D>();
            
            grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
            grabbedObject.transform.SetParent(null);

            //Applying force to the object in the direction of the mouse cursor 
            Vector2 throwDirection = new Vector2(mousePos.x - grabPoint.transform.position.x, mousePos.y - grabPoint.transform.position.y).normalized; // Adjust as needed for desired throw angle
            float throwForce = 20f; // Adjust the force value as needed
            rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);

            grabbedObject = null;
        }
    }   
}
