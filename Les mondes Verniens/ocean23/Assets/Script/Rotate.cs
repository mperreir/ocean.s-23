using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed = 4.0f;

    private GameObject s1;
    private GameObject s2;
    private GameObject s3;
    

    // Start is called before the first frame update
    void Start()
    {
      s1 = GameObject.Find("s1");
      s2 = GameObject.Find("s2");
      s3 = GameObject.Find("s3");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject [] ss= {s1, s2, s3};
        // transform.Rotate(0, speed, speed);
        foreach (GameObject s in ss) 
        {
          Vector3 lp = transform.localPosition;
          transform.localPosition = Vector3.MoveTowards (lp, s.transform.position, speed);          
        }

    }
}