using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalFlock : MonoBehaviour
{
    public GameObject fishPrefab;
    public static int tankSize;

    public static int numFish = 150;
    public static GameObject[] allfish = new GameObject[numFish];

    
    // Start is called before the first frame update
    void Start()
    {
        tankSize = 200;
        for(int i = 0; i<numFish; i++){
            Vector3 pos = new Vector3(Random.Range(-500,-400), Random.Range(0, tankSize*2), 320);
            allfish[i] = (GameObject) Instantiate(fishPrefab, pos, Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
    }
}
