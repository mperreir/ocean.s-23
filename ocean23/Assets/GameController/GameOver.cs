using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI Text;
    
    public void Setup(int score){
        gameObject.SetActive(true);
        Text.text = score.ToString() + " POINTS";
    }
}
