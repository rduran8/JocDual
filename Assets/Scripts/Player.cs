using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MovingObject
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


    private Animator animator;           
    private int food;  
    private int live; 


    protected override void Start()
    {
        animator = GetComponent<Animator>();
        food = GameManager.instance.playerFoodPoints;
        foodText.text = "Food: " + food;
        live = GameManager.instance.playerLivePoints;
        liveText.text = "Live: " + live;
        base.Start();
    }

    private void OnDisable()
    {
        GameManager.instance.playerFoodPoints = food;
        GameManager.instance.playerLivePoints = live;
    }


    private void Update()
    {
        if (!GameManager.instance.playersTurn) return;

        int horizontal = 0;    
        int vertical = 0;   

        horizontal = (int)(Input.GetAxisRaw("Horizontal"));
        vertical = (int)(Input.GetAxisRaw("Vertical"));
        if (horizontal != 0)
        {
            vertical = 0;
        }
        if (horizontal != 0 || vertical != 0)
        {
            AttemptMove<Wall>(horizontal, vertical);
        }
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
        Wall hitWall = component as Wall;
        hitWall.DamageWall(wallDamage);
        animator.SetTrigger("playerChop");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Exit")
        {
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        }
        else if (other.tag == "Food")
        {
            food += pointsPerFood;
            foodText.text ="Food: " + food +  " +" + pointsPerFood;
            SoundManager.instance.RandomizeSfx(eatSound1, eatSound2);
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "Soda")
        {
            food += pointsPerSoda;
            foodText.text = "Food: " + food + " +" + pointsPerSoda;
            SoundManager.instance.RandomizeSfx(drinkSound1, drinkSound2);
            other.gameObject.SetActive(false);
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

    private void CheckIfGameOver()
    {
        if (live <= 0)
        {
            SoundManager.instance.PlaySingle(gameOverSound);
            SoundManager.instance.musicSource.Stop();
            GameManager.instance.GameOver();
        }
    }
}
