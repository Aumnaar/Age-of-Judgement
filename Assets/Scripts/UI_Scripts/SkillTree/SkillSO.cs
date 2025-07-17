using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "New Skill", menuName = "SkillTree/Skill")]
public class SkillSO : ScriptableObject
{
    public string skillName;
    public int maxLevel;
    public Sprite skillIcon;
    public Sprite unlockIcon;
    public Sprite unactiveIcon;

    [TextArea(1, 5)]
    public string skillDescription;

}
