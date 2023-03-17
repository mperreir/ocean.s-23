using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingManager : MonoBehaviour
{
    static private int next_harmopha_fishing = 1;
    static private int max_harphomas = 5;
    static public int nb_fishing = 0;
    static private int count = 0;
    static private int air_remaining;
    public Harmopha_Manager h1;
    public Harmopha_Manager h2;
    public Harmopha_Manager h3;
    public Harmopha_Manager h4;
    public Harmopha_Manager h5;

    int particles_count;


    // Start is called before the first frame update
    void Start()
    {
        air_remaining = 100000;
        particles_count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Harmopha_Manager[] hs = { h1, h2, h3, h4, h5 };

        if (Input.GetKeyUp(KeyCode.Space) && nb_fishing < max_harphomas)
        {
            switch (next_harmopha_fishing)
            {
                case 1:
                    h1.GoFishing();
                    break;
                case 2:
                    h2.GoFishing();
                    break;
                case 3:
                    h3.GoFishing();
                    break;
                case 4:
                    h4.GoFishing();
                    break;
                case 5:
                    h5.GoFishing();
                    break;
                default:
                    Debug.Log("ERROR : UPDATE OF FISHINGTURN");
                    break;
            }

            if (next_harmopha_fishing == max_harphomas)
                next_harmopha_fishing = 1;
            else
                next_harmopha_fishing++;
        }


        foreach (Harmopha_Manager h in hs)
        {
            var emission = h.bubbles_object.emission;
            particles_count += (int)emission.rateOverTime.constant;
        }

        Debug.Log(particles_count >= air_remaining);
        if (air_remaining <= particles_count)
        {
            foreach (Harmopha_Manager h in hs)
            {
                var main = h.bubbles_object.main;
                main.stopAction = ParticleSystemStopAction.Destroy;
            }
        }
    }

    static public bool HarmophaCanGoFishing(int id_harmopha)
    {
        Debug.Log("METHOD count : " + count + "\n\tnext_fishing" + next_harmopha_fishing + "\tID : " + id_harmopha);
        return (id_harmopha == next_harmopha_fishing);
    }
}
