using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{    
    SoundManager soundManager;
    void Start()
    {
        soundManager = GetComponent<SoundManager>();
    }

    public void escapeMenu()
    {
        GameManager.instance.escapeMenu();
        soundManager = GetComponent<SoundManager>();
        SoundManager.instance.iniciarGame();
    }

    public void settingsMenu()
    {
        GameManager.instance.settingsMenu();
        soundManager = GetComponent<SoundManager>();
        SoundManager.instance.iniciarGame();
    }

    public void SetMusicVolume(float vol)
    {
        SoundManager.instance.SetMusicVolume(vol);
    }

    public void SetEffectsVolume(float vol)
    {
        SoundManager.instance.SetEffectsVolume(vol);
    }

    public void SetMusicVolumeOff(bool musicVolumeOn)
    {
        SoundManager.instance.SetMusicVolumeOff(musicVolumeOn);
    }

    public void SetEffectsVolumeOff(bool efxVolumeOn)
    {
        SoundManager.instance.SetEffectsVolumeOff(efxVolumeOn);
    }
}
