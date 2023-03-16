using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

interface ISelectorState
{
    public void KeyRightPressed() { }
    public void KeyLeftPressed() { }
    public void KeyUpPressed() { }
    public void KeyDownPressed() { }
    public void KeyEnterPressed() { }

    public int GetStateID();
}

class FiletHoverState : ISelectorState
{
    readonly public int StateID = 0;

    public int GetStateID()
    {
        return StateID;
    }

    public void KeyDownPressed() {
        SelectorController.SetState(new BulleurHoverState());
    }

    public void KeyEnterPressed()
    {
        SelectorController.SelectAccessory(StateID);
    }
}

class BulleurHoverState : ISelectorState
{
    readonly public int StateID = 1;
    public int GetStateID()
    {
        return StateID;
    }
    public void KeyUpPressed() {
        SelectorController.SetState(new FiletHoverState());
    }
    public void KeyDownPressed() {
        SelectorController.SetState(new ReservoirHoverState());
    }

    public void KeyEnterPressed()
    {
        SelectorController.SelectAccessory(StateID);
    }
}

class ReservoirHoverState : ISelectorState
{
    readonly public int StateID = 2;
    public int GetStateID()
    {
        return StateID;
    }

    public void KeyUpPressed()
    {
        SelectorController.SetState(new BulleurHoverState());
    }
    public void KeyDownPressed()
    {
        SelectorController.SetState(new ReacteurHoverState());
    }

    public void KeyEnterPressed()
    {
        SelectorController.SelectAccessory(StateID);
    }
}

class ReacteurHoverState : ISelectorState
{
    readonly public int StateID = 3;
    public int GetStateID()
    {
        return StateID;
    }

    public void KeyUpPressed()
    {
        SelectorController.SetState(new ReservoirHoverState());
    }

    public void KeyDownPressed()
    {
        SelectorController.SetState(new StartHoverState());
    }
    public void KeyEnterPressed()
    {
        SelectorController.SelectAccessory(StateID);
    }
}

class RetourHoverState : ISelectorState
{
    readonly public int StateID = 4;
    public int GetStateID()
    {
        return StateID;
    }

    public void KeyUpPressed()
    {
        SelectorController.SetState(new ReacteurHoverState());
    }

    public void KeyRightPressed()
    {
        SelectorController.SetState(new StartHoverState());
    }

    public void KeyEnterPressed()
    {
        SceneManager.LoadScene("Scenes/IntroScene/IntroScene1");
    }
}

class StartHoverState : ISelectorState
{
    readonly public int StateID = 5;
    public int GetStateID()
    {
        return StateID;
    }

    public void KeyUpPressed()
    {
        SelectorController.SetState(new ReacteurHoverState());
    }

    public void KeyLeftPressed()
    {
        SelectorController.SetState(new RetourHoverState());
    }

    public void KeyEnterPressed()
    {
        SceneManager.LoadScene("Main scene");
    }
}

public class SelectorController : MonoBehaviour
{
    static SelectorController Instance;

    ISelectorState State = new FiletHoverState();

    List<int> SelectedAccessories = new();

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            State.KeyUpPressed();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            State.KeyDownPressed();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            State.KeyRightPressed();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            State.KeyLeftPressed();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            State.KeyEnterPressed();
        }
    }

    internal static void SetState(ISelectorState state)
    {
        if (Instance == null) return;
        Instance.State = state;
    }

    public static int GetStateID()
    {
        if (Instance == null) return 0;

        return Instance.State.GetStateID();
    }

    internal static void SelectAccessory(int id)
    {
        if (Instance == null) return;

        if (Instance.SelectedAccessories.Contains(id))
        {
            Instance.SelectedAccessories.Remove(id);
            return;
        }

        if (Instance.SelectedAccessories.Count >= 2) return;
        
        Instance.SelectedAccessories.Add(id);
    }

    public static bool ContainsAccessory(int id) {
        if (Instance == null) return false;

        return Instance.SelectedAccessories.Contains(id);
    }
}
