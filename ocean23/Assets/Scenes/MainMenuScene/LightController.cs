using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightController : MonoBehaviour
{
    private Sprite defaultSprite;
    [SerializeField] private Sprite selectedSprite;
    [SerializeField] private int LightID;

    void setSelectedSprite()
    {
        gameObject.GetComponent<Image>().sprite = selectedSprite;
    }

    void setDefaultSprite()
    {
        gameObject.GetComponent<Image>().sprite = defaultSprite;
    }

    // Start is called before the first frame update
    void UpdateSprite()
    {
        if (SelectorController.ContainsAccessory(LightID))
        {
            setSelectedSprite();
        }
        else
        {
            setDefaultSprite();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        defaultSprite = gameObject.GetComponent<Image>().sprite;
        UpdateSprite();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSprite();
    }
}
