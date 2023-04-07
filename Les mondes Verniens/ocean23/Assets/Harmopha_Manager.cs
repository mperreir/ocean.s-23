using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using PathCreation.Examples;

/*
  Class managing : 
    - individual harmopha
    - the path it's on
    - its bubbles
*/
public class Harmopha_Manager : MonoBehaviour
{
    // Objects having a PathFollower script attached
    public PathFollower harmopha, manager;
    // path_circle is the path for Harmophas at the bottom of the ocean
    public PathCreator path_circle;
    // path_fishing is the path for Harmophas to swim to the surface and go back down
    public PathCreator path_fishing;
    // Whether or not the Harmopha is currently fishing
    public bool isFishing = false;
    // Whether or not there's still air available
    public bool hasAir = true;
    // Bubbles particles object
    public ParticleSystem bubbles_object;

    public float starting_point = 0.0f;

    public int id_harmopha;

    private int value_emission = 0;
    // for the detection of fish module 
    public bool isBubble = false; 

    // Start is called before the first frame update
    void Start()
    {
        // main = main parameters for the bubble object
        var main = bubbles_object.main;
        main.stopAction = ParticleSystemStopAction.None;
        main.maxParticles = 1000000;
        main.loop = true;
        main.startSpeed = 13;
        main.startLifetime = 10;

        // shape = shape parameters for the bubble object
        var shape = bubbles_object.shape;
        shape.shapeType = ParticleSystemShapeType.Cone;
        shape.angle = 5;

        var emission = bubbles_object.emission;
        emission.rateOverTime = 0;

        bubbles_object.Pause();

        GameObject.Find("Harmopha" + id_harmopha).GetComponent<Rigidbody>().isKinematic = true;

        harmopha.speed = 105;

        manager.speed = 80;
        manager.distanceTravelled = starting_point;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFishing)
            ReturnToCirclePath();
        if (hasAir)
            BubbleGeneration();
    }

    // Method called by the FishingManager
    public void GoFishing()
    {
        var emission = bubbles_object.emission;
        emission.rateOverTime = 0;

        // Changing the path followed by the harmopha
        harmopha.pathCreator = path_fishing;
        isFishing = true;
        harmopha.enabled = true;
        manager.enabled = false;
        bubbles_object.Stop();
        FishingManager.nb_fishing++;
    }

    void ReturnToCirclePath()
    {
        Vector3 harmopha_pos = GameObject.Find("Harmopha" + id_harmopha).transform.position;
        Vector3 end_path_pos = harmopha.pathCreator.path.GetPoint(harmopha.pathCreator.path.NumPoints - 1);

        if (Vector3.Distance(harmopha_pos, end_path_pos) < 0.001f && harmopha.distanceTravelled > 1.0f)
        {
            harmopha.distanceTravelled = 0.0f;
            harmopha.pathCreator = path_circle;
            isFishing = false;
            harmopha.enabled = false;
            manager.enabled = true;
            FishingManager.nb_fishing--;
            GameObject.Find("Harmopha" + id_harmopha).transform.position = end_path_pos;
        }
    }

    void BubbleGeneration()
    {
        var emission = bubbles_object.emission;
        value_emission = InputController.GetBubbleStreamValue();
        emission.rateOverTime = value_emission * 20;
        
        // To launch the emission of bubble so to modify the parameter isBubble
        if (value_emission > 0 && !isFishing)
        {
            bubbles_object.Play();
            isBubble = true;
        }
        else if (value_emission == 0 && bubbles_object.isPlaying && !isFishing)
        {
            bubbles_object.Stop();
            isBubble = false;
        }

    }
}
