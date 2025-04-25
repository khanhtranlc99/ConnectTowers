using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "Quoc_Dev/UnitData", order = 1)]
public class UnitData : SingletonScriptableObject<UnitData>
{
    [TableList]
    public List<UnitBase> unitBases = new List<UnitBase>();

    public UnitBase GetUnit(int id)
    {
        foreach (var item in unitBases)
        {
            if (item.id == id) return item;
        }
        return null;
    }
#if UNITY_EDITOR
    private void OnValidate()
    {
        for (int i = 0; i < unitBases.Count; i++)
        {
            unitBases[i].showErr = false;
            foreach (var item in unitBases)
            {
                if (item.name == unitBases[i].name && item != unitBases[i])
                {
                    unitBases[i].showErr = true;
                }
            }
        }
    }
#endif
}

[System.Serializable]
public class UnitBase
{
    [TableColumnWidth(150, false)]
    [VerticalGroup("Base")]
    public int id;
    [VerticalGroup("Base")]
#if UNITY_EDITOR
    [InfoBox(
    "Name has already existed.",
    InfoMessageType.Error,
    VisibleIf = "showErr"
  )]
#endif
    public string name;
    [VerticalGroup("Base")]
    public UnitType unitType;
    [VerticalGroup("Base")]
    public UnitRank unitRank;

    [TableColumnWidth(130, false)]
    
    [HideInInspector] public GameObject _prefab;
    [HideInInspector] public GameObject _prefabHigh;

    
#if UNITY_EDITOR
    [HideInInspector]
    public bool showErr = false;
#endif

#if UNITY_EDITOR
    [PreviewField(50)]
    [TableColumnWidth(50, false)]
    public GameObject prefab;
#endif
    public GameObject GetPrefab
    {
        get
        {
#if UNITY_EDITOR
            return Resources.Load<GameObject>("Models/Model_" + id);
#endif

            if (_prefab == null)
            {
                _prefab = Resources.Load<GameObject>("Models/Model_" + id);
            }
            return _prefab;
        }
    }
}
[System.Serializable]
public class SkillUnit
{
    public Skill skill;
    public float value;

    public SkillUnit(SkillUnitData skill, int lv, float addition = 0)
    {
        this.skill = skill.skill;
        this.value = skill._base + lv * skill.increasePerLv + addition;
    }

    public SkillUnit(Skill skill, float value)
    {
        this.skill = skill;
        this.value = value;
    }
}

[System.Serializable]
public class PassiveUnitData
{
    public Skill skill;
    public float first;
    public float second;
    public float third;
}


[System.Serializable]
public class SkillUnitData
{
    public Skill skill;
    public float _base;
    public float increasePerLv;

    public SkillUnitData(Skill skill, float @base, float increasePerLv)
    {
        this.skill = skill;
        _base = @base;
        this.increasePerLv = increasePerLv;
    }

    public SkillUnitData(Skill skill, float @base)
    {
        this.skill = skill;
        _base = @base;
    }
}