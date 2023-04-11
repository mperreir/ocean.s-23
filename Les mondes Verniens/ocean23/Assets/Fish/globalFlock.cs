using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalFlock : MonoBehaviour
{
    public GameObject fishPrefab;
    public static int tankSize; // box size
    public static int numFish = 150; // number of fish
    public static GameObject[] allfish = new GameObject[numFish];


    void Start()
    {
        // generation of the flock by limiting the range
        tankSize = 200;
        for(int i = 0; i<numFish; i++){
            Vector3 pos = new Vector3(Random.Range(-500,-400), Random.Range(0, tankSize*2), 320);
            allfish[i] = (GameObject) Instantiate(fishPrefab, pos, Quaternion.identity);
        }

    }

    void Update()
    {
    }
}
