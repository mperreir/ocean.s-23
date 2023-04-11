using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Flock : MonoBehaviour
{
    // public TextMeshProUGUI Text;
    
    // score
    public static int score;
    
    public float speed = 1.0f;
    public float rotationSpeed = 10.0f;
    public float followSpeed = 0.1f;
    Vector3 averageHeading;
    Vector3 averagePosition;
    public int tankSize = globalFlock.tankSize;
    public float angleSpeed = 0.01f;
    public bool isRotate = true;

    public static float w = 0.3f;

    public Harmopha_Manager hm;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(20.0f,50);
        transform.Translate(speed * Time.deltaTime,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        // If bubble 
        if(hm.isBubble){
            CircleAround();
        }
        else{
            if(Random.Range(0,10)<1){
                transform.Rotate(0.0f,0.0f,0.5f);
            }
            if(Random.Range(0,10)<1){
                transform.Rotate(0.0f,0.0f,-0.5f);
            }
            transform.Translate(speed * Time.deltaTime,0,0);
        }

        // If out of range
        if(Vector3.Distance(transform.position,new Vector3(0,0,0))>500){
            transform.position = new Vector3(Random.Range(-500,-400), Random.Range(tankSize / 2 - 15, tankSize / 2 - 5), Random.Range(280, 360));
            transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f,1);
        }

    }
    
    // circle
    public void CircleAround(){
        if(Vector3.Distance(transform.position, new Vector3(0, transform.position.y, 320)) <= 50)
        {
            int z = 2;

            if (Random.Range(0, 3) < 1)
            {
                z = 10;
            }
            // To rotate around a point which is related to the circle of bubble 
            transform.RotateAround(new Vector3(0, transform.position.y, 320), Vector3.up, 100.0f * Time.deltaTime);
        }
        else
        {
            // To move in the direction of head
            transform.Translate(speed * Time.deltaTime,0,0);
        }
    }

    // To calculate the score everytime fish is eated(collision detected by unity) 
    // and reinitilise the position of fish eaten
    void OnTriggerEnter(Collider collider) {
        Vector3 pos = this.transform.position;
        pos.x = -500;
        this.transform.position = pos;
        this.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f,1);
        score += 1;
        
        // if score is realised, load Victory scene
        if(score == 100){
            score = 0;
            SceneManager.LoadScene("Scenes/GameOverScene/Victory");
        }

    }
}



