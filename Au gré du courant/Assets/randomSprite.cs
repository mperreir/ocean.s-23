using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSprite : MonoBehaviour
{

    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;
    void Start()
    {
        //Pick up a random item from the array
        int randomIndex = Random.Range(0, sprites.Length);
        //Get the sprite at the random index
        Sprite randomSprite = sprites[randomIndex];
        //Set the sprite to the sprite renderer
        spriteRenderer.sprite = randomSprite;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
