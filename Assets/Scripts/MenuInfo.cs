using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuInfo : MonoBehaviour
{
    public Text playerLive;
    public Text enemyLive;
    public Text enemyName;
    public Slider playerAttackBar;
    public Slider playerSpecialBar;
    public Slider enemyAttackBar;
    public Slider enemySpecialBar;
    public Canvas combat;
    private Combat CombatScript;

    // Start is called before the first frame update
    void Start()
    {
        CombatScript = combat.GetComponent<Combat>();
    }

    
    void Update()
    {
        playerAttackBar.value += Time.deltaTime/4;
        if(playerAttackBar.value == 1)
        {
            playerAttackBar.value = 0;
            CombatScript.playerAttack();
            enabled = false;
            
        }
        enemyAttackBar.value += Time.deltaTime / 4 - 0.00001f;
        if (enemyAttackBar.value == 1)
        {
            enemyAttackBar.value = 0;
            CombatScript.enemyAttack();
            enabled = false;

        }
    }

    public void iniciCombatInfo()
    {
        
    }
}
