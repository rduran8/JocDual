using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupMessage : MonoBehaviour
{
    public GameObject ui;
    public AudioClip epicSaxGuy;
    public Reproductor reproductor;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Open(Reproductor reproductor)
    {
        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf)
        {
                this.reproductor = reproductor;

                var texture = reproductor.getRawImage();
                RawImage image = ui.gameObject.GetComponentInChildren<RawImage>();
                image.texture = texture;
      
                SoundManager.instance.RandomizeSfx(epicSaxGuy);



            Time.timeScale = 0f;
        }
    }
    public void Close()
    {
        ui.SetActive(!ui.activeSelf);
        if (!ui.activeSelf)
        {
            if (reproductor != null){


                Destroy(reproductor);
                reproductor.gameObject.SetActive(false);
                Player.instance.RestartPlayerPosition();
            }
            SoundManager.instance.stopSound();
            Time.timeScale = 1f;
        }
    }
}
