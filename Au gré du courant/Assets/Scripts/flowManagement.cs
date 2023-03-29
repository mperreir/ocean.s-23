using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flowManagement : MonoBehaviour
{
    [SerializeField] private Vector2 direction = Vector2.right;
    [SerializeField] private float force = 2f;

    void Start()
    {
        
    }
    

    void OnTriggerEnter2D(Collider2D collider)
    {
        
        GameObject otherObj = collider.gameObject;

        IFlowMovable movable = otherObj.GetComponent<IFlowMovable>();

        if (movable != null) {
            // Set flow values
            movable.CurrentFlowDir = direction;
            movable.CurrentFlowForce = force;

        }

    }

    void OnTriggerExit2D(Collider2D collider)
    {   
        
        GameObject otherObj = collider.gameObject;
        
        IFlowMovable movable = otherObj.GetComponent<IFlowMovable>();

        if (movable != null) {
            // Set flow values
            movable.CurrentFlowDir = Vector2.zero;
            movable.CurrentFlowForce = 0f;
        }
 

    }
    // Update is called once per frame
    void Update()
    {
    }

}
