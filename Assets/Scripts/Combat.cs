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

    public void playerAttack()
    {
        playerCombat.Attack();
    }
    public void enemyAttack()
    {
        enemy.Attack();
    }
}
