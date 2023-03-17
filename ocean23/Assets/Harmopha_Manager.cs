using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using PathCreation.Examples;

public class Harmopha_Manager : MonoBehaviour
{
    // Objects having a PathFollower script attached
    public PathFollower harmopha, manager;
    // path_circle is the path for Harmophas at the bottom of the ocean
    // path_fishing is the path for Harmophas to swim to the surface and go back down
    public PathCreator path_circle, path_fishing;
    public bool isFishing = false;
    // Bubbles particles object
    public ParticleSystem bubbles_object;
    public float starting_point = 0.0f;

    public int id_harmopha;

    // Start is called before the first frame update
    void Start()
    {
        var main = bubbles_object.main;
        main.stopAction = ParticleSystemStopAction.None;
        bubbles_object.Pause();
        harmopha.speed = 105;
        manager.speed = 80;
        manager.distanceTravelled = starting_point;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFishing)
            ReturnToCirclePath();
        else
            BubbleGeneration();
    }

    public void GoFishing()
    {
        harmopha.pathCreator = path_fishing;
        isFishing = true;
        harmopha.enabled = true;
        manager.enabled = false;
        bubbles_object.Stop();
        FishingTurn.nb_fishing++;
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
            FishingTurn.nb_fishing--;
            GameObject.Find("Harmopha" + id_harmopha).transform.position = end_path_pos;
        }
    }

    void BubbleGeneration()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            bubbles_object.Play();
        }

        if (Input.GetKeyUp(KeyCode.B) && bubbles_object.isPlaying)
        {
            bubbles_object.Stop();
        }

    }
}
