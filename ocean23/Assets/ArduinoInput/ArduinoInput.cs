using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO.Ports;

[System.Serializable]
class ArduinoInputState {
    public int StartEndButton = 0;
    public int BubbleStream = 0;

    public int JoystickX = 0;
    public int JoystickY = 0;

    // Consider as queue 
    public int LaunchHunter = 0;


    public void UpdateValues(ArduinoInputState input) {
        this.StartEndButton = input.StartEndButton;
        this.BubbleStream = input.BubbleStream;
        this.JoystickX = input.JoystickX;
        this.JoystickY = input.JoystickY;
        this.LaunchHunter += input.LaunchHunter;
    }

    public void DebugLaunchHunter() {
        Debug.Log(LaunchHunter);
    }
}

public enum ArduinoInputKey {
    StartEndButton,
    BubbleStream, 
    JoystickXRight,
    JoystickXLeft,
    JoystickYUp,
    JoystickYDown,
    LaunchHunter
}

public class ArduinoInput : MonoBehaviour
{
    static ArduinoInput Instance;

    ArduinoInputState Input = new ArduinoInputState();

    // The port need to be changed
    SerialPort stream = new SerialPort("/dev/cu.usbmodem143101", 9600);

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        stream.Open();
        Debug.Log("Start");
        // Application.runInBackground = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stream.IsOpen) return;
        if (stream.BytesToRead <=0 ) return;
        String value = stream.ReadLine();
        Debug.Log(value);
        try {
            ArduinoInputState input = JsonUtility.FromJson<ArduinoInputState>(value);
            Input.UpdateValues(input);
            Input.DebugLaunchHunter();
        }
        catch (Exception e) {
            Debug.Log(e);
        }
    }

    void Test() {
        // Debug.Log("test fine");
    }

    bool _GetKey(ArduinoInputKey key) {
        switch (key) {
            case ArduinoInputKey.StartEndButton: return Input.StartEndButton != 0;
            case ArduinoInputKey.BubbleStream: return Input.BubbleStream != 0;
            case ArduinoInputKey.JoystickXRight: return Input.JoystickX < 0;
            case ArduinoInputKey.JoystickXLeft: return Input.JoystickX > 0;
            case ArduinoInputKey.JoystickYUp: return Input.JoystickY > 0;
            case ArduinoInputKey.JoystickYDown: return Input.JoystickY < 0;
            case ArduinoInputKey.LaunchHunter: {
                if (Input.LaunchHunter <= 0) return false;
                Input.LaunchHunter--;
                return true;
            };
            default: return false;
        }
    }

    public static bool GetKey(ArduinoInputKey key) {
        return Instance._GetKey(key);
    }
}
