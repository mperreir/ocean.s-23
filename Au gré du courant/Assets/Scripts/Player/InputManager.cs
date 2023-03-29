using System;
using UnityEngine;
using UnityEngine.Events;


namespace Player
{
    [RequireComponent(typeof(PlayerController))]
    public class InputManager : MonoBehaviour
    {
        private Camera viewCam;
        public ReadTwoArduinoValuesExample myArduino;
        public bool locked = false;        
        private PlayerController _playerController;
        
        

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
        }
        
        private void Start()
        {
            viewCam = Camera.main;
            
        }
        
        private void Update()
        {
            if (locked)
                return;
            
            float horizontalMovement = Input.GetAxisRaw("Horizontal");
            float verticalMovement = Input.GetAxisRaw("Vertical");
            
            // FR : Lecture de la 1er valeur

           // float horizontalMovement = myArduino.values[0];
            //float verticalMovement = myArduino.values[1];
            // FR : Lecture de la 2eme valeur
            // EN : Read the second value
            _playerController.MovementInput = Vector2.ClampMagnitude(new Vector2(horizontalMovement, verticalMovement), 1);

            
        }



    }
}
