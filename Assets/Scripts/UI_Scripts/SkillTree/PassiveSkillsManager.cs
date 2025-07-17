using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkillsManager : MonoBehaviour
{
    public HeroHealth heroHealth;
    public PlayerCombat pc;


    private void OnEnable()
    {
        SkillSlot.OnAbilityPointSpent += HandleAbilityPointSpent;
        SkillSlot.OnAbilityDisable += HandleAbilityDisable;
        SkillSlot.OnAbilityAlreadyActive += HandleAbilityAlreadyActive;
    }

    private void OnDisable()
    {
        SkillSlot.OnAbilityPointSpent -= HandleAbilityPointSpent;
        SkillSlot.OnAbilityDisable -= HandleAbilityDisable;
        SkillSlot.OnAbilityAlreadyActive += HandleAbilityAlreadyActive;
    }

    private void HandleAbilityPointSpent(SkillSlot slot)
    {
        string skillName = slot.skillSO.skillName;

        switch (skillName)
        {
            case "Shield of Will":
                heroHealth.manaShield = true;
                heroHealth.shieldOfWill.SetActive(true);
                break;
            case "Power Syphon":
                pc.Syphoning = true;
                pc.Syphon();
                break;
            default:
                Debug.Log("No" + skillName);
                break;
        }
    }

    void HandleAbilityDisable(SkillSlot slot)
    {
        string skillName = slot.skillSO.skillName;
        
        switch (skillName)
        {
            case "Shield of Will":
                heroHealth.manaShield = false;
                heroHealth.shieldOfWill.SetActive(false);
                Debug.Log("!");
                break;
            case "Power Syphon":
                pc.Syphoning = false;
                pc.Syphon();
                break;
            default:
                Debug.Log("No" + skillName);
                break;
        }
    }

    private void HandleAbilityAlreadyActive(SkillSlot slot)
    {
        string skillName = slot.skillSO.skillName;

        switch (skillName)
        {
            case "Shield of Will":
                heroHealth.manaShield = true;
                heroHealth.shieldOfWill.SetActive(true);
                break;
            case "Power Syphon":
                pc.Syphoning = true;
                pc.Syphon();
                break;
            default:
                Debug.Log("No" + skillName);
                break;
        }
    }

}
