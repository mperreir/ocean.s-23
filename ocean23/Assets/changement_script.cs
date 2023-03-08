using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using PathCreation.Examples;

public class changement_script : MonoBehaviour
{
    public PathFollower harmopha, manager;
    public PathCreator path_circle, path_fishing;
    public bool isFishing = false;
    private int updateCount = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !isFishing)
        {
            harmopha.pathCreator = path_fishing;
            isFishing = true;
            harmopha.enabled = true;
            manager.enabled = false;
            Debug.Log("bonjour");
        }

        Vector3 harmopha_pos = GameObject.Find("Harmopha").transform.position;
        Vector3 end_path_pos = harmopha.pathCreator.path.GetPoint(harmopha.pathCreator.path.NumPoints - 1);

        if (Vector3.Distance(harmopha_pos, end_path_pos) < 0.001f && isFishing)
        {
            harmopha.pathCreator = path_circle;
            isFishing = false;
            harmopha.enabled = false;
            manager.enabled = true;
            updateCount = 0;
            GameObject.Find("Harmopha").transform.position = end_path_pos;
            Debug.Log("au revoir");

        }
    }
}
