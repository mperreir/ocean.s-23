using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Threading;
using System.IO.Ports;

[System.Serializable]
class ArduinoInputState
{
    public int StartEndButton = 0;
    public int BubbleStream = 0;

    public int JoystickX = 0;
    public int JoystickY = 0;

    // Consider as queue 
    public int LaunchHunter = 0;

    public void UpdateValues(ArduinoInputState input)
    {
        this.StartEndButton = input.StartEndButton;
        this.BubbleStream = input.BubbleStream;
        this.JoystickX = input.JoystickX;
        this.JoystickY = input.JoystickY;
        this.LaunchHunter += input.LaunchHunter;
    }

    public void DebugLaunchHunter()
    {
        Debug.Log(LaunchHunter);
    }
}

public enum ArduinoInputKey
{
    StartEndButton,
    BubbleStream,
    JoystickXRight,
    JoystickXLeft,
    JoystickYUp,
    JoystickYDown,
    LaunchHunter
}

public class ArduinoInput
{
    static ArduinoInput Instance;

    ArduinoInputState Input = new ArduinoInputState();

    // The port need to be changed
    SerialPort stream = new SerialPort();

    const int SERIAL_BAUD_RATE = 115200;

    public bool ArduinoFound = false;

    private ArduinoInput()
    {
        Start();
    }

    public static ArduinoInput GetInstance()
    {
        if (Instance == null) Instance = new ArduinoInput();

        return Instance;
    }

    void InitArduino()
    {
        foreach (var portName in SerialPort.GetPortNames())
        {
            try
            {
                stream = new SerialPort(portName, SERIAL_BAUD_RATE);
                stream.ReadTimeout = 500;
                stream.Open();
                Debug.Log($"Found : {portName}");

                if (stream.BytesToRead <= 0) continue;
                String value = stream.ReadLine();

                ArduinoInputState input = JsonUtility.FromJson<ArduinoInputState>(value);
                Input.UpdateValues(input);
                Debug.Log($"{portName} is arduino.");
                ArduinoFound = true;
                break;
            }
            catch (Exception e)
            {

            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null) return;
        Instance = this;
        Debug.Log("Start");
        InitArduino();
    }

    // Update is called once per frame
    void Update()
    {
        if (this != Instance)
        {
            Instance.Update();
            return;
        }

        if (!stream.IsOpen) return;
        if (stream.BytesToRead <= 0) return;
        String value = stream.ReadLine();
        try
        {
            ArduinoInputState input = JsonUtility.FromJson<ArduinoInputState>(value);
            Input.UpdateValues(input);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    void Test()
    {
        // Debug.Log("test fine");
    }

    bool _GetKey(ArduinoInputKey key)
    {
        switch (key)
        {
            case ArduinoInputKey.StartEndButton: return Input.StartEndButton != 0;
            case ArduinoInputKey.BubbleStream: return Input.BubbleStream != 0;
            case ArduinoInputKey.JoystickXRight: return Input.JoystickX < 0;
            case ArduinoInputKey.JoystickXLeft: return Input.JoystickX > 0;
            case ArduinoInputKey.JoystickYUp: return Input.JoystickY > 0;
            case ArduinoInputKey.JoystickYDown: return Input.JoystickY < 0;
            case ArduinoInputKey.LaunchHunter:
                {
                    if (Input.LaunchHunter <= 0) return false;
                    Input.LaunchHunter--;
                    return true;
                };
            default: return false;
        }
    }

    public static bool GetKey(ArduinoInputKey key)
    {
        return Instance._GetKey(key);
    }

    public static int GetBubbleStreamValue()
    {
        return Instance.Input.BubbleStream;
    }
}
