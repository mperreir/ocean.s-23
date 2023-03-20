using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine.UI;
using Unity.VisualScripting;

public class HarmophaSpriteManager : MonoBehaviour
{
    void UpdateSprite()
    {
        List<string> accessoriesList = new();

        // Filet
        if (SelectorController.ContainsAccessory(0)) accessoriesList.Add("filet");
        if (SelectorController.ContainsAccessory(1)) accessoriesList.Add("bulleur");
        if (SelectorController.ContainsAccessory(2)) accessoriesList.Add("reservoir");
        if (SelectorController.ContainsAccessory(3)) accessoriesList.Add("reacteur");

        // Have sprite of combinaison
        accessoriesList.Sort();

        string spriteName = String.Join('_', accessoriesList);

        if (accessoriesList.Count == 0)
            spriteName = "empty";

        var texture = Resources.Load($"HarmophaSprite/{spriteName}") as Texture2D;
        var currentSprite = gameObject.GetComponent<Image>().sprite;

        gameObject.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateSprite();
    }
}
