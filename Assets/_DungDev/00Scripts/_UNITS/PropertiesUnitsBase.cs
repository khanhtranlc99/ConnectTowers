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

    [Header("Special Skills")]
    public List<UnitSpecialSkill> lsUnitSpecialSkills = new();

    [Header("Star Level Bonus")]
    public List<StarLevelBonus> lsStartLevelBonus = new();




}

[System.Serializable]
public class StarLevelBonus
{
    public string bonusName;  
    public float bonusValue;  
    public Sprite bonusIcon;  
}

[System.Serializable]
public class UnitSpecialSkill
{
    public string skillName;  
    public float skillValue;  
    public Sprite skillIcon;  
}
