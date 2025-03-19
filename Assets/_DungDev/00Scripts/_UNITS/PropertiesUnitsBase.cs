using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PropertiesUnitsBase : ScriptableObject
{
    public int iD;
    public UnitsType unitType;
    public UnitRank unitRank;
    public int currentLevel;
    public int starLevel;

    public int countCurrentUnit;

    [Header("Special Skills")]
    public List<UnitSpecialSkill> lsUnitSpecialSkills = new();

    [Header("Star Level Bonus")]
    public List<StarLevelBonus> lsStartLevelBonus = new();
    public virtual float GetSkillValue(string name)
    {
        foreach (var child in lsUnitSpecialSkills)
        {
            if(child.skillName.Equals(name)) return child.skillValue;
        }
        return 0f;
    }

}

[System.Serializable]
public class StarLevelBonus
{
    public string bonusName;  
    public float bonusValue;  
}

[System.Serializable]
public class UnitSpecialSkill
{
    public string skillName;  
    public float skillValue;  
    public Sprite skillIcon;  
}
