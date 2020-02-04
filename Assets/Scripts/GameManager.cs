using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float levelStartDelay = 1f;
    public float turnDelay = 0.1f;
    public static GameManager instance = null;
    public BoardManager boardScript;
    public int playerFoodPoints = 0;
    [HideInInspector] public bool playersTurn = true;

    private Text levelText;
    private GameObject levelImage;
    private GameObject pauseImage;
    private int level = 1;
    private List<Enemy> enemies;
    private bool enemiesMoving;
    private bool doingSetup = true;
    private bool escape = false;

    private static bool inici = true;

    public int playerLivePoints = 100;


    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        enemies = new List<Enemy>();
        boardScript = GetComponent<BoardManager>();
        InitGame();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static public void CallbackInitialization()
    {
        //register the callback to be called everytime the scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    static private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if(inici){
                inici = false;
            }else{
                instance.level++;
                instance.InitGame();
            }
            
        }

    public void InitGame()
    {
        BlockPlayerMove();
        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text = "Day " + level;
        levelImage.SetActive(true);
        pauseImage = GameObject.Find("PauseMenu");
        pauseImage.SetActive(false);
        Invoke("HideLevelImage", levelStartDelay);

        enemies.Clear();
        boardScript.SetupScene(level);
    }

    void HideLevelImage()
    {
        levelImage.SetActive(false);
        UnBlockPlayerMove();
        HidePauseMenu();
    }

    public void GameOver()
    {
        levelText.text = "After " + level + " days, you starved.";
        levelImage.SetActive(true);
        enabled = false;
    }

    public void Update()
    {
        //Menu de pausa
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(escape){
                escape = false;
                pauseImage.SetActive(false);
                UnBlockPlayerMove();
            }else{
                escape = true;
                BlockPlayerMove();
                pauseImage.SetActive(true);
            }
        }

        if (playersTurn || enemiesMoving || doingSetup)
        {
            return;
        }
        StartCoroutine(MoveEnemies());
    }

    public void AddEnemyToList(Enemy script)
    {
        enemies.Add(script);
    }

    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;
        yield return new WaitForSeconds(turnDelay);
        if (enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay);
        }
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].MoveEnemy();
            yield return new WaitForSeconds(enemies[i].moveTime);
        }
        playersTurn = true;
        enemiesMoving = false;
    }

    //sortir menu de pasusa
    void HidePauseMenu()
    {
        pauseImage.SetActive(false);
        UnBlockPlayerMove();
    }

    //impedeix que el jugador es mogui
    public void BlockPlayerMove()
    {
        doingSetup = true;
        playersTurn = false;
    }

    //permet que el jugador es mogui
    public void UnBlockPlayerMove()
    {
        doingSetup = false;
        playersTurn = true;
    }
}
