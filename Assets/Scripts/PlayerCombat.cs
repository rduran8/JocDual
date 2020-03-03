using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCombat : MovingObject
{
    public float restartLevelDelay = 1f;       
    public int pointsPerFood = 1;
    public int livePerFood = 10;               
    public int pointsPerSoda = 20;              
    public int wallDamage = 1;
    public Text foodText;
    public AudioClip moveSound1;
    public AudioClip moveSound2;
    public AudioClip eatSound1;
    public AudioClip eatSound2;
    public AudioClip drinkSound1;
    public AudioClip drinkSound2;
    public AudioClip gameOverSound;
    public Text liveText;
    public int maxLive;
    private Animator animator;           
    public int food;  
    public int live; 
    public static PlayerCombat instance = null;
    private GameObject combat;
    // Start is called before the first frame update
    protected override void Start()
    {
        instance = this;
        combat = GameObject.Find("Combat");
        animator = GetComponent<Animator>();
        base.Start();
    }
private void OnDisable()
    {
        GameManager.instance.playerFoodPoints = food;
        GameManager.instance.playerLivePoints = live;
    }
    
    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        foodText.text = "Food: " + food;
        liveText.text = "Live: " + live;
        base.AttemptMove<T>(xDir, yDir);
        RaycastHit2D hit;
        if (Move(xDir, yDir, out hit))
        {
            SoundManager.instance.RandomizeSfx(moveSound1, moveSound2);
        }
        CheckIfGameOver();
        GameManager.instance.playersTurn = false;
       
    }

    protected override void OnCantMove<T>(T component)
    {
        if(component.tag.Equals("Wall"))
        {
            Wall hitWall = component as Wall;
            hitWall.DamageWall(wallDamage);
            animator.SetTrigger("playerChop");
        }else if(component.tag.Equals("BoneFire"))
        {
            Debug.Log(component.tag);
            BoneFire boneFire = component as BoneFire;
            animator.SetTrigger("playerChop");
            SoundManager.instance.RandomizeSfx(eatSound1, eatSound2);
        }
    }

    [System.Obsolete]
    private void Restart()
    {
        Application.LoadLevel(Application.loadedLevel); 
    }

    public void LoseLive(int loss)
    {
        animator.SetTrigger("playerHit");
        live -= loss;
        liveText.text ="Live: " + live +  " -" + loss;
        CheckIfGameOver();
    }
    public int getFood()
    {
        return food;
    }
    public int getLife()
    {
        return live;
    }
    private void CheckIfGameOver()
    {
        if (live <= 0)
        {
            SoundManager.instance.PlaySingle(gameOverSound);
            SoundManager.instance.musicSource.Stop();
            GameManager.instance.GameOver();

        }
    }

    public void Attack()
    {
        animator.SetTrigger("playerChop");
    }
}
