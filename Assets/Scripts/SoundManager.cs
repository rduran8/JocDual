using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundManager : MonoBehaviour
{
    public AudioSource efxSource;                   
    public AudioSource musicSource;                    
    public static SoundManager instance = null;                      
    public float lowPitchRange = .95f;               
    public float highPitchRange = 1.05f;   
    private float musicVolume = 1f;
    private float efxVolume = 1f;
    public Text musicVolumeText;
    public Text efxVolumeText;
    private bool musicVolumeOn = true;
    private bool efxVolumeOn = true;

    private void Start() 
    {
        iniciarGame();
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        iniciarGame();
    }

    public void iniciarGame()
    {
        musicVolumeText = GameObject.Find("MusicVolume").GetComponent<Text>();
        efxVolumeText = GameObject.Find("EffectsVolume").GetComponent<Text>();
        musicVolumeText.text = ((int)(100*musicVolume)).ToString();
        efxVolumeText.text = ((int)(100*efxVolume)).ToString();
        Slider sliderMusica = GameObject.Find("SliderMusic").GetComponent<Slider>();
        Slider sliderEffects = GameObject.Find("SliderEffects").GetComponent<Slider>();
        sliderMusica.value = musicVolume;
        sliderEffects.value = efxVolume;
        GameObject.Find("MussicOff").GetComponent<Toggle>().isOn = musicVolumeOn;
        GameObject.Find("EffectsOff").GetComponent<Toggle>().isOn = efxVolumeOn;
    }

    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void RandomizeSfx(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        efxSource.pitch = randomPitch;
        efxSource.clip = clips[randomIndex];
        efxSource.Play();
    }

    void Update () {
        //Setting volume option of Audio Source to be equal to musicVolume
        if(musicVolumeOn){
            musicSource.volume = musicVolume;
        }else{
            musicSource.volume  = 0;
        }
        if(efxVolumeOn){
            efxSource.volume = efxVolume;
        }else{
            efxSource.volume = 0;
        }
	}

    public void SetMusicVolume(float vol)
    {
        musicVolume = vol;
        musicVolumeText.text = ((int)(100*musicVolume)).ToString();
    }

    public void SetEffectsVolume(float vol)
    {
        efxVolume = vol;
        efxVolumeText.text = ((int)(100*efxVolume)).ToString();
    }

    public void SetMusicVolumeOff(bool musicVolumeOn)
    {
        this.musicVolumeOn = musicVolumeOn;
    }

    public void SetEffectsVolumeOff(bool efxVolumeOn)
    {
       this.efxVolumeOn = efxVolumeOn;
    }
}
