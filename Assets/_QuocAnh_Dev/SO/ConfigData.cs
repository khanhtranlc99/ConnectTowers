using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "ConfigData", menuName = "Quoc_Dev/ConfigData", order = 1)]
public class ConfigData : SingletonScriptableObject<ConfigData>
{
    public List<Color> colors;
    public List<Texture2D> texture;
    public List<int> unitLayer;
    public LayerMask obstacle;
    public float TimeAutoIncs;

    [TableList]
    public List<LevelPerData> lv = new List<LevelPerData>();

    private void OnValidate()
    {
        for (int i = 0; i < lv.Count; i++)
        {
            lv[i].Lv = i + 1;
        }
    }

    [TableList]
    public List<Rank> ranks = new List<Rank>();

    [TableList]
    public List<SkillData> skills = new List<SkillData>();

    public Rank GetRank(UnitRank rank)
    {
        foreach (var item in ranks)
        {
            if (item.rank == rank)
            {
                return item;
            }
        }
        return null;
    }

    public SkillData GetSkill(Skill _skill)
    {
        foreach (var item in skills)
        {
            if (item.skill == _skill)
            {
                return item;
            }
        }
        return null;
    }
}

[System.Serializable]

public class Rank
{
    public UnitRank rank;
    public Sprite sprite;
    public Color color;
    public int DuplicateLevel;
    public int Rate;
}

[System.Serializable]
public class SkillData
{
    public Skill skill;
    public string skillName;
    public Sprite sprite;
}
[System.Serializable]
public class LevelPerData
{
    public int Lv;
    public GiftType Type;
    public int Reward;
}