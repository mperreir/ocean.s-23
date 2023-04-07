using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.Playables;


public class ControlScene : MonoBehaviour
{
    // Start is called before the first frame update
    //Change scene on arduino value
    public ReadTwoArduinoValuesExample myArduino;
    public bool locked = false;
    public string sceneToLoad;

    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        
        if(myArduino.values[0]!=0&&myArduino.values[1]!=0)
        {
            //if not in dead zone
            if ((myArduino.values[0] <= 490 && myArduino.values[0] >= 550) && (myArduino.values[1] <= 490 && myArduino.values[1] >= 550))
            {
                Debug.Log("Change scene");
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad, UnityEngine.SceneManagement.LoadSceneMode.Additive);
            }
        }else if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Change scene");
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad, UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
        
        
    }
}
