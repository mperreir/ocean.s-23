using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public int direction = 1;
    public int tankSize = globalFlock.tankSize;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        // movement up and down
        Vector3 pos = transform.position;
        pos.z += direction * 15 * Time.deltaTime;
        transform.position = pos;
        if(transform.position.z>tankSize){
            direction = -1;
        }
        else if(transform.position.z<0){
            direction = 1;
        }
    }
}
