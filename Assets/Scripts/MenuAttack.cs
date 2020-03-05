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
    private string buttonSelecionat;


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

    public void afterAttack()
    {
        setIninteractableButtons();
        SpecialsMenu.SetActive(false);
        buttonSelecionat = null;
    }

    //reinicia tots els buttons principals
    private void setIninteractableButtons()
    {
        buttonAttack.interactable = true;
        buttonDefend.interactable = true;
        buttonSpecial.interactable = true;
        buttonRecuperar.interactable = true;
    }

    //ButtonAttack desactia el de attack al fer click i activa la resta
    public void SelectAttack()
    {
        setIninteractableButtons();
        buttonAttack.interactable = false;
        SpecialsMenu.SetActive(false);
        buttonSelecionat = "Attack";
    }

    //ButtonDefend desactia el de defend al fer click i activa la resta
    public void SelectDefend()
    {
        setIninteractableButtons();
        buttonDefend.interactable = false;
        SpecialsMenu.SetActive(false);
        buttonSelecionat = "Defend";
    }

    //ButtonSpecial desactia el de special al fer click i activa la resta
    public void SelectSpecial()
    {
        setIninteractableButtons();
        buttonSpecial.interactable = false;
        SpecialsMenu.SetActive(true);
        buttonSelecionat = "Special";
    }

    //ButtonRecuperar desactia el de recuperar al fer click i activa la resta
    public void SelectRecuperar()
    {
        setIninteractableButtons();
        buttonRecuperar.interactable = false;
        SpecialsMenu.SetActive(false);
        buttonSelecionat = "Recuperar";
    }

    public string getSeleccioButton()
    {
        return buttonSelecionat;
    }
    
}
