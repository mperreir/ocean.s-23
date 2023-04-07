using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ArduinoInput.GetKey(ArduinoInputKey.JoystickXLeft)) {
            transform.position += (Vector3.left / 10);
        }
        if (ArduinoInput.GetKey(ArduinoInputKey.JoystickXRight)) {
            transform.position += (Vector3.right / 10);
        }
        if (ArduinoInput.GetKey(ArduinoInputKey.JoystickYDown)) {
            transform.position += (Vector3.down / 10);
        }
        if (ArduinoInput.GetKey(ArduinoInputKey.JoystickYUp)) {
            transform.position += (Vector3.up / 10);
        }
    }
}
