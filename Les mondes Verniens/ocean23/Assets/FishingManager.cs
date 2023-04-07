using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishingManager : MonoBehaviour
{
    // ID of the next harmorpha that will go fishing when action triggered
    static private int next_harmopha_fishing = 1;
    // Total number of harmophas in the game
    static private int max_harphomas = 5;
    // Number of harmophas currently in the fishing path
    static public int nb_fishing = 0;
    // All Harmophas
    public Harmopha_Manager h1;
    public Harmopha_Manager h2;
    public Harmopha_Manager h3;
    public Harmopha_Manager h4;
    public Harmopha_Manager h5;

    // The total capacity of harmophas' air tank
    static public int air_total_capacity;
    // Number of particles (bubbles) emitted
    static public int particles_count;

    // Start is called before the first frame update
    void Start()
    {
        air_total_capacity = 650000;
        particles_count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Harmopha_Manager[] harmorphas_group = { h1, h2, h3, h4, h5 };
        
        // Launch of harmophas to go fishing
        if (InputController.GetKeyDown(InputKey.LaunchHunter) && nb_fishing < max_harphomas)
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

        // Emission of bubbles
        foreach (Harmopha_Manager h in harmorphas_group)
        {
            if (!h.isFishing)
            {
                var emission = h.bubbles_object.emission;
                particles_count += (int)emission.rateOverTime.constant;
            }
            
        }

        // Managing the lack of air
        if (air_total_capacity <= particles_count)
        {
            Debug.Log("NO MORE AIR");
            foreach (Harmopha_Manager h in harmorphas_group)
            {
                h.bubbles_object.Stop();
                h.hasAir = false;
            }
            h1.isBubble = false;
            air_total_capacity = 650000;
            particles_count = 0;
            // Call function in six seconds
            Invoke("EndGame",6);
        }
    }

    // Load GameOver scene
    public void EndGame(){
        SceneManager.LoadScene("Scenes/GameOverScene/GameOver");
    }
}
