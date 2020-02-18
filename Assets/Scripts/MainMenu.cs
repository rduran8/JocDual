using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    static public bool loadBoolea = false;
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
    public void LoadButton()
    {
        loadBoolea = true;
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public static void setBooleaFalse()
    {
        loadBoolea = false;
    }
    
    
}