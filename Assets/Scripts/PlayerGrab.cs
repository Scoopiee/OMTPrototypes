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
   private PlayerMovement playerMovement;
    
    void Start() // Start is called before the first frame update
    {
        layerIndex = LayerMask.NameToLayer("Objects");
        playerMovement = GetComponent<PlayerMovement>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        
    }

    // Update is called once per frame
    void Update()
    {
         mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        
        int playerDirection = playerMovement.GetDirection();
        
        RaycastHit2D hitInfo = Physics2D.Raycast(rayPoint.position, Vector2.right * playerDirection, rayDistance);

        if (hitInfo.collider != null && hitInfo.collider.gameObject.layer == layerIndex)
        {
            if (Input.GetKeyDown(KeyCode.E) && grabbedObject == null)
            {
                grabbedObject = hitInfo.collider.gameObject;
                grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;
                grabbedObject.transform.position = grabPoint.position;
                grabbedObject.transform.SetParent(transform);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
            grabbedObject.transform.SetParent(null);
            grabbedObject = null;
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            Rigidbody2D rb = grabbedObject.GetComponent<Rigidbody2D>();
            grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
            grabbedObject.transform.SetParent(null);
 
            // Apply force to throw the object
            Vector2 throwDirection = new Vector2(mousePos.x - grabPoint.transform.position.x, mousePos.y - grabPoint.transform.position.y).normalized; // Adjust as needed for desired throw angle
            float throwForce = 20f; // Adjust the force value as needed
            rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);

            grabbedObject = null;
        }
    }   
}
