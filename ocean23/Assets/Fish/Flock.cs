using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public float speed = 1.0f;
    public float rotationSpeed = 10.0f;
    public float followSpeed = 0.1f;
    Vector3 averageHeading;
    Vector3 averagePosition;
    float neighbourDistance = 20.0f;
    Vector3 goalPos = globalFlock.goalPos;
    public float angleSpeed = 0.01f;
    public bool isRotate = true;

    // Transform parentTransform;
    // Transform childTransform;


    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(20.0f,50);
        // parentTransform = GameObject.Find("Parent").GetComponent<Transform>();
        // childTransform = GameObject.Find("Child").GetComponent<Transform>();
        // childTransform.transform.parent = parentTransform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, new Vector3(0,0, transform.position.z))>200){
            Vector3 direction = Vector3.zero - new Vector3(transform.position.x,0,transform.position.z);
            transform.Rotate(0.0f, 0.0f, rotationSpeed * 180.0f);
            transform.Translate(speed * Time.deltaTime,0,0);
        }
        else{
            // rotation angle
            if(Random.Range(0,20000)<20){
                transform.Rotate(0.0f, 1.0f, 0.0f);
            }
            if(Random.Range(0,20000)<20){
                transform.Rotate(0.0f, -1.0f, 0.0f);
            }
            // turn back
            if(Random.Range(0,20000)<20){
                transform.Rotate(0.0f, 0.0f, rotationSpeed * 180.0f);
            }

            // avoid
            // if(this.transform.position.z == 30){
            //     transform.Rotate(0.0f, 0.0f, rotationSpeed * 180.0f);
            //     transform.Translate(speed * Time.deltaTime,0,0);
            // }

            // follow
            // ApplyFollow(goalPos);
        
            transform.Translate(speed * Time.deltaTime,0,0);
        }
    }

    void ApplyFollow(Vector3 goalPos){
        bool isRotate = true;
        if (isRotate)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(goalPos-transform.position),rotationSpeed*Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, goalPos, followSpeed * Time.deltaTime);    
        }
        if (Vector3.Angle(goalPos, transform.position) < 0.1f)
        {
            isRotate = false;
        }
    }

    void ApplyRule(){
        GameObject[] gos;
        gos = globalFlock.allfish;

        Vector3 vcentre = new Vector3(10,10,10);
        Vector3 vavoid = Vector3.zero;
        float gSpeed = 0.1f;

        Vector3 goalPos = globalFlock.goalPos;
        int tankSize = globalFlock.tankSize;

        float dist;
        int groupSize = 0;

        foreach (GameObject go in gos)
        {
            if(go != this.gameObject){
                dist = Vector3.Distance(go.transform.position,this.transform.position);
                if(dist<=neighbourDistance){
                    vcentre += go.transform.position;
                    groupSize++;

                    if(dist<1.0f){
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }

                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }

        if (groupSize>0){
            vcentre = vcentre/groupSize + (goalPos - this.transform.position);
            speed = gSpeed/groupSize;

            // Vector3 direction = goalPos - this.transform.position;
            // Vector3 direction = vcentre + vavoid;
            
            Vector3 direction = new Vector3(Random.Range(-tankSize,tankSize),0,Random.Range(-tankSize,tankSize));
            direction += vcentre + vavoid;
            if(direction != Vector3.zero){
                transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(goalPos-transform.position),rotationSpeed*Time.deltaTime);
            }
        }
    }
}


        // Vector3 vec = -goalPos;
        // Quaternion rotate = Quaternion.LookRotation(vec);
        // if (Vector3.Angle(vec, transform.forward) < 0.1f)
        // {
        //     isRotate = false;
        // }
        // if (isRotate)
        // {
        //     transform.localRotation = Quaternion.Slerp(transform.localRotation, rotate, angleSpeed);
        // }

        // if(turning){
        // transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(direction),rotationSpeed*Time.deltaTime); 
        // }
        // else{
        //     if(Random.Range(0,5)<1){
        //         this.ApplyRule();
        //     }
        // }
