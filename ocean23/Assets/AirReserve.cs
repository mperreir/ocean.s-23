using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirReserve : MonoBehaviour
{
    public Slider air_reserve;
    // Start is called before the first frame update
    void Start()
    {
        air_reserve.value = 100;
    }

    // Update is called once per frame
    void Update()
    {
        float value = (float)(FishingManager.air_remaining - FishingManager.particles_count) / FishingManager.air_remaining * 100;
        if (value > 100)
        {
            air_reserve.value = 100;
        }
        else if (value < 0)
        {
            air_reserve.value = 0;
        }
        else
        {
            air_reserve.value = value;
        }
    }
}
