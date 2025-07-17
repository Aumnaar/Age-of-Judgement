using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SkillSlot : MonoBehaviour
{
    public SkillSO skillSO;

    public bool isChosen;
    public bool isDone;
    public bool isFreeCost;
    public bool isActive;
    public Toggle toggle;
    public Button openButton;
    public Button closeButton;
    public int currentLevel;
    public bool isUnlocked;
    public Text skillName;
    public Text skillDescription;

    public Image skillIcon;
    public Image unlockIcon;
    public GameObject selector;
    //public GameObject closeButton;

    public static event Action<SkillSlot> OnAbilityPointGain;
    public static event Action<SkillSlot> OnAbilityPointSpent;
    public static event Action<SkillSlot> OnSkillMaxed;
    public static event Action<SkillSlot> OnAbilityDisable;
    public static event Action<SkillSlot> OnAbilityAlreadyActive;

    void OnDisable()
    {
        selector.SetActive(false);
        isChosen = false;
        skillName.text = "";
        skillDescription.text = "";
        toggle.isOn = false;
    }

    public void Start()
    {
        isDone = false;
        isFreeCost = false;
        isActive = false;

    }

    public void LearnButton()
    {

        if (toggle.isOn)
        {
            selector.SetActive(true);
            isChosen = true;
            skillName.text = skillSO.skillName;
            skillDescription.text = skillSO.skillDescription;
        }
        else
        {
            selector.SetActive(false);
            isChosen = false;
            skillName.text = "";
            skillDescription.text = "";
        }
    }

    private void OnValidate()
    {
        if (skillSO != null)
        {
            UpdateUI();
        }
    }

    public void DisablePassive()
    {
        if (currentLevel == skillSO.maxLevel && isChosen)
        {
            currentLevel--;
            skillIcon.sprite = skillSO.unactiveIcon;
            isActive = false;
            OnAbilityDisable?.Invoke(this);
            //closeButton.SetActive(false);
            //closeButton.enabled = false;

        }

    }

    public void TryUpgradeSkill()
    {
        if(isUnlocked && currentLevel < skillSO.maxLevel && !isFreeCost)
        {
            currentLevel = 1;
            OnAbilityPointSpent?.Invoke(this);
            UpdateUI();

            if (!isDone && currentLevel >= skillSO.maxLevel)
            {
                //OnSkillMaxed?.Invoke(this);
                skillIcon.sprite = unlockIcon.sprite;
                isActive = true;
                isDone = true;
                isFreeCost = true;
                //closeButton.SetActive(true);
                //closeButton.enabled = true;
            }

        }
        else if (isFreeCost)
        {
            currentLevel = 1;
            isActive = true;
            skillIcon.sprite = skillSO.unlockIcon;
            OnAbilityAlreadyActive?.Invoke(this);
            //OnAbilityPointSpent?.Invoke(this);

            //closeButton.SetActive(true);
            //closeButton.enabled = true;
        }
    }

    public void TryUpgradeActiveSkill()
    {
            currentLevel = 1;
            isActive = true;
            skillIcon.sprite = skillSO.unlockIcon;
        
    }


    private void UpdateUI()
    {
        skillIcon.sprite = skillSO.skillIcon;

        if (isUnlocked)
        {
            //skillButton.interactable = true;
            //skillIcon.sprite = unlockIcon.sprite;
            skillIcon.color = Color.white;
        }
        else
        {
            //skillButton.interactable = false;
            skillIcon.color = Color.white;
            //skillIcon.sprite = unlockIcon.sprite;
        }
    }

    //public bool CanUnlockSkill()
    //{
    //    foreach (SkillSlot slot in prerequisiteSkillSlots)
    //    {
    //        if (!slot.isUnlocked || slot.currentLevel < slot.skillSO.maxLevel)
    //        {
    //            return false;
    //        }
    //    }

    //    return true;
    //}

    //public void Unlock()
    //{
    //    isUnlocked = true;
    //    UpdateUI();
    //}

}
