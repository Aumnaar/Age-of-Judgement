using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    public GameObject background;
    public GameObject characterMenu;
    public GameObject halo;
    public GameObject stormTree;
    public GameObject passives;
    public GameObject chosenButton;

    bool chosenButtonActive = false;
    public bool StormCrushActive = false;
    public bool LightningSpearActive = false;
    public bool Syphon = false;

    [SerializeField] private PlayerMov playerMov;

    public static CharacterMenu instance;

    public void EnableMenu()
    {
        Time.timeScale = 0;
        characterMenu.SetActive(true);
        background.SetActive(true);
    }

    public void DisableMenu()
    {
        characterMenu.SetActive(false);
        background.SetActive(false);
        CloseStormTree();
        ClosePasssives();
        Time.timeScale = 1;

    }

    ///////////////////////////////////////////////////////

    public void StormCrushEnabled()
    {
       
         StormCrushActive=true;
    }

    public void StormCrushDisabled()
    {
        
        StormCrushActive = false;
    }

    public void ChosenButtonEnable()
    {
        chosenButton.SetActive(true);
    }

    public void ChosenButtonDisables()
    {
        chosenButton.SetActive(false);
    }

    ///////////////////////////////////////////////////////

    public void OpenCategories()
    {
        halo.SetActive(true);
        characterMenu.SetActive(false);

    }

    public void CloseCategories()
    {
        halo.SetActive(false);

    }

    public void OpenPassives()
    {
        passives.SetActive(true);
        characterMenu.SetActive(false);


    }

    public void ClosePasssives()
    {
        passives.SetActive(false);
        
    }

    public void OpenStormTree()
    {
        stormTree.SetActive(true);
        halo.SetActive(false);
    }

    public void CloseStormTree()
    {
        stormTree.SetActive(false);
        
    }

}
