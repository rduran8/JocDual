using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject
{
    public int playerDamage;

    private Animator animator;                            
    private Transform target;      
    private bool skipMove;
    public AudioClip enemyAttack1;
    public AudioClip enemyAttack2;
    private int wallMove;
    public int vida;

    protected override void Start()
    {
        GameManager.instance.AddEnemyToList(this);
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
    }

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        if(skipMove)
        {
            skipMove = false;
            return;
        }
        base.AttemptMove<T>(xDir, yDir);
        wallMove = 0;
        skipMove = true; 
    }

    public void MoveEnemy()
    {
        int xDir = 0;
        int yDir = 0;
        if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)

            
            yDir = target.position.y > transform.position.y ? 1 : -1;

        else
            xDir = target.position.x > transform.position.x ? 1 : -1;
        AttemptMove<Component>(xDir,yDir);
    }

    protected override void OnCantMove<T>(T component)
    {
        if(!skipMove){
            if(component.tag.Equals("Player"))
            {
                Player hitPlayer = Player.instance;
                hitPlayer.iniciarCombat(this);
                //hitPlayer.LoseLive(playerDamage);
                animator.SetTrigger("enemyAttack");
                SoundManager.instance.RandomizeSfx(enemyAttack1, enemyAttack2);
            }else if(component.tag.Equals("Wall"))
            {
                Debug.Log(component.tag);
                int xDir = 0;
                int yDir = 0;
                yDir = target.position.y > transform.position.y ? 1 : -1;
                if(wallMove == 1)
                {
                    yDir = yDir*(-1);
                }
                if(wallMove == 3)
                {
                    wallMove = 0;
                    return;
                }
                wallMove++;
                AttemptMove<Component>(xDir, yDir);
            }
        }
    }

    public void Attack()
    {
        animator.SetTrigger("enemyAttack");
    }
}
