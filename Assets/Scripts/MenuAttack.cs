using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuAttack : MonoBehaviour
{
    public Canvas combat;
    public GameObject menuInfo;
    private Combat CombatScript;
    private MenuInfo menuInfoScript;
    public GameObject SpecialsMenu;


    public Button buttonAttack;
    public Button buttonDefend;
    public Button buttonSpecial;
    public Button buttonRecuperar;


    public Button buttonSpecial1;
    public Button buttonSpecial2;
    public Button buttonSpecial3;
    public Button buttonSpecial4;
    public Button buttonSpecial5;
    public Button buttonSpecial6;
    public Button buttonSpecial7;
    public Button buttonSpecial8;
    public Button buttonSpecial9;
    public Button buttonSpecial10;
    public Button buttonSpecial11;
    public Button buttonSpecial12;
    void Start()
    {
        menuInfoScript = menuInfo.GetComponent<MenuInfo>();
        CombatScript = combat.GetComponent<Combat>();
        SpecialsMenu.SetActive(false);
    }


    
    void Update()
    {
        
    }

    private void setIninteractableButtons()
    {
        buttonAttack.interactable = true;
        buttonDefend.interactable = true;
        buttonSpecial.interactable = true;
        buttonRecuperar.interactable = true;
    }

    public void SelectAttack()
    {
        buttonAttack.interactable = false;
        SpecialsMenu.SetActive(false);
    }
    public void SelectDefend()
    {
        buttonDefend.interactable = false;
        SpecialsMenu.SetActive(false);
    }
    public void SelectSpecial()
    {
        buttonSpecial.interactable = false;
        SpecialsMenu.SetActive(true);
    }
    public void SelectRecuperar()
    {
        buttonRecuperar.interactable = false;
        SpecialsMenu.SetActive(false);
    }
}
