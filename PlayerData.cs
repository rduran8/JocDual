using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData
{
    public int life;

    public int food;

    public PlayerData (Player player)
    {

        life = player.getLife();
        food = player.getFood();
        
    }
   
}
