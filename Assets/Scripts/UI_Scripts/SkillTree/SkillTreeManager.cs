using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeManager : MonoBehaviour
{
   
    public Button openButton;
    public SkillSlot[] skillSlots;
    public Toggle[] chosenSkill;
    public Text pointsText;
    public int avaliablePoints;
    
    private void OnEnable()
    {
        SkillSlot.OnAbilityPointGain += HandleAbilityPointGain;
        SkillSlot.OnAbilityPointSpent += HandleAbilityPointsSpent;
        //SkillSlot.OnSkillMaxed += HandleSkillMaxed;
      
    }

    private void OnDisable()
    {
        SkillSlot.OnAbilityPointGain -= HandleAbilityPointGain;
        SkillSlot.OnAbilityPointSpent -= HandleAbilityPointsSpent;
        //SkillSlot.OnSkillMaxed -= HandleSkillMaxed;
      
    }

    private void Start()
    {

        foreach (SkillSlot slot in skillSlots)
        {
            {
                //slot.skillButton.onClick.AddListener(() => CheckAvaliablePoints(slot));
                slot.openButton.onClick.AddListener(() => CheckAvaliablePoints(slot));
                slot.closeButton.onClick.AddListener(() => CheckDisable(slot));
            }
        }
        UpdateAbilityPoints(0);
    }

    private void CheckDisable(SkillSlot slot)
    {
        if (slot.isActive && slot.isChosen)
        {
            slot.DisablePassive();
        }
    }

    private void CheckAvaliablePoints(SkillSlot slot)
    {
            
                if (avaliablePoints > 0 && slot.isChosen || slot.isFreeCost && slot.isChosen)
                {
                    slot.TryUpgradeSkill();
                }
           
    }


    //private void HandleSkillMaxed(SkillSlot skillSlot)
    //{
    //    foreach (SkillSlot slot in skillSlots)
    //    {
    //        if (!slot.isUnlocked && slot.CanUnlockSkill())
    //        {
    //            slot.Unlock();
    //        }
    //    }
    //}


    private void HandleAbilityPointsSpent(SkillSlot skillSlot)
    {
        if (avaliablePoints > 0)
        {
            UpdateAbilityPoints(-1);
        }
        
    }

    private void HandleAbilityPointGain(SkillSlot skillSlot)
    {
        if (avaliablePoints > 0)
        {
            UpdateAbilityPoints(+1);
        }

    }


    public void UpdateAbilityPoints(int amount)
    {
        avaliablePoints += amount;
        pointsText.text = "Points:" + avaliablePoints;
    }

}
