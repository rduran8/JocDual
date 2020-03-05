using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public static Combat instance = null;
    private Player player;
    private PlayerCombat playerCombat;
    private Enemy enemy;
    private GameObject mapa;
    public Animator animator;
    public Canvas canvas;
    public GameObject menuCombat;
    public GameObject menuAtac;
    public GameObject menuInfo;
    private BoardManager  BoardScript;
    private MenuAttack MenuAttackScript;
    private MenuInfo MenuInfoScript;

    // Start is called before the first frame update
    void Start()
    {
        //canvas = GameObject.Find("Combat");
        instance = this;
        mapa = GameObject.Find("BoardExtra");
        menuCombat.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void newCombat(Player player, Enemy enemy)
    {
        mapa = GameObject.Find("BoardExtra");
        this.player = player;
        this.enemy = enemy;
        BoardScript = GameManager.instance.GetComponent<BoardManager>();
        MenuAttackScript = menuAtac.GetComponent<MenuAttack>();
        MenuInfoScript = menuInfo.GetComponent<MenuInfo>();
        FadetoCombat();
    }

    public void addPlayer()
    {
        //BoardManager  BoardScript = BoardManager.script;
        playerCombat = BoardScript.addPlayerOnCombat();
        //player = GameObject.Find("BoardCombat").GetComponentInChildren<pl;
        //BoardScript.newCombat(this,enemy);
    }

    public void addEnemy()
    { 
        enemy = BoardScript.addEnemyOnCombat();
    }

    public void FadetoCombat()
    {
        animator.SetTrigger("Fadeout");
    }

    public void OnFadeComplete()
    {
        mapa.SetActive(false);
        SpriteRenderer render = player.GetComponent<SpriteRenderer>();
        render.enabled = false;
        menuCombat.SetActive(true);
        addPlayer();
        addEnemy();
        animator.SetTrigger("Fadein");
    }

    public void OnFadeInComplete()
    {
        MenuInfo info = menuInfo.GetComponent<MenuInfo>();
        
    }

    public void playerTurn()
    {
        string buttonSeleccionat = null;
        while(buttonSeleccionat == null)
        {
            buttonSeleccionat = MenuAttackScript.getSeleccioButton();
            if(buttonSeleccionat == "Attack")
            {
                Debug.Log("Attack");
                playerCombat.Attack();
                MenuInfoScript.changeEnemyLive(enemy.LoseLive(player.attack));
            }
            else if(buttonSeleccionat == "Defensa")
            {
                Debug.Log("Defensa");
            }
            else if(buttonSeleccionat == "Special")
            {
                Debug.Log("Special");
                playerCombat.Attack();
                MenuInfoScript.changeEnemyLive(enemy.LoseLive(player.attack));
            }
            else if(buttonSeleccionat == "Recuperar")
            {
                Debug.Log("Recuperar");
            }
        }
        MenuInfoScript.continueCombat();
    }
    public void enemyTurn()
    {
        enemy.Attack();
    }
}
