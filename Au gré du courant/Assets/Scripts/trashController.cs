using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trashController : MonoBehaviour, IFlowMovable
{
    // Start is called before the first frame update
    private Rigidbody2D _rb;
    // pr l'afficher dans l'éditeur
    [SerializeField] private float coeffSpeed = 0.8f;
    
    private Vector2 currentFlowDir;
        public Vector2 CurrentFlowDir
        {
            get
            {
                return currentFlowDir;
            }
            set
            {
                currentFlowDir = value;
            }
        }

        private float currentFlowForce;
        public float CurrentFlowForce
        {
            get
            {
                return currentFlowForce;
            }
            set
            {
                currentFlowForce = value;
            }
        }
    void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

    }
    //Make a log of the collision

    public void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject otherObj = collider.gameObject;
    }


    public void ApplyFlow() {
        _rb.velocity += this.CurrentFlowDir * this.CurrentFlowForce * coeffSpeed;
        }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        this.ApplyFlow();
    }
}
