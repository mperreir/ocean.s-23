using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalFlock : MonoBehaviour
{
    public GameObject fishPrefab;
    public GameObject goalPrefab;
    public static int tankSize = 100;

    public static int numFish = 100;
    public static GameObject[] allfish = new GameObject[numFish];

    public static Vector3 goalPos;
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i<numFish; i++){
            Vector3 pos = new Vector3(Random.Range(-500,-400), Random.Range(tankSize / 2 - 25, tankSize / 2 + 25), Random.Range(280, 360));
            allfish[i] = (GameObject) Instantiate(fishPrefab, pos, Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
    }
}
