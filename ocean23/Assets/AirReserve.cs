using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirReserve : MonoBehaviour
{
    // Visual object slider for the air reserve
    public Slider air_reserve;

    // Start is called before the first frame update
    void Start()
    {
        // Air reserve at 100%
        air_reserve.value = 100;
    }

    // Update is called once per frame
    void Update()
    {
        // Percentage of air remaining
        float percentage_remaining = (float)(FishingManager.air_total_capacity - FishingManager.particles_count) / FishingManager.air_total_capacity * 100;

        if (percentage_remaining > 100)
        {
            air_reserve.value = 100;
        }
        else if (percentage_remaining < 0)
        {
            air_reserve.value = 0;
        }
        else
        {
            air_reserve.value = percentage_remaining;
        }
    }
}
