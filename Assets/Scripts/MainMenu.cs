﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayButton()
    {
        //SceneManager.LoadScene("Main",LoadSceneMode.Additive);
        //SceneManager.UnloadScene("MainMenu");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.LoadScene("Main", LoadSceneMode.Additive);
        //SceneManager.UnloadScene("MainMenu")
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
    
    public void ExitButton()
    {
        Application.Quit();
    }

    
    
}