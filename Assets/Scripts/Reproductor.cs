using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Reproductor : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private RawImage rawimage;
    // Start is called before the first frame update
 
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rawimage = GetComponent<RawImage>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public Texture getRawImage()
    {
        
        return this.rawimage.texture;
    }

}
