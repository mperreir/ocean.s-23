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

                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        String value = stream.ReadLine();
                        Debug.Log(value);
                        ArduinoInputState input = JsonUtility.FromJson<ArduinoInputState>(value);
                        Input.UpdateValues(input);
                        Debug.Log($"{portName} is arduino.");
                        ArduinoFound = true;
                        break;
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                if (ArduinoFound) break;
            }
            catch (Exception)
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
    public void Update()
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

    float StartEndButtonLastRead = 0;
    float RightLastRead = 0;
    float LeftLastRead = 0;
    float UpLastRead = 0;
    float DownLastRead = 0;

    bool _GetKey(ArduinoInputKey key)
    {

        switch (key)
        {
            case ArduinoInputKey.StartEndButton:
                if (Input.StartEndButton != 0)
                {
                    if (StartEndButtonLastRead == 0)
                    {
                        StartEndButtonLastRead += Time.deltaTime;
                        return true;
                    }
                    if (StartEndButtonLastRead >= 1.0)
                    {
                        StartEndButtonLastRead = 0 + Time.deltaTime;
                        return true;
                    }
                    StartEndButtonLastRead += Time.deltaTime;
                }
                else
                {
                    StartEndButtonLastRead = 0;
                }
                return false;
            case ArduinoInputKey.BubbleStream: return Input.BubbleStream != 0;
            case ArduinoInputKey.JoystickXRight:
                if (Input.JoystickX > 0)
                {
                    if (RightLastRead == 0)
                    {
                        RightLastRead += Time.deltaTime;
                        return true;
                    }
                    if (RightLastRead >= 1.0)
                    {
                        RightLastRead = 0 + Time.deltaTime;
                        return true;
                    }
                    RightLastRead += Time.deltaTime;
                }
                else
                {
                    RightLastRead = 0;
                }
                return false;
            case ArduinoInputKey.JoystickXLeft:
                if (Input.JoystickX < 0)
                {
                    if (LeftLastRead == 0)
                    {
                        LeftLastRead += Time.deltaTime;
                        return true;
                    }
                    if (LeftLastRead >= 1.0)
                    {
                        LeftLastRead = 0 + Time.deltaTime;
                        return true;
                    }
                    LeftLastRead += Time.deltaTime;
                }
                else
                {
                    LeftLastRead = 0;
                }
                return false;
            case ArduinoInputKey.JoystickYUp:
                if (Input.JoystickY > 0)
                {
                    if (UpLastRead == 0)
                    {
                        UpLastRead += Time.deltaTime;
                        return true;
                    }
                    if (UpLastRead >= 1.0)
                    {
                        UpLastRead = 0 + Time.deltaTime;
                        return true;
                    }
                    UpLastRead += Time.deltaTime;
                }
                else
                {
                    UpLastRead = 0;
                }
                return false;
            case ArduinoInputKey.JoystickYDown:
                if (Input.JoystickY < 0)
                {
                    if (DownLastRead == 0)
                    {
                        DownLastRead += Time.deltaTime;
                        return true;
                    }
                    if (DownLastRead >= 1.0)
                    {
                        DownLastRead = 0 + Time.deltaTime;
                        return true;
                    }
                    DownLastRead += Time.deltaTime;
                }
                else
                {
                    DownLastRead = 0;
                }
                return false;
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
