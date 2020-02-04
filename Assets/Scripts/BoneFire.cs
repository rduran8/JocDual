using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneFire : MonoBehaviour
{
 
    public AudioClip eatSound1;                //1 of 2 audio clips that play when the wall is attacked by the player.
    public AudioClip eatSound2;                //2 of 2 audio clips that play when the wall is attacked by the player. 
    private SpriteRenderer spriteRenderer;        //Store a component reference to the attached SpriteRenderer.


    void Awake()
    {
        //Get a component reference to the SpriteRenderer.
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    //DamageWall is called when the player attacks a wall.
    public void DamageWall(int loss)
    {
        //Call the RandomizeSfx function of SoundManager to play one of two chop sounds.
        SoundManager.instance.RandomizeSfx(eatSound1, eatSound2);
    }
}
