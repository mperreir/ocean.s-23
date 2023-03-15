using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalFlock : MonoBehaviour
{
    public GameObject fishPrefab;
    public GameObject goalPrefab;
    public static int tankSize = 50;

    static int numFish = 1;
    public static GameObject[] allfish = new GameObject[numFish];

    public static Vector3 goalPos;
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i<numFish; i++){
            Vector3 pos = new Vector3(-10,0,Random.Range(tankSize/2,tankSize/2+5));
            allfish[i] = (GameObject) Instantiate(fishPrefab, pos, Quaternion.identity);
        }

        goalPrefab.transform.position = new Vector3(-100,0,10);

    }

    // Update is called once per frame
    void Update()
    {
        if(Random.Range(1,20000)<20){
            goalPrefab.transform.Rotate(0.0f,1.0f,0.0f);
        }
        if(Random.Range(1,20000)<20){
            goalPrefab.transform.Rotate(0.0f,1.0f,0.0f);
        }
        goalPrefab.transform.Translate(10.0f * Time.deltaTime,0,0);
        goalPos = goalPrefab.transform.position;
    }
}
