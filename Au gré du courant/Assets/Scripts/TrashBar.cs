using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashBar : MonoBehaviour
{

    public Slider slider;

    public void setMaxTrash(int trash)
    {
        slider.maxValue = trash;
        slider.value = trash;
    }
    public void setTrash(int trash)
    {
        slider.value = trash;
    } 
}
