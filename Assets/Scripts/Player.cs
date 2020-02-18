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
    public int maxLive;
    private Animator animator;           
    public int food;  
    public int live; 
    


    protected override void Start()
    {
        animator = GetComponent<Animator>();
        food = GameManager.instance.playerFoodPoints;
        foodText.text = "Food: " + food;
        live = GameManager.instance.playerLivePoints;
        liveText.text = "Live: " + live;
        Debug.Log(getFood());
        
        if (MainMenu.loadBoolea)
        {
            MainMenu.setBooleaFalse();
            LoadPlayer();
        }
        else
        {
            
        }
        //LoadPlayer();
        SavePlayer();
        Debug.Log(getLife());
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
            AttemptMove<Component>(horizontal, vertical);
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
        Debug.Log("p0");
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
            boneFire.CoockMeat(this);
        }
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

    public void SavePlayer ()
    {
        SaveSystem.SavePlayer(this);
    }
    public void LoadPlayer ()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        food = data.food;
        live = data.life;

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

    public void Eat()
    {
        int menjar = decimal.ToInt32(decimal.Truncate((maxLive-live)/livePerFood));
        Debug.Log(menjar);
        if(menjar == 0 && live != maxLive && food > 0)
        {   
            menjar = 1;
            food -= menjar;
            foodText.text ="Food: " + food +  " -" + menjar;
            int cura = (maxLive-live);
            live = maxLive;
            liveText.text ="Live: " + live +  " +" + cura;
        }else
        {
            if(menjar <= food){
                food -= menjar;
                foodText.text ="Food: " + food +  " -" + menjar;
                int cura = (menjar*livePerFood);
                live += cura;
                liveText.text ="Live: " + live +  " +" + cura;
            }else
            {
                int cura = food*livePerFood;
                live += cura;
                liveText.text ="Live: " + live +  " +" + cura;
                int tfood = food;
                food = 0;
                foodText.text ="Food: " + food +  " -" + tfood;
            }
            
        }
            
        
        
    }
}
